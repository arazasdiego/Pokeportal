using Microsoft.Extensions.Configuration;

namespace Pokeshop.Entities.Data
{
    public class Context : IContext
    {
        private readonly IConfiguration _configuration;

        public Context(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ConnectionString
        {
            get
            {
                return _configuration.GetConnectionString("DefaultConnection");
            }
        }
    }
}
