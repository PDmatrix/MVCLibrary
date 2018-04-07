using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pract.Modules
{
    public class JavaScript
    {
        private static string scriptTag = "<script type=\"\" language=\"\">{0}</script>";

        public static void ConsoleLog(string message)
        {       
            string function = "console.log('{0}');";
            string log = String.Format((string) GenerateCodeFromFunction(function), message);
            HttpContext.Current.Response.Write(log);
        }

        private static string GenerateCodeFromFunction(string function)
        {
            return String.Format(scriptTag, function);
        }
    }
}