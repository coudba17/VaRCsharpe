using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VaRLib;

namespace VaRUnitTest
{   
    // Création d'un tableau de données à partir d'un fichier Excel de test pour faire nos test unitaires
    class Common
    {
        public static DataProvider dp= new ExcelDataProvider("../../PortfolioUnitTest.xlsx");


    }
}
