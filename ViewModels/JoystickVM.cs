using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FlightSimulatorApp.ViewModels
{
    class JoystickVM
    {
        public IModel model;

        private readonly string rudderDirecrion = "/controls/flight/rudder";
        private readonly string elevatorDirecrion = "/controls/flight/elevator";
        private readonly string aileronDirecrion = "/controls/flight/aileron";
        private readonly string throttleDirecrion = "/controls/engines/current-engine/throttle";

        private double rudder;
        private double elevator;
        private double aileron;
        private double throttle;

        public double Rudder
        {
            get { return rudder; }
            set { rudder = value; }
        }
        public double Elevator
        {
            get { return elevator; }
            set { elevator = value; }
        }
        public double VM_Aileron
        {
            get { return aileron; }
            set { aileron = value; }
        }
        public double VM_Throttle
        {
            get { return throttle; }
            set { throttle = value; }
        }

        public JoystickVM(IModel model)
        {
            this.model = model;

            aileron = 0;
            throttle = 0;
        }

        /// <summary>
        /// send to his model the commands
        /// </summary>
        /// <param name="x">rudder value</param>
        /// <param name="y">elevator value</param>
        public void UpdateDirection(double x, double y)
        {
            this.rudder = x;
            this.elevator = y;

            new Thread(
                delegate ()
                {
                    model.UpdateServer(rudderDirecrion, x);
                }).Start();

            new Thread(
                delegate ()
                {
                    model.UpdateServer(elevatorDirecrion, y);
                }).Start();
        }

        /// <summary>
        /// send aliron value to server
        /// </summary>
        public void UpdateAileron(double value)
        {
            new Thread(
                delegate ()
                {
                    model.UpdateServer(aileronDirecrion, value);
                }).Start();
        }

        /// <summary>
        /// send throttle value to server
        /// </summary>
        public void UpdateThrottle(double value)
        {
            new Thread(
                delegate ()
                {
                    model.UpdateServer(throttleDirecrion, value);
                }).Start();
        }
    }
}
