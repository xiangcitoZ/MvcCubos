using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcCubos.Models
{
    [Table("COMPRACUBOS")]
    public class CompraCubos
    {
       
            [Key]
            [Column("ID_PEDIDO")]
            public int IdPedido { get; set; }
            [Column("ID_CUBO")]
            public int IdCubo { get; set; }
            [Column("ID_USUARIO")]
            public int IdUsuario { get; set; }
            [Column("FECHAPEDIDO")]
            public DateTime FechaPedido { get; set; }
          
        


    }
}
