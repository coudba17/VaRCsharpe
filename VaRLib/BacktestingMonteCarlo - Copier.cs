using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaRLib
{
    class BacktestingMonteCarlo : Backtesting
    {
        protected override CalculVar GetCalculVar(DataProvider dp, double alpha = 0.01)
        {
            return new VarMonteCarlo(dp, alpha);
        }
        public BacktestingMonteCarlo(string path, int days = 252) : base(path, days)
        {

        }

        public BacktestingMonteCarlo(DataProvider dp) : base(dp)
        {

        }
    }
}
