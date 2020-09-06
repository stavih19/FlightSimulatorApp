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
    /// Interaction logic for LogOutbuttonC.xaml
    /// </summary>
    public partial class LogOutbuttonC : UserControl
    {
        IModel model;
        MainWindow mainWindow;

        public LogOutbuttonC(IModel model, MainWindow mainWindow)
        {
            this.model = model;
            this.mainWindow = mainWindow;
            InitializeComponent();
        }

        /// <summary>
        /// when we log out
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            model.DisConnect();

            // show the openning window
            OpeningWindow openingWindow = new OpeningWindow();
            openingWindow.Show();

            mainWindow.Close();
        }

        /// <summary>
        /// when we log out and close
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            model.DisConnect();
            mainWindow.Close();
        }
    }
}
