using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MicroFx.Identity.Dtos.User
{
    public class UserTokenDto:UserDto
    {
        public string Token { get; set; }

        public long Seconds { get; set; }

        public override string ToString()
        {
            return $"{{Token:{Token},Seconds:{Seconds}}}";
        }
    }
}
