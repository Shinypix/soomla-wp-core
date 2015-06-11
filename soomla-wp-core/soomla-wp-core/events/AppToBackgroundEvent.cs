using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoomlaWpCore.events
{
    public class AppToBackgroundEvent : SoomlaEvent
    {
        public AppToBackgroundEvent() : this(null)
        {
        }

        public AppToBackgroundEvent(Object sender)
            : base(sender)
        {
        }
    }
}
