using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.Models
{
    public class AuthenticateResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(bool success, string token)
        {
            this.Success = success;
            this.Token = token;
        }
    }
}
