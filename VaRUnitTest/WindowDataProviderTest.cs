using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VaRLib;

namespace VaRUnitTest
{
    [TestClass]
    public class WindowDataProviderTest
    {
        // On vérifie que la fenêtre déroulante prend les bonnes données à chaque itération
        [TestMethod]
        public void TestMethod1()
        {
            int windowSize = 5; // taille de la fenêtre
            int i0 = 3;
            WindowDataProvider wdp = new WindowDataProvider(Common.dp.MatriceAction, i0, windowSize);
            Assert.IsTrue(wdp.Days == windowSize);
            for(int i=i0; i<i0+windowSize; i++)
            {
                for(int j=0; j<Common.dp.MatriceAction.Colonnes; j++)
                {
                    Assert.IsTrue(Common.dp.MatriceAction[i, j] == wdp.MatriceAction[i - i0, j]);
                }
            }
        }
    }
}
