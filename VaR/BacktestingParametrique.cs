using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaR
{
    class BacktestingParametrique:Backtesting
    {
        protected override CalculVar GetCalculVar(DataProvider dp, double alpha = 0.01)
        {
            return new VarParametrique(dp, alpha);
        }
        public BacktestingParametrique(string path, int days = 252) : base(path, days)
        {

        }

        public BacktestingParametrique(DataProvider dp) : base(dp)
        {

        }
    }
}
