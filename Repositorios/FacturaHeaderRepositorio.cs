namespace YeserSabillon_PruebaTecnica.Repositorios
{
    using Dapper;
    using Microsoft.Data.SqlClient;
    using YeserSabillon_PruebaTecnica.Configuraciones;
    using YeserSabillon_PruebaTecnica.Models;

    public class FacturaHeaderRepositorio
    {
        public readonly string _connectionString;

        public FacturaHeaderRepositorio(DBcon db)
        {
            _connectionString = db.ConnectionString;
        }

        public void AgregarFacturaHeader(Factura_Header facturaHeader)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Execute("SP_INSERTAR_FACTURA_HEADER",
                new
                {
                    facturaHeader.IdCliente,
                    facturaHeader.Fecha,
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

    }
}
