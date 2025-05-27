using entitati;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_POS
{
    public class PacheteMgr: ProduseMgrAbstract
    {
        public override void WriteElemente()
        {
            base.WriteElemente();


            foreach (ProdusAbstract element in elemente)
            {
                Produs prod = (Produs)element;
                Console.WriteLine(prod.Descriere());
            }
        }

        public bool VerificareProduse(Produs prodNou)
        {


            foreach (ProdusAbstract element in elemente)
            {
                if (((Produs)element).VerificareProdus(prodNou))
                    return false;
            }

            return true;
        }


        public bool Cautare(Produs prodCautat)
        {

            foreach (ProdusAbstract element in elemente)
            {
                if (element == prodCautat)
                    return true;
            }

            return false;
        }

        public bool Cautare(string? numeCautat)
        {

            foreach (ProdusAbstract element in elemente)
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
