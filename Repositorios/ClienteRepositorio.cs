using System.Runtime.InteropServices.Marshalling;

namespace YeserSabillon_PruebaTecnica.Repositorios
{
    using Dapper;
    using Microsoft.Data.SqlClient;
    using System.Reflection.Metadata.Ecma335;
    using YeserSabillon_PruebaTecnica.Configuraciones;
    using YeserSabillon_PruebaTecnica.Models;

    public class ClienteRepositorio
    {

        private readonly string _connectionString;
        public ClienteRepositorio(DBcon db)
        {
            _connectionString = db.ConnectionString;
        }

        public void AgregarCliente(Cliente cliente)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Execute("SP_INSERTAR_CLIENTE",
                new
                {
                    cliente.Nombre,
                    cliente.Apellido,
                    cliente.Email,
                    cliente.Telefono
                },
                
                commandType: System.Data.CommandType.StoredProcedure
                
                );

        }


        public Cliente ActualizarCliente(int IdCliente,Cliente cliente)
        {
            using var conn = new SqlConnection(_connectionString);
               return conn.QueryFirstOrDefault<Cliente>("SP_ACTUALIZAR_CLIENTE",
                        new
                        {
                            IdCliente,
                            cliente.Nombre,
                            cliente.Apellido,
                            cliente.Email,
                            cliente.Telefono
                        },
            commandType: System.Data.CommandType.StoredProcedure

                    );
        }

        public void EliminarCliente(int IdCliente)
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Execute("SP_ELIMINAR_CLIENTE",
                new
                {
                    IdCliente,
                },
                commandType: System.Data.CommandType.StoredProcedure
                );
        }

        public List<Cliente> ListarClientes()
        {
            using var conn = new SqlConnection(_connectionString);
            return conn.Query<Cliente>(
                "SELECT * FROM CLIENTE"
            ).ToList();
         }

        

    }
}
