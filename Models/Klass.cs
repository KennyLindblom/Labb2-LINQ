using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_LINQ.Models
{
    public class Klass
    {
        public int Id { get; set; }
        public string KlassNamn { get; set; }

        public ICollection<Kurs> Kurser { get; set; }

    }
}
