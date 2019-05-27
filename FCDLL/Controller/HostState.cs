using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.NetworkInformation;

namespace FCDLL.Controller
{
    public class HostState
    {
        /// <summary>
        /// Check availability of a remote device.
        /// </summary>
        public bool Onlne(string hostname, int time = 50)
        {
            try
            {
                return new Ping().Send(hostname, time).Status.ToString() != "TimedOut" ? true : false;
            }
            catch
            {
                return false;
            }
        }
    }
}
