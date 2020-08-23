using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperTest
{
    public class Source
    {
        public string Prop1 { get; set; }
        public DateTime DateAdded { get; set; }
        public string Area { get; set; }
        public string User { get; set; }
        public Nested Address { get; set; }
    }

    public class Nested
    {
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
    }
}
