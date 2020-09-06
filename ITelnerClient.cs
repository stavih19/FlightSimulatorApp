using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    public interface ITelnerClient
    {
        void Connect(string ip, int port);
        string WriteNread(string massage);
        void Disconnect();
    }
}
