using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace SoomlaWpCore
{
    
    class SoomlaUtils
    {
        public static void DebugLog(String tag, String message)
        {
            Debug.WriteLine(tag + " " + message);
        }

        private const String TAG = "SOOMLA SoomlaUtils"; //used for Log messages
    }
}
