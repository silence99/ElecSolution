using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Emergence.Common.Authorization
{
    public interface IAuthorizationControl
    {
        string GetAuthorization();
    }
}
