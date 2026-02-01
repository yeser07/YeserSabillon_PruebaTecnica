namespace YeserSabillon_PruebaTecnica.Repositorios
{

    using Dapper;
    using Microsoft.Data.SqlClient;
    using System.Reflection.Metadata.Ecma335;
    using YeserSabillon_PruebaTecnica.Configuraciones;
    using YeserSabillon_PruebaTecnica.Models;

    public class ProductoRepositorio
    {
        private readonly string _connectionString;
        public ProductoRepositorio(DBcon db)
        {
            _connectionString = db.ConnectionString;
        }


        public void AgregarProducto(Producto producto)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Execute("SP_INSERTAR_PRODUCTO",
                new
                {
                    producto.Nombre,
                    producto.Precio,
                    producto.Stock
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

        public Producto ActualizarProducto(int IdProducto, Producto producto)
        {
            using var conn = new SqlConnection(_connectionString);
            return conn.QueryFirstOrDefault<Producto>("SP_ACTUALIZAR_PRODUCTO",
                     new
                     {
                         IdProducto,
                         producto.Nombre,
                         producto.Precio,
                         producto.Stock
                     },
         commandType: System.Data.CommandType.StoredProcedure
                 );
        }

        public void EliminarProducto(int IdProducto)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Execute("SP_ELIMINAR_PRODUCTO",
                new
                {
                    IdProducto,
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

        public List<Producto> ListarProductos()
        {
            using var conn = new SqlConnection(_connectionString);
            return conn.Query<Producto>("SELECT * FROM PRODUCTO"
  
                ).ToList();
        }


    }
}
