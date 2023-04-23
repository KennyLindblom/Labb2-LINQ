using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labb2_LINQ.Models
{
    public class Kurs
    {
        public int Id { get; set; }
        public string KursNamn { get; set; }

        public Lärare Lärarna { get; set; }

        public Klass Klasser { get; set; }
    }
}
