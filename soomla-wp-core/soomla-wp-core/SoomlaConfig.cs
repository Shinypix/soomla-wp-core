using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoomlaWpCore
{
    class SoomlaConfig
    {
        public static int SOOMLA_VERSION = 1;
        public static bool logDebug = true;
        public const String obfuscationSalt = "q2D34E6fGmlBns45Zdmlks";//new byte[] { 64, 54, 113, 47, 98, 52, 87, 102, 65, 127, 89, 51, 11, 35, 30, 77, 45, 75, 26, 3 };
        public static bool DB_DELETE = false;
    }
}
