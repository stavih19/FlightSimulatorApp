using FlightSimulatorApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightSimulatorApp.Controls
{
    /// <summary>
    /// Interaction logic for PedalsC.xaml
    /// </summary>
    public partial class PedalsC : UserControl
    {
        readonly JoystickVM joystick;

        public PedalsC(IModel model)
        {
            InitializeComponent();

            joystick = new JoystickVM(model);
        }

        /// <summary>
        /// send the command to the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Throttle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            joystick.UpdateThrottle(throttle.Value);
        }

        /// <summary>
        /// send the command to the server
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Alieron_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            joystick.UpdateAileron(alieron.Value);
        }
    }
}
