using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FCDLL.Controller
{
    public class CopyController
    {
        #region Fields
        /// <summary>
        /// Remote host list.
        /// </summary>
        List<Model.Destination> Targets { get; }

        /// <summary>
        /// Result list.
        /// </summary>
        List<Model.Result> Results { get; }

        /// <summary>
        /// The path to the folder whose contents will be copied.
        /// </summary>
        public Model.Source File { get; }
        #endregion

        #region Ctors

        private CopyController(string source, string newFolderName)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                throw new ArgumentNullException("Путь null или пустой.", nameof(source));
            }
            if (isExist(source))
            {
                File = new Model.Source(source);
            }
            else throw new ArgumentException($@"No such file in directory: {source}");
            Results = new List<Model.Result>();
        }

        /// <summary>
        /// Get the path to a text file with device names.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="txtPath"></param>
        /// <param name="targetFolder"></param>
        /// <param name="newFolderName"></param>
        public CopyController(string source, string txtPath, string targetFolder = "Temp", string newFolderName = "")
            : this(source, new TextReader().Read(txtPath), targetFolder, newFolderName) { }

        /// <summary>
        /// Get the string array with device names.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="targets"></param>
        /// <param name="targetFolder"></param>
        /// <param name="newFolderName"></param>
        public CopyController(string source, string[] targets, string targetFolder = "Temp", string newFolderName = "")
            : this(source, newFolderName)
        {
            if (targets == null)
            {
                throw new ArgumentNullException(nameof(targets));
            }
            if (string.IsNullOrWhiteSpace(targetFolder))
            {
                targetFolder = "Temp";
            }
            Targets = new List<Model.Destination>();


            for (int i = 0; i < targets.Length; i++)
            {
                Targets.Add(new Model.Destination(targets[i], targetFolder, newFolderName));
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Catalog existence.
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        private bool isExist(string source)
        {
            if (Directory.Exists(source))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Start copying. Returns a list of results.
        /// </summary>
        /// <returns></returns>
        public List<Model.Result> CopyRun()
        {
            foreach (Model.Destination target in Targets)
            {
                if (new HostState().Onlne(target.Name))
                {
                    Results.Add(
                        new Model.Result(target,
                        DirectCopy(File.ToString(), target.ToString())));
                }
                else
                {
                    Results.Add(new Model.Result(target, "Device is offline."));
                }
            }
            return Results;
        }

        /// <summary>
        /// Start catalog copying, return Exception.Message.
        /// </summary>
        private string DirectCopy(string sourceDirName, string destDirName, bool copySubDirs = true)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(sourceDirName);
                DirectoryInfo[] dirs = dir.GetDirectories();

                if (!Directory.Exists(destDirName))
                {
                    Directory.CreateDirectory(destDirName);
                }

                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string temppath = Path.Combine(destDirName, file.Name);
                    file.CopyTo(temppath, false);
                }

                if (copySubDirs)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        string temppath = Path.Combine(destDirName, subdir.Name);
                        DirectCopy(subdir.FullName, temppath, copySubDirs);
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
    }
}
