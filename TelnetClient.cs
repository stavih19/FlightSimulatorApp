using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    class TelnetClient : ITelnerClient
    {
        TcpClient client;
        NetworkStream stream;
        readonly Mutex mut = new Mutex();

        public NetworkStream Stream()
        {
            return stream;
        }

        /// <summary>
        /// connect to the server
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void Connect(string ip, int port)
        {
            try
            {
                client = new TcpClient();
                client.Connect(ip, port);
                stream = client.GetStream();
                stream.WriteTimeout = 10000;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
        }

        /// <summary>
        /// disconnect from the server
        /// </summary>
        public void Disconnect()
        {
            try
            {
                Thread.Sleep(250);
                client.Close();
                stream.Close();
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }

        /// <summary>
        /// write and than read immediately after that
        /// </summary>
        /// <param name="massage"></param>
        /// <returns></returns>
        public string WriteNread(string massage)
        {
            try
            {
                mut.WaitOne();
                Byte[] data = System.Text.Encoding.ASCII.GetBytes(massage);
                stream.Write(data, 0, data.Length);
                data = new byte[256];
                Int32 bytes = stream.Read(data, 0, data.Length);
                string temp = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                //temp = temp[0..^1];
                temp = temp.Substring(0, temp.Length - 1);
                mut.ReleaseMutex();
                return temp;
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            return "";
        }
    }
}
