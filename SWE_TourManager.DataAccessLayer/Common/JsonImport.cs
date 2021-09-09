using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWE_TourManager.DataAccessLayer.Common
{
    class JsonImport
    {
        public string getJsonFileName()
        {
            string jsonFilename = "";
            
                Microsoft.Win32.OpenFileDialog dialog = new Microsoft.Win32.OpenFileDialog();
                dialog.FileName = "Tour";
                dialog.DefaultExt = ".json";
                dialog.Filter = "Json files (.json)|*.json";
                Nullable<bool> result = dialog.ShowDialog();
                if (result == true)
                {

                    jsonFilename = dialog.FileName;
                }

            return jsonFilename;
        }
    }
}
