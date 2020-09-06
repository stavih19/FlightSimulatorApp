using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net.Sockets;

namespace FlightSimulatorApp
{
    public class ServerModel : IModel
    {
        //string ip = "127.0.0.1";
        //int port = 5402;

        volatile int failCounter = 0;

        readonly PropertyChangedEvent[] panelProperties = new PropertyChangedEvent[8];
        readonly PropertyChangedEvent[] mapProperties = new PropertyChangedEvent[2];
        readonly Dictionary<string, string> mapErrors = new Dictionary<string, string>();

        public double IndicatedHeadingDeg { get { return panelProperties[0].Value; } set { panelProperties[0].Value = value; } }
        public double GpsIndicatedVerticalSpeed { get { return panelProperties[1].Value; } set { panelProperties[1].Value = value; } }
        public double GpsIndicatedGroundSpeedKt { get { return panelProperties[2].Value; } set { panelProperties[2].Value = value; } }
        public double AirspeedIndicatorIndicatedSpeedKt { get { return panelProperties[3].Value; } set { panelProperties[3].Value = value; } }
        public double GpsIndicatedAltitudeFt { get { return panelProperties[4].Value; } set { panelProperties[4].Value = value; } }
        public double AttitudeOIndicatorInternalRollDeg { get { return panelProperties[5].Value; } set { panelProperties[5].Value = value; } }
        public double AttitudeIndicatorInternalPitchDeg { get { return panelProperties[6].Value; } set { panelProperties[6].Value = value; } }
        public double AltimeterIndicatedAltitudeFtt { get { return panelProperties[7].Value; } set { panelProperties[7].Value = value; } }

        public double LatitudeDeg { get { return mapProperties[0].Value; } set { mapProperties[0].Value = value; } }
        public double LongitudeDeg { get { return mapProperties[1].Value; } set { mapProperties[1].Value = value; } }

        public string ErrorLabel { get { return mapErrors["ErrorLabel"]; } set { mapErrors["ErrorLabel"] = value; } }

        volatile Boolean stop;

        public event PropertyChangedEventHandler PropertyChanged;

        readonly ITelnerClient telnetClient;

        public ServerModel(ITelnerClient telnetClient)
        {
            this.telnetClient = telnetClient;
            stop = false;

            // intiate the properties that come from server
            panelProperties[0] = new PropertyChangedEvent("IndicatedHeadingDeg", 0, "/instrumentation/heading-indicator/indicated-heading-deg");
            panelProperties[1] = new PropertyChangedEvent("GpsIndicatedVerticalSpeed", 0, "/instrumentation/gps/indicated-vertical-speed");
            panelProperties[2] = new PropertyChangedEvent("GpsIndicatedGroundSpeedKt", 0, "/instrumentation/gps/indicated-ground-speed-kt");
            panelProperties[3] = new PropertyChangedEvent("AirspeedIndicatorIndicatedSpeedKt", 0, "/instrumentation/airspeed-indicator/indicated-speed-kt");
            panelProperties[4] = new PropertyChangedEvent("GpsIndicatedAltitudeFt", 0, "/instrumentation/gps/indicated-altitude-ft");
            panelProperties[5] = new PropertyChangedEvent("AttitudeOIndicatorInternalRollDeg", 0, "/instrumentation/attitude-indicator/internal-roll-deg");
            panelProperties[6] = new PropertyChangedEvent("AttitudeIndicatorInternalPitchDeg", 0, "/instrumentation/attitude-indicator/internal-pitch-deg");
            panelProperties[7] = new PropertyChangedEvent("AltimeterIndicatedAltitudeFtt", 0, "/instrumentation/altimeter/indicated-altitude-ft");

            // intiate the properties positions that goes to the server
            mapProperties[0] = new PropertyChangedEvent("LatitudeDeg", 0, "/position/latitude-deg");
            mapProperties[1] = new PropertyChangedEvent("LongitudeDeg", 0, "/position/longitude-deg");

            mapErrors["ErrorLabel"] = "";
        }

        /// <summary>
        /// connect to server
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void Connect(string ip, int port)
        {
            telnetClient.Connect(ip, port);
        }

        /// <summary>
        /// disconnect from the server
        /// </summary>
        public void DisConnect()
        {
            stop = true;
            telnetClient.Disconnect();
        }

        /// <summary>
        /// loop that run all time app, and take from server 4 times in seconde the 8 propperties
        /// </summary>
        public void Start()
        {
            new Thread(delegate ()
            {
                // until we disconnect
                while (!stop)
                {
                    // run over the 8 properties
                    for (int i = 0; i < panelProperties.Length; i++)
                    {
                        // get in while we have listenrs
                        if (PropertyChanged != null)
                        {
                            string text = "get " + panelProperties[i].Direcetion + "\n";
                            text = WriteAndRead(text);
                            if (text != "ERR")
                            {
                                double value;
                                if (Double.TryParse(text, out value))
                                {
                                    panelProperties[i].Value = value;
                                }
                                PropertyChanged(this, new PropertyChangedEventArgs(panelProperties[i].Name));
                                DeleteErrors();
                            }
                            else
                            {
                                // when there is error from the server
                                mapErrors["ErrorLabel"] = "Error by getting " + panelProperties[i].Name;
                                PropertyChanged(this, new PropertyChangedEventArgs("ErrorLabel"));
                            }
                        }
                    }

                    //run over the position values
                    for (int i = 0; i < mapProperties.Length; i++)
                    {
                        // get in while we have listenrs
                        if (PropertyChanged != null)
                        {
                            string text = "get " + mapProperties[i].Direcetion + "\n";
                            text = WriteAndRead(text);
                            if (text != "ERR")
                            {
                                double value;
                                if (Double.TryParse(text, out value))
                                {
                                    mapProperties[i].Value = value;
                                }
                                PropertyChanged(this, new PropertyChangedEventArgs(mapProperties[i].Name));
                                DeleteErrors();
                            }
                            else
                            {
                                // when there is error from the server
                                mapErrors["ErrorLabel"] = "Error by getting " + mapProperties[i].Name;
                                PropertyChanged(this, new PropertyChangedEventArgs("ErrorLabel"));
                            }
                        }
                    }
                    Thread.Sleep(250);
                }
            }).Start();
        }

        /// <summary>
        /// push value into the server
        /// </summary>
        /// <param name="direction">path in the server</param>
        /// <param name="valueD">the neew value</param>
        public void UpdateServer(string direction, double valueD)
        {
            string tempV = valueD.ToString();
            if (tempV.Length > 6) { tempV = tempV.Substring(0, 6); }
            WriteAndRead("set " + direction + " " + tempV + "\n\r");
        }

        /// <summary>
        /// check the value in case the value goes out of range
        /// </summary>
        /// <param name="value"></param>
        /// <param name="e"></param>
        /// <returns></returns>
        private double VerifyValue(double value, PropertyChangedEvent e)
        {
            if (e.Min > value) { value = e.Value; }
            if (e.Max < value) { value = e.Value; }

            return value;
        }

        /// <summary>
        /// gattering all the srever working in one place
        /// </summary>
        /// <param name="data">the massage</param>
        /// <returns></returns>
        private string WriteAndRead(string data)
        {
            string newData = null;
            Thread thread = new Thread(delegate ()
            {
                // send the massage
                newData = telnetClient.WriteNread(data);
            });
            thread.Start();
            // check if the srever doesn't answer in 10 seconds
            if (!thread.Join(TimeSpan.FromSeconds(10)))
            {
                if (failCounter > 3)
                {
                    mapErrors["ErrorLabel"] = "ReConnect";
                    PropertyChanged(this, new PropertyChangedEventArgs("ErrorLabel"));
                    failCounter = 0;
                }
                else
                {
                    failCounter++;
                    thread.Join();
                    mapErrors["ErrorLabel"] = "The server busy";
                    PropertyChanged(this, new PropertyChangedEventArgs("ErrorLabel"));
                }
            }
            else
            {
                failCounter = 0;
            }

            return newData;
        }

        // when the values are updated we want to delete the error massage
        private void DeleteErrors()
        {
            new Thread(delegate ()
            {
                Thread.Sleep(3000);
                mapErrors["ErrorLabel"] = "";
                PropertyChanged(this, new PropertyChangedEventArgs("ErrorLabel"));
            }).Start();
        }
    }
}