﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtletiAPI
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityKeyGenerator : ControllerBase
    {

        public static SecurityKey GetSecurityKey()
        {
            var key = Encoding.ASCII.GetBytes(Startup.MasterKey);
            return new SymmetricSecurityKey(key);
        }
    }
}
