using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCDLL.Model
{
    public class Source
    {
        /// <summary>
        /// Path to source directory. Example - c:\Photo will be copied.
        /// </summary>
        public string Path { get; }

        public Source(string path)
        {
            Path = path;
        }

        public override string ToString()
        {
            return Path;
        }
    }
}
