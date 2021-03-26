using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outils
{
    public abstract class Decomposition
    {
        protected Matrice _L;
        protected Matrice _U;
        protected Matrice _A;

        public Decomposition(Matrice A)
        {
            this._A = A;
        }

        public abstract void Decomposer(); // génère les LU

        public Matrice L
        {
            get { return _L; }
        }

        public Matrice U
        {
            get { return _U; }
        }

        public Matrice A
        {
            get { return _A; }
        }

    }
}
