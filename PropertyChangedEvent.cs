using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    class PropertyChangedEvent
    {
        private readonly string name;
        private double valueD;
        private readonly string direcetion;
        private readonly double min;
        private readonly double max;

        public double Value
        {
            get { return valueD; }
            set { valueD = value; }
        }

        public string Name
        {
            get { return name; }
        }

        public string Direcetion
        {
            get { return direcetion; }
        }

        public double Min
        {
            get { return min; }
        }

        public double Max
        {
            get { return max; }
        }

        /// <summary>
        /// object witch help to organize the values information
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="direction"></param>
        public PropertyChangedEvent(string name, double value, string direction)
        {
            this.name = name;
            this.valueD = value;
            this.direcetion = direction;
            this.min = 0;
            this.max = 100;
        }

        /// <summary>
        /// /// object witch help to organize the values information
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="direction"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public PropertyChangedEvent(string name, double value, string direction, double min, double max)
        {
            this.name = name;
            this.valueD = value;
            this.direcetion = direction;
            this.min = min;
            this.max = max;
        }
    }
}
