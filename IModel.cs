using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    public interface IModel : INotifyPropertyChanged
    {
        // connection funcrions
        void Connect(string ip, int port);
        void DisConnect();
        void Start();

        // insert commands to the server
        void UpdateServer(string path, double valueD);

        // the eight values to the panel
        double IndicatedHeadingDeg { set; get; }
        double GpsIndicatedVerticalSpeed { set; get; }
        double GpsIndicatedGroundSpeedKt { set; get; }
        double AirspeedIndicatorIndicatedSpeedKt { set; get; }
        double GpsIndicatedAltitudeFt { set; get; }
        double AttitudeOIndicatorInternalRollDeg { set; get; }
        double AttitudeIndicatorInternalPitchDeg { set; get; }
        double AltimeterIndicatedAltitudeFtt { set; get; }

        // position values
        double LatitudeDeg { set; get; }
        double LongitudeDeg { set; get; }

        // error massage
        string ErrorLabel { set; get; }
    }
}
