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
        public static void LogDebug(String tag, String message)
        {
            if (SoomlaConfig.logDebug)
            {
                Debug.WriteLine(tag + " " + message);
            }
        }

        public static void LogError(String tag, String message)
        {
            Debug.WriteLine("ERROR " + tag + " " + message);
        }

        private const String TAG = "SOOMLA SoomlaUtils"; //used for Log messages
    }
}
