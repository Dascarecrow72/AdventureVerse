using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace CYOA.cs
{
    class ExceptionHandling
    {
        public void LogException(Exception ex, string exLoc)
        {
            using (FileStream fs = new FileStream(@AppGlobals.sysGameDir + "/" + exLoc + "_" + DateTime.Now.ToFileTime() + ".err", FileMode.Create))
            {
                if (!ex.Message.Contains("Unable to cast object of type"))
                    XamlWriter.Save(ex.Message, fs);                   
            }
        }
    }
}
