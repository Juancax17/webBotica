namespace webBotica2.Models
{
    public class ProductoVM
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Sku { get; set; }
        public decimal Precio { get; set; }
       
        public int Stock { get; set; }
    }

}
