using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Infrastructure.Services.Abstracts
{
    public interface ICryptoService
    {
        string ToMd5(string text);
        string ToSha1(string text);

        string Encrypt(string text, bool appliedUrlEncode = false);
        string Decrypt(string text);

    }
}
