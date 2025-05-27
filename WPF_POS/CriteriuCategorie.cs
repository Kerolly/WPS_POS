using entitati;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_POS
{
    public class CriteriuCategorie: ICriteriu
    {

        public string Categorie { get; set; }

        public CriteriuCategorie(string categorie)
        {
            Categorie = categorie;
        }

        public bool IsIndeplinit(ProdusAbstract element)
        {
            return element.Categorie.Equals(Categorie);
        }

    }
}
