using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace idle
{
    class EarningsManager
    {
        private double _earnings;
        private double _level;

        public EarningsManager(double level)
        {
            _level = level;
        }

        public double GetEarnings(double earningsCoefficient)
        {
            _earnings  += _level * earningsCoefficient;
            return _earnings;
        }

        public void SetLevel(double level)
        {
            _level = level;
        }

        public bool CanBuyImprovement(double cost)
        {
            return _earnings >= (double)cost;
        }

        public void BuyImprovement(double cost)
        {
            _earnings -= (double)cost;
        }
    }
}
