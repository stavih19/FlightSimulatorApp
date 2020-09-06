using FlightSimulatorApp.Controls;
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

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IModel model;

        public MainWindow(IModel model)
        {
            this.model = model;

            InitializeComponent();

            Joystick joystickC = new Joystick(model);
            joystick.Children.Add(joystickC);

            LogOutbuttonC logOutbutton = new LogOutbuttonC(model, this);
            logOutbuttonC.Children.Add(logOutbutton);

            NavigateMapC navigate = new NavigateMapC(model);
            navigateMapC.Children.Add(navigate);

            RegularPanelsC regularPanels = new RegularPanelsC(model);
            regularPanelsC.Children.Add(regularPanels);
        }
    }
}
