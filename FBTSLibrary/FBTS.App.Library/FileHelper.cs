using FBTS.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBTS.App.Library
{
    public class FileHelper
    {
        public static class Helper
        {
            public static string MapPath(string path)
            {
                return path.Replace(@"~\", AppDomain.CurrentDomain.BaseDirectory);
            }
            public static string GetExcelFileDirectory()
            {
                return MapPath(Constants.UserExcelFileDirectory);
            }
            public static string GetUserImageFolder()
            {
                return MapPath(Constants.UserImagesDirectory);
            }
           
        }
    }
}
