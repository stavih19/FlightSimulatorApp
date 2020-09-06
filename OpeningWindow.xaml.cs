using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FlightSimulatorApp
{
    /// <summary>
    /// Interaction logic for OpeningWindow.xaml
    /// </summary>
    public partial class OpeningWindow : Window
    {
        private IModel model;
        private MainWindow mainWindow;

        public OpeningWindow()
        {
            this.model = new ServerModel(new TelnetClient());

            InitializeComponent();
        }

        /// <summary>
        /// when we start to use the app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            string ip = ipAdress.Text;
            int port = Int32.Parse(portAdress.Text);
            try
            {
                model.Connect(ip, port);
                model.Start();

                mainWindow = new MainWindow(model);
                mainWindow.Show();

                this.Close();
            }
            catch (SocketException exept)
            {
                string a = exept.Message;
                ErrorBottn.Content = "ip or port doesn't apropriate";
            }
        }

        /// <summary>
        /// when we close the app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCencel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// delete errror massage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ipAdress_MouseEnter(object sender, MouseEventArgs e)
        {
            Thread.Sleep(250);
            ErrorBottn.Content = "";
        }

        /// <summary>
        /// delete errror massage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void portAdress_MouseEnter(object sender, MouseEventArgs e)
        {
            Thread.Sleep(250);
            ErrorBottn.Content = "";
        }
    }
}
