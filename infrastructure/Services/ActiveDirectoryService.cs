using core.Interfaces;
using Microsoft.Extensions.Configuration;
using System.DirectoryServices.AccountManagement;

namespace infrastructure.Services
{
    public class ActiveDirectoryService : IActiveDirectoryService
    {
        private readonly IConfiguration _configuration;

        public ActiveDirectoryService(IConfiguration configuration) 
        {
            _configuration = configuration;
        }

        public async Task<bool> ValidateActiveDirectoryCredentials(string username, string password)
        {
            string adServerIpAddress = _configuration["ADServer:IP"];

            // Create a PrincipalContext for the Active Directory domain using the server IP.
            using (var context = new PrincipalContext(ContextType.Domain, adServerIpAddress))
            {
                // Validate the credentials against the Active Directory server.
                bool isValid = context.ValidateCredentials(username, password);

                // Return the validation result.
                return isValid;
            };
        }
    }
}
