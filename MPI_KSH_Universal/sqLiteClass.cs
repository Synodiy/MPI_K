using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPI_KSH_Universal
{
    static class sqLiteClass
    {
        public static string sqlDirectory = "profiles//";
        public static string sqlFileName = sqlDirectory + "SystemBD";
        public static string sqlConnectString = "Data Source = " + sqlFileName + "; Version = 3; Password = 906056; UseUTF16Encoding = True;";
    }
}
