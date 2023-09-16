using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Infrastructure.Services.Abstracts
{
    public interface IFileService
    {
        string Upload(IFormFile file);
        string ChangeFile(IFormFile file,string oldFileName,bool withoutArchive = false);
    }
}
