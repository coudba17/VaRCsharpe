using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VaRLib;
namespace VaRUnitTest
{
    [TestClass]
    public class SimulationHisto
    {   
        // test sur le taux d'erreur de la VaR historique
        [TestMethod]
        public void TestMethod1()
        {   
            double errorRateMax = 0.5; // niveau d'erreur max accepté
            Backtesting simulation = new Backtesting(Common.dp);
            double errorRateHisto = simulation.StartSimulation();
            Assert.IsTrue(errorRateHisto < errorRateMax);
            Console.WriteLine("Ratio erreur VaR Historique = " + errorRateHisto);
        }
    }
}
