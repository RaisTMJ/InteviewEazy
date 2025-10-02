using Recipe.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Application.Interfaces
{
    public  interface ITokenService
    {
        string GenerateToken(User user);
    }
}
