using System.ComponentModel.DataAnnotations;

namespace POOII_CL3.Models
{
    public class Producto
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public String idtipo { get; set;}
        public decimal precio { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime fecha { get; set; }
    }
}
