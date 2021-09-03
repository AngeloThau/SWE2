using SWE_TourManager.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.DataAccessLayer.Common
{
    interface IFileAccess
    {
        int CreateNewTourItemFile(string name, string description, double distance);
        int CreateNewLogItemFile(string report, TourItem tourItem);
        IEnumerable<FileInfo> SearchFiles(string searchTerm, MediaTypes searchType);
        IEnumerable<FileInfo> GetAllFiles(MediaTypes searchType);
    }
}
