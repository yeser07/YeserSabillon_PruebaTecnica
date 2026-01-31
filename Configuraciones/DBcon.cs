namespace YeserSabillon_PruebaTecnica.Configuraciones
{
    using Microsoft.Extensions.Configuration;

    public class DBcon
    {
        public string ConnectionString { get; }

        public DBcon(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}
