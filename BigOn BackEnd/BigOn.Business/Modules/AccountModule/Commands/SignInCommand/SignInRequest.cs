﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Business.Modules.AccountModule.Commands.SignInCommand
{
    public class SignInRequest : IRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
