using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flamboyant.Models
{
    public class ModelVendas
    {
        public string codVenda { get; set; }

        public string DtVenda { get; set; }

        public string UsuarioID { get; set; }

        public string horaVenda { get; set; }

        public double ValorTotal { get; set; }

        public List<ModelItemCarrinho> ItensPedido = new List<ModelItemCarrinho>();

    }
}