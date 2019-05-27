using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCDLL.Controller
{
    public class TextReader
    {
        /// <summary>
        /// Just return strings from text file.
        /// </summary>
        /// <param name="txtPath"></param>
        /// <returns></returns>
        public string[] Read(string txtPath)
        {
            if (string.IsNullOrWhiteSpace(txtPath))
            {
                throw new ArgumentException("Txt path can't be null or whitespace.", nameof(txtPath));
            }
            if (!System.IO.File.Exists(txtPath))
            {
                throw new Exception($"File not found on path: {txtPath}");
            }
            return System.IO.File.ReadLines(txtPath).ToArray();
        }
    }
}
