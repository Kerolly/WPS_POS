using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using entitati;

namespace entitati
{
    public class ProduseMgr : ProduseMgrAbstract
    {

        public override void WriteElemente()
        {
            base.WriteElemente();

            foreach (ProdusAbstract element in Elemente)
            {
                Produs prod = (Produs)element;
                Console.WriteLine(prod.Descriere());
            }
        }

        public bool VerificareProduse(Produs prodNou)
        {

            foreach (ProdusAbstract element in Elemente)
            {
                if (((Produs)element).VerificareProdus(prodNou))
                    return false;
            }

            return true;
        }

        

        public bool Cautare(Produs prodCautat)
        {

            foreach (ProdusAbstract element in Elemente)
            {
                if (element == prodCautat)
                    return true;
            }

            return false;
        }

        public bool Cautare(string? numeCautat)
        {

            foreach (ProdusAbstract element in Elemente)
            {
                if (element.Nume == numeCautat)
                    return true;
            }
            return false;
        }



        public override void CitireFisier()
        {
            InitListaFromXml();
        }



    }
}
