using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maps.MapControl.WPF;

namespace FlightSimulatorApp
{
    class NavigateMapVM : INotifyPropertyChanged
    {
        private readonly IModel model;

        Dictionary<string, double> propMap = new Dictionary<string, double>();

        // map borders
        const double MAXLONGITUDEDEG = 180;
        const double MINLONGITUDEDEG = -180;
        const double MAXLATITUDEDEG = 85;
        const double MINLATITUDEDEG = -85;

        private string location = "";

        public event PropertyChangedEventHandler PropertyChanged;

        public string VM_LocationString
        {
            get { return location; }
        }

        public Location VM_Location
        {
            get { return new Location(propMap["LatitudeDeg"], propMap["LongitudeDeg"]); }
        }

        public NavigateMapVM(IModel model)
        {
            this.model = model;

            propMap["LatitudeDeg"] = 32.018644;
            propMap["LongitudeDeg"] = 34.899;
            // inside parameter for navigate
            propMap["Part"] = 0;

            model.PropertyChanged +=
                delegate (object sender, PropertyChangedEventArgs e)
                {
                    ChangeFileds(e.PropertyName);
                };
        }

        /// <summary>
        /// take new position and locate the plain
        /// </summary>
        /// <param name="name">name of position</param>
        private void ChangeFileds(string name)
        {
            if (name == "LatitudeDeg")
            {
                CheckFrameCoordinate(name, model.LatitudeDeg);
                location = propMap["LatitudeDeg"] + ", " + propMap["LongitudeDeg"];
            }
            else if (name == "LongitudeDeg")
            {
                CheckFrameCoordinate(name, model.LongitudeDeg);
                location = propMap["LatitudeDeg"] + ", " + propMap["LongitudeDeg"];
            }

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("VM_LocationString"));
            }
        }

        /// <summary>
        /// locate the plain on the map by the position values
        /// the values will change in case the values are out of borders
        /// from reduction that the world is a circle, and when the plain get out
        /// od the map it suppos to continue some where in the globe
        /// </summary>
        /// <param name="name">name of value</param>
        /// <param name="coordinate">value</param>
        private void CheckFrameCoordinate(string name, double coordinate)
        {
            if (name == "LongitudeDeg") // x
            {
                double m = coordinate % (2 * MAXLONGITUDEDEG);
                int a = (int)(m / MAXLONGITUDEDEG);
                switch (a % 2)
                {
                    case 0:
                        if (propMap["Part"] == 0 || propMap["Part"] == 3)
                        {
                            propMap["LongitudeDeg"] = coordinate;
                        }
                        else if (propMap["Part"] == 1 || propMap["Part"] == 2)
                        {
                            propMap["LongitudeDeg"] = -m + MAXLONGITUDEDEG;
                        }
                        break;
                    case 1:
                        if (propMap["Part"] == 0 || propMap["Part"] == 3)
                        {
                            propMap["LongitudeDeg"] = m - (2 * MAXLONGITUDEDEG);
                        }
                        else if (propMap["Part"] == 1 || propMap["Part"] == 2)
                        {
                            propMap["LongitudeDeg"] = m - (2 * MAXLONGITUDEDEG);
                        }
                        break;
                }
            }
            else if (name == "LatitudeDeg") // y
            {
                double m = coordinate % (4 * MAXLATITUDEDEG);
                int a = (int)(m / MAXLATITUDEDEG);
                switch (a)
                {
                    case -3:
                        propMap["Part"] = 3;
                        propMap["LatitudeDeg"] = (m % MAXLATITUDEDEG) + MAXLATITUDEDEG;
                        break;
                    case -2:
                        propMap["Part"] = 2;
                        propMap["LatitudeDeg"] = -(m % MAXLATITUDEDEG);
                        break;
                    case -1:
                        propMap["Part"] = 1;
                        propMap["LatitudeDeg"] = -(m % MAXLATITUDEDEG) - MAXLATITUDEDEG;
                        break;
                    case 0:
                        propMap["Part"] = 0;
                        propMap["LatitudeDeg"] = m % MAXLATITUDEDEG;
                        break;
                    case 1:
                        propMap["Part"] = 1;
                        propMap["LatitudeDeg"] = MAXLATITUDEDEG - (m % MAXLATITUDEDEG);
                        break;
                    case 2:
                        propMap["Part"] = 2;
                        propMap["LatitudeDeg"] = -(m % MAXLATITUDEDEG);
                        break;
                    case 3:
                        propMap["Part"] = 3;
                        propMap["LatitudeDeg"] = (m % MAXLATITUDEDEG) - MAXLATITUDEDEG;
                        break;
                }
            }
        }

        /// <summary>
        /// add 180 to the position if needed
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private double JumpLongCoordinate(double x)
        {
            if (x >= 0) { x -= MAXLONGITUDEDEG; }
            else { x += MAXLONGITUDEDEG; }

            return x;
        }
    }
}