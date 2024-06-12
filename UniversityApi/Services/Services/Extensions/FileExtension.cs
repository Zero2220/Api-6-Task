using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Extensions
{
    namespace UniversityApp.Service.Extentions
    {
        public static class FileExtentions
        {
            public static string Save(this IFormFile file, string root, string folder)
            {
                string newFileName = Guid.NewGuid().ToString() + file.FileName;
                string path = Path.Combine(root, folder, newFileName);

                using (FileStream fs = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(fs);
                }

                return newFileName;
            }
        }
    }
}
