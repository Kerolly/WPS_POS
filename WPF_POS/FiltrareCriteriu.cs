using entitati;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_POS
{
    public class FiltrareCriteriu: IFiltrare
    {
        public IEnumerable<ProdusAbstract> Filtrare(IEnumerable<ProdusAbstract> elemente, ICriteriu criteriu)
        {

            IEnumerable<ProdusAbstract> interogare_linq =
            from elem in elemente
            where criteriu.IsIndeplinit(elem)
            orderby elem.Pret
            select elem;

            return interogare_linq;

        }
    }
}
