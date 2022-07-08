using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flamboyant.Models
{
    public class ModelFormaPagamento
    {
        public string CodEspecialidade { get; set; }

        [Display(Name = "Especialidade")]
        public string Especialidade { get; set; }
    }
}