using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoomlaWpCore.events
{
    public class SoomlaEvent
    {
        public Object sender;
        public SoomlaEvent(Object _sender)
        {
            sender = _sender;
        }
    }
}
