using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTT.Common
{
    public class FileHelper
    {
        /// <summary>
        /// Finds a unique variation of the supplied file name by appending _1, _2 and so on.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static FileInfo GetUniqueFile(string file)
        {
            var directory = Path.GetDirectoryName(file);
            var fileName = Path.GetFileNameWithoutExtension(file);
            var fileExtension = Path.GetExtension(file);

            for (var i = 1; ; ++i)
            {
                if (!File.Exists(file))
                    return new FileInfo(file);

                file = Path.Combine(directory, fileName + "_" + i + fileExtension);
            }
        }
    }
}
