using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoomlaWpCore.events
{
    public class AppToForegroundEvent : SoomlaEvent
    {
        public AppToForegroundEvent() : this(null)
        {
        }

        public AppToForegroundEvent(Object sender)
            : base(sender)
        {
        }
    }
}
