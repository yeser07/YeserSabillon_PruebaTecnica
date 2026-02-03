namespace YeserSabillon_PruebaTecnica.Repositorios
{
    using Dapper;
    using Microsoft.Data.SqlClient;
    using System.Data;
    using YeserSabillon_PruebaTecnica.Configuraciones;
    using YeserSabillon_PruebaTecnica.Models;

    public class FacturaRepositorio
    {
        public readonly string _connectionString;

        public FacturaRepositorio(DBcon db)
        {
            _connectionString = db.ConnectionString;
        }

        public int AgregarFacturaHeader(Factura_Header facturaHeader)
        {
            using var conn = new SqlConnection(_connectionString);

            var parameters = new DynamicParameters();
            parameters.Add("@IdCliente", facturaHeader.IdCliente);
            parameters.Add("@Fecha", facturaHeader.Fecha);
            parameters.Add("@IdFactura", dbType: DbType.Int32, direction: ParameterDirection.Output);

            conn.Execute("SP_INSERTAR_FACTURA_HEADER",
                parameters,
                commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("@IdFactura");
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

        public int GuardarFacturaCompleta(FacturaDetalleRequest request)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            using var tran = conn.BeginTransaction();

            try
            {

                var parametros = new DynamicParameters();
                parametros.Add("@IDCLIENTE", request.FacturaHeader.IdCliente);
                parametros.Add("@FECHA", request.FacturaHeader.Fecha);
                parametros.Add("@IdFactura", dbType: DbType.Int32, direction: ParameterDirection.Output);


                conn.Execute(
                    "SP_INSERTAR_FACTURA_HEADER",
                    parametros,
                    tran,
                    commandType: CommandType.StoredProcedure
                );

                int idFactura = parametros.Get<int>("@IdFactura");

                foreach (var det in request.FacturaDetalles)
                {
                    conn.Execute(
                        "SP_INSERTAR_FACTURA_DETALLE",
                        new
                        {
                            IdFactura = idFactura,
                            det.NumeroDetalle,
                            det.IdProducto,
                            det.Cantidad,
                            det.Total
                        },
                        tran,
                        commandType: CommandType.StoredProcedure
                    );
                }

                tran.Commit();
                return idFactura;
            }
            catch
            {
                tran.Rollback();
                throw;
            }
        }




    }
}
