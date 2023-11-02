using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Infrastructure.Enums
{
    public enum TokenType : byte
    {
        None = 0,
        RefreshToken = 1,
        OtpToken = 2
    }
}
