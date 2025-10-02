using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Application.Features
{
   public record LoginQuery(string Email, string Password): IRequest<string>;
}
