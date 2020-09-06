using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModels
{
    class RegulatorPanelsVM : INotifyPropertyChanged
    {
        IModel model;
        Dictionary<string, double> propMap = new Dictionary<string, double>();

        public event PropertyChangedEventHandler PropertyChanged;

        public double VM_IndicatedHeadingDeg
        {
            get { return ShortNumber(model.IndicatedHeadingDeg); }
        }
        public double VM_GpsIndicatedVerticalSpeed
        {
            get { return ShortNumber(model.GpsIndicatedVerticalSpeed); }
        }
        public double VM_GpsIndicatedGroundSpeedKt
        {
            get { return ShortNumber(model.GpsIndicatedGroundSpeedKt); }
        }
        public double VM_AirspeedIndicatorIndicatedSpeedKt
        {
            get { return ShortNumber(model.AirspeedIndicatorIndicatedSpeedKt); }
        }
        public double VM_GpsIndicatedAltitudeFt
        {
            get { return ShortNumber(model.GpsIndicatedAltitudeFt); }
        }
        public double VM_AttitudeOIndicatorInternalRollDeg
        {
            get { return ShortNumber(model.AttitudeOIndicatorInternalRollDeg); }
        }
        public double VM_AttitudeIndicatorInternalPitchDeg
        {
            get { return ShortNumber(model.AttitudeIndicatorInternalPitchDeg); }
        }
        public double VM_AltimeterIndicatedAltitudeFt
        {
            get { return ShortNumber(model.AltimeterIndicatedAltitudeFtt); }
        }

        public string VM_ErrorLabel
        {
            get { return model.ErrorLabel; }
        }

        public RegulatorPanelsVM(IModel model)
        {
            this.model = model;

            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    ChangeFileds(e.PropertyName);
                };
        }

        private void ChangeFileds(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("VM_" + name));
            }
        }

        /// <summary>
        /// take 4 digits after the point in double value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private double ShortNumber(double value)
        {
            string temp = value.ToString();
            if (temp.Length > 6)
            {
                temp = temp.Substring(0, 5);
            }
            return Double.Parse(temp);
        }
    }
}
