using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tekenprogramma
{
    public class Ornament
    {
        public string ornament, position;

        public Ornament(string ornament, string position)
        {
            this.ornament = ornament;
            this.position = position;
        }

        public Ornament Copy()
        {
            return new Ornament(ornament, position);
        }
    }
}
