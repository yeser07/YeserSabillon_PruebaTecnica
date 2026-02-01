namespace YeserSabillon_PruebaTecnica.Repositorios
{
    using Dapper;
    using Microsoft.Data.SqlClient;
    using YeserSabillon_PruebaTecnica.Configuraciones;
    using YeserSabillon_PruebaTecnica.Models;

    public class FacturaDetalleRepositorio
    {
        private readonly string _connectionString;
        public FacturaDetalleRepositorio(DBcon db)
        {
            _connectionString = db.ConnectionString;
        }
        public void AgregarFacturaDetalle(Factura_Detalle facturaDetalle)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Execute("SP_INSERTAR_FACTURA_DETALLE",
                new
                {
                    facturaDetalle.IdFactura_Header,
                    facturaDetalle.NumeroDetalle,
                    facturaDetalle.IdProducto,
                    facturaDetalle.Cantidad,
                    facturaDetalle.Total
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }
    }
}
