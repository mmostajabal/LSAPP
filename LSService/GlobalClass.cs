using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LSService
{
    public class GlobalClass
    {
        //**************************************************************************
        //  MakeCurTime
        //**************************************************************************

        public String MakeCurTime()
        {
            String CurTime;
            CurTime = "";

            if (DateTime.Now.Hour < 10) CurTime = "0";

            CurTime += DateTime.Now.Hour.ToString() + ":";

            if (DateTime.Now.Minute < 10) CurTime += "0";
            CurTime += DateTime.Now.Minute.ToString() + ":";

            if (DateTime.Now.Second < 10) CurTime += "0";
            CurTime += DateTime.Now.Second.ToString();

            return (CurTime);
        }

    }
}
