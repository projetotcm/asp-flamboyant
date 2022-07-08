using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flamboyant.Models
{
    public class ModelLogin
    {
        [Display(Name = "Usuário")]
        public string usuario { get; set; }
        [Display(Name = "Senha")]
        public string senha { get; set; }
        public string tipo { get; set; }
    }
}