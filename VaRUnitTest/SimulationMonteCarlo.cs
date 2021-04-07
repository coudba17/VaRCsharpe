using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VaRLib;

namespace VaRUnitTest
{
    [TestClass]
    public class SimulationMonteCarlo
    {
        // test sur le taux d'erreur de la VaR Monte Carlo
        [TestMethod]
        public void TestMethod1()
        {   
            double errorRateMax = 0.5; // niveau d'erreur max accepté
            Backtesting simulation = new BacktestingMonteCarlo(Common.dp);
            double errorRateParametrique = simulation.StartSimulation();
            Assert.IsTrue(errorRateParametrique < errorRateMax);
            Console.WriteLine("Ratio erreur VaR Paramétrique = " + errorRateParametrique);
        }
    }
}
