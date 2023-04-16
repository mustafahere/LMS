using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS
{
    static class Global
    {
        private static int userId = 0;
        private static string userName = "";


        public static int UserId
        {
            get { return userId; }
            set { userId  = value; }
        }

        public static string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
    }
}
