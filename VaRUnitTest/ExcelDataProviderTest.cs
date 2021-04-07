using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VaRLib;
using Outils;
using System.Collections.Generic;

namespace VaRUnitTest
{
    [TestClass]
    public class ExcelDataProviderTest
    {
        private void checkProvider(DataProvider dataProvider)
        {
            int days = 20;
            Assert.IsTrue(dataProvider.Days == days);
            Matrice actions = dataProvider.MatriceAction;
            Assert.IsNotNull(actions);
            Assert.IsTrue(actions.Lignes == days);
            List<Company> companyList = dataProvider.GetCompanyList();
            String[] companyNames = new string[] { "Apple", "Amazon", "Alphabet", "Microsoft", "Facebook" };
            double[] companyWeights = new double[] { 0.2, 0.2, 0.2, 0.2, 0.2 };
            Assert.IsNotNull(companyList);
            Assert.IsTrue(companyList.Count == 5);
            int i = 0;
            foreach (Company comp in companyList)
            {
                Assert.IsTrue(comp.Name == companyNames[i]);
                Assert.IsTrue(comp.Weight == companyWeights[i]);
                i++;
            }
        }

        [TestMethod]
        public void TestMethod1()
        {
            checkProvider(new ExcelDataProvider("/../../../PortfolioUnitTest.xlsx"));
        }
    }
}
