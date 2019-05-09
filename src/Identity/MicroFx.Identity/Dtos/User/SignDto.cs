using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroFx.Identity.Dtos.User
{
    public class SignInDto
    {
        public string UName { get; set; }

        public string Pwd { get; set; }

        public string VCode { get; set; }
    }
}
