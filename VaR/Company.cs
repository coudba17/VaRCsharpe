using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VaR
{
    class Company
    {
        private string name;
        private double weight;


        public Company(string name,double weight)
        {
            this.name = name;
            this.weight = weight;
        }

        public string Name
        {
            get { return name; }
        }

        public double Weight
        {
            get { return weight; }
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }
    }
}
