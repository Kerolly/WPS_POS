using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace entitati
{
    public class ProduseMgrAbstract
    {
        protected static List<ProdusAbstract> elemente = new List<ProdusAbstract>();
        protected static int Id = 0;

        public uint IdGenerator()
        {
            return (uint)Interlocked.Increment(ref Id);
        }

        public void AdaugareElemente(ProdusAbstract element)
        {
            elemente.Add(element);
        }
    }
}
