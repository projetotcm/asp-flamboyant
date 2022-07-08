using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flamboyant.Models
{
    public class ModelItemCarrinho
    {
        [Key]
        public Guid ItemPedidoID { get; set; }

        public string ProdutoID { get; set; }
        public string Produto { get; set; }
        public string PedidoID { get; set; }
        public double valorUnit { get; set; }
        public double valorParcial { get; set; }
        // public virtual modelCarrinho Pedido { get; set; }



        public int Qtd { get; set; }
    }
}