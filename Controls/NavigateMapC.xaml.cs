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
    /// Interaction logic for NavigateMapC.xaml
    /// </summary>
    public partial class NavigateMapC : UserControl
    {
        private readonly NavigateMapVM navigateMapVM;

        public NavigateMapC(IModel model)
        {
            InitializeComponent();

            //map.ZoomLevel = 12;
            navigateMapVM = new NavigateMapVM(model);
            DataContext = navigateMapVM;
        }

        /// <summary>
        /// show by numbers the position of the plane
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Map_MouseMove(object sender, MouseEventArgs e)
        {
            xC.Content = "x : " + map.Center.Latitude;
            yC.Content = "y : " + map.Center.Longitude;
        }

        /// <summary>
        /// show by numbers the position of the plane
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Map_MouseLeave(object sender, MouseEventArgs e)
        {
            xC.Content = "x : " + map.Center.Latitude;
            yC.Content = "y : " + map.Center.Longitude;
        }
    }
}
