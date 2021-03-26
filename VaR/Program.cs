using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outils;

namespace VaR
{
    class Program
    {   
        static void TestAllVar()
        {
            DataProvider test = new ExcelDataProvider(@"C:\Users\baptc\VaR solution\VaR\Portfolio1.xlsx", 20);

            CalculVar[] tabCalculVar = new CalculVar[] { new VarHistorique(test), new VarMonteCarlo(test), new VarParametrique(test) };

            foreach (CalculVar calculVar in tabCalculVar)
            {
                Tuple<double, double> resultat = calculVar.Calcul(); // On profite du polymorphisme 
                Console.WriteLine("VaR = " + resultat.Item1);
                Console.WriteLine("CVaR = " + resultat.Item2);
            }
        }

        static void TestSimulation()
        {
            DataProvider dp = new ExcelDataProvider(@"C:\Users\baptc\VaR solution\VaR\Portfolio1.xlsx");
            Backtesting simulation = new Backtesting(dp);
            double errorRateHisto = simulation.StartSimulation();
            Console.WriteLine("Ratio erreur VaR Historique = " + errorRateHisto);

             simulation = new BacktestingParametrique(dp);
            double errorRateParametrique =simulation.StartSimulation();
            Console.WriteLine("Ratio erreur VaR Paramétrique = " + errorRateParametrique);

            simulation = new BacktestingMonteCarlo(dp);
            double errorRateMC = simulation.StartSimulation();
            Console.WriteLine("Ratio erreur VaR MC = " + errorRateMC);

        }
        static void Main(string[] args)
        {


            TestAllVar();
            
            Console.ReadLine();


        }
    }
}
