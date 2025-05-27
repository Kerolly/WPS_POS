using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using entitati;

namespace app1
{
    public class ServiciiMgr : ProduseMgrAbstract
    {
        

        public override void WriteElemente()
        {
            base.WriteElemente();


            foreach (ProdusAbstract element in elemente)
            {
                Serviciu serv = (Serviciu)element;
                Console.WriteLine(serv.AltaDescriere());
            }
        }

        public bool VerificareServicii(Serviciu servNou)
        {

            foreach (ProdusAbstract element in elemente)
            {
                if (((Serviciu)element).VerificareServiciu(servNou))
                    return false;
            }

            return true;
        }

        

        public override void CitireFisier()
        {
            InitListaFromXml();
        }

    }
}
