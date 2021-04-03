using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using VaRLib;


namespace VaRUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAllVar()
        {
            CalculVar[] tabCalculVar = new CalculVar[] { new VarHistorique(Common.dp), new VarMonteCarlo(Common.dp), new VarParametrique(Common.dp) };

            foreach (CalculVar calculVar in tabCalculVar)
            {
                Tuple<double, double> resultat = calculVar.Calcul(); // On profite du polymorphisme 
                Assert.IsTrue(-0.2 <= resultat.Item1 && resultat.Item1 < 0);
                Assert.IsTrue(-0.2 <= resultat.Item2 && resultat.Item2 < 0);
                Console.WriteLine("VaR = " + resultat.Item1);
                Console.WriteLine("CVaR = " + resultat.Item2);
            }
        }
    }
}
