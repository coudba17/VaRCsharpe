using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Outils;

namespace VaR_solution
{
    class Program
    {
        static void Main(string[] args)
        {   
            
            DataProvider test = new ExcelDataProvider(@"D:\Master 2\C#\VaR solution\VaR solution\Portfolio.xlsx");

            CalculVar[] tabCalculVar = new CalculVar[] { new VarHistorique(test), new VarMonteCarlo(test) };

            foreach (CalculVar calculVar in tabCalculVar)
            {
                double resultat = calculVar.Calcul(); // On profite du polymorphisme 
                Console.WriteLine(resultat);
            }


            Console.ReadLine();


        }
    }
}
