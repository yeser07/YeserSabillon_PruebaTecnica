namespace YeserSabillon_PruebaTecnica.Models
{
    public class Factura_Detalle
    {
        public int IdFactura_Detalle { get; set; }
        public int IdFactura_Header { get; set; }
        public int NumeroDetalle { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }


    }

    public class FacturaDetalleRequest
    {
        public Factura_Header FacturaHeader { get; set; }
        public List<Factura_Detalle> FacturaDetalles { get; set; }
    }
}
