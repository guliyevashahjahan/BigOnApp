using BigOn.Infrastructure.Services.Abstracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration configuration;

        public FileService(IHostEnvironment environment, IConfiguration configuration)
        {
            this.environment = environment;
            this.configuration = configuration;
        }

        public string ChangeFile(IFormFile file, string oldFileName, bool withoutArchive = false)
        {
            if (file == null)
            {
                return oldFileName;
            }

            string folder = configuration["physicalPath"];
            if (!string.IsNullOrWhiteSpace(folder))
            {
                folder = Path.Combine(folder, "images");
            }
            else
            {
                folder = Path.Combine(environment.ContentRootPath, "wwwroot", "uploads", "images");
            }

            FileInfo fileInfo = new FileInfo(folder);
            if (withoutArchive && fileInfo.Exists)
            {
                fileInfo.Delete();
            }
            else if (!withoutArchive && fileInfo.Exists)
            {
                var newFileName = Path.Combine(environment.ContentRootPath, "wwwroot", "uploads", "images", $"archive-{oldFileName}");
                fileInfo.MoveTo(newFileName);
            }

            return Upload(file);
        }

        public string Upload(IFormFile file)
        {
            string folder = configuration["physicalPath"];
            if (!string.IsNullOrWhiteSpace(folder))
            {
                folder = Path.Combine(folder, "images");
            }
            else
            {
                folder = Path.Combine(environment.ContentRootPath, "wwwroot", "uploads", "images");
            }
            string fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            string physicalPath = Path.Combine(folder, fileName);

            using (FileStream fs = new FileStream(physicalPath, FileMode.CreateNew, FileAccess.Write))
            {
                file.CopyTo(fs);
            }
            return fileName;
        }
    }
}
