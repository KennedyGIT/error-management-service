using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace core.Interfaces
{
    public interface IActiveDirectoryService
    {
        Task<bool> ValidateActiveDirectoryCredentials(string username, string password);
    }
}
