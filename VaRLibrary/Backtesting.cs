using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outils;

namespace VaRLib
{
    public class Backtesting
    {
        protected DataProvider dp;

        protected virtual CalculVar GetCalculVar(DataProvider dp,double alpha=0.01)
        {
            return new VarHistorique(dp, alpha);
        }
        public Backtesting(string path,int days=252)
        {
            dp = new ExcelDataProvider(path,days) ;
        }

        public Backtesting(DataProvider dp)
        {
            this.dp = dp;
        }

        public double StartSimulation(int windowSize=20,double alpha=0.01)
        {
            
            CalculVar calculVarTotal = GetCalculVar(dp,alpha);
            calculVarTotal.Calcul();
            Matrice rendements = calculVarTotal.GetRendement();

 
            int i0 = 0;
            int errorCount = 0;
            int n = dp.Days- windowSize;
            for (int i = 0; i < n; i++)
            {
                WindowDataProvider windowdp = new WindowDataProvider(dp.MatriceAction, i0, windowSize);
                windowdp.SetCompanyList(dp.GetCompanyList());
                CalculVar calcul = GetCalculVar(windowdp,alpha);
                double Var = calcul.Calcul().Item1;
                double r = 0;
                int k = i0 + windowSize;
                if (k<rendements.Lignes)
                {
                    for (int j = 0; j < rendements.Colonnes; j++)
                    {
                        r += rendements[k, j] * dp.GetCompanyList()[j].Weight;
                    }
                   
                    
                    if (Var>r)
                    {
                        errorCount++;
                    }
                    

                }

                
                i0++;
            }

            double errorRate = (double) errorCount / n;

            return errorRate;
        }

    }
}
