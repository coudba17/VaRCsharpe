﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Outils
{
    public interface IIndexable // définit une facette à toutes classes qui implémentent cette interface
    {

        double this[int i, int j] // this : la méthode s'applique sur l'objet, donc pas de nom de propriétés
        {
            get;
            set;

        }

        
        
    }
}
