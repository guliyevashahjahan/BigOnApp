using BigOn.Infrastructure.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Infrastructure.Services.Concrates
{
    public class FileService : IFileService
    {
        private readonly IHostEnvironment environment;

        public FileService(IHostEnvironment environment)
        {
            this.environment = environment;
        }

        public string ChangeFile(IFormFile file, string oldFileName, bool withoutArchive = false)
        {
            if (file == null)
            {
                return oldFileName;
            }
            string physicalPath = Path.Combine(environment.ContentRootPath, "wwwroot", "uploads", "images", oldFileName);
            FileInfo fileInfo = new FileInfo(physicalPath);
            if (withoutArchive && fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            else if (!withoutArchive && fileInfo.Exists) 
            {
                var newFileName = Path.Combine(environment.ContentRootPath, "wwwroot", "uploads", "images",$"archive-{oldFileName}" );
             fileInfo.MoveTo(newFileName);
            }

            return Upload(file);
        }

        public string Upload(IFormFile file)
        {
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string physicalPath = Path.Combine(environment.ContentRootPath, "wwwroot", "uploads", "images", fileName);

            using (FileStream fs = new FileStream(physicalPath, FileMode.CreateNew, FileAccess.Write))
            {
                file.CopyTo(fs);
            }
            return fileName;
        }
    }
}
