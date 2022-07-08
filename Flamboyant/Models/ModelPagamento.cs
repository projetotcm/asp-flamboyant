using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flamboyant.Models
{
    public class ModelPagamento
    { 
        [Display(Name = "")]
        public string Codigo_Pag { get; set; }

        [Display(Name = "Nome do Cliente")]
        public string ClienteId { get; set; }

        [Display(Name = "Forma de Pagamento")]
        public string CodEspecialidade { get; set; }

        public string CodVenda { get; set; }

        public string CoditemVenda { get; set; }

    }
}