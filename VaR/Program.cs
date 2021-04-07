using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outils;
using VaRLib;

namespace VaR
{
    class Program
    {   
    
        static void TestAllVar() // Méthode pour calculer l'ensemble des VaR et de CVaR
        {
            DataProvider test = new ExcelDataProvider(@"C:\Users\baptc\VaR solution\VaR\Portfolio1.xlsx");

            CalculVar[] tabCalculVar = new CalculVar[] { new VarHistorique(test), new VarMonteCarlo(test), new VarParametrique(test) };

            foreach (CalculVar calculVar in tabCalculVar)
            {
                Tuple<double, double> resultat = calculVar.Calcul(); 
                Console.WriteLine("VaR = " + resultat.Item1);
                Console.WriteLine("CVaR = " + resultat.Item2);
            }
        }

        static void TestSimulation() // Méthode pour tester le taux d'erreur des VaR
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
