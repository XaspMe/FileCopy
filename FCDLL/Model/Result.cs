using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCDLL.Model
{
    public class Result
    {
        
        /// <summary>
        /// Exception message.
        /// </summary>
        public string CopyResult { get; }
        public Destination Destination { get; }


        public Result(Destination target, string exception)
        {
            if (target == null)
            {
                throw new ArgumentNullException(nameof(target));
            }

            if (exception == null)
            {
                CopyResult = "No exception.";
            }
            else
                CopyResult = exception.ToString();

            Destination = target;
        }
    }
}
