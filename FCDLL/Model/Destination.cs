using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FCDLL.Model
{
    public class Destination
    {
        /// <summary>
        /// Remote host name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Path on remote host, for example \\hostname\С\..
        /// </summary>
        public string Folder { get; }


        /// <summary>
        /// New folder name, if required.
        /// </summary>
        public string NewFolderName { get; }

        /// <summary>
        /// Combined path.
        /// </summary>
        public string FullPath { get; }

        private Destination(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name of target cannot be null", nameof(name));
            }
            Name = name;
        }


        /// <summary>
        /// If new folder does not required set:""
        /// </summary>
        /// <param name="name"></param>
        /// <param name="folder"></param>
        /// <param name="newFolderName"></param>
        public Destination(string name, string folder, string newFolderName = "") : this(name)
        {
            if (string.IsNullOrWhiteSpace(folder))
            {
                throw new ArgumentException("Folder of target cannot be null", nameof(folder));
            }
            Folder = folder;
            NewFolderName = newFolderName;
            FullPath = $@"\\{Name}\c\{Folder}\" + (NewFolderName == "" ? "" : $@"{NewFolderName}\");
        }

        public override string ToString()
        {
            return FullPath;
        }
    }
}
