
using MapperLibrary;
using System;

namespace MapperTest
{
   public partial class ExtendedActions:CustomActions<Source>
    {
        public object AssignArea(Source o)
        {

            return string.Format("{0} {1}", o.Area,"Added");
        }
        public object AssignDescription(Source o)
        {
            return string.Format("{0} {1}", "Source Comments",o.Address.Address2);
        }
    }
}
