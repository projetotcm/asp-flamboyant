using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flamboyant.Models
{
    public class ModelCliente
    {
        [Display(Name = "")]
        public string ClienteId { get; set; }

        [Display(Name = "Nome")]
        public string ClienteNome { get; set; }

        [Display(Name = "Telefone")]
        //[RegularExpression(@"^\([1-9]{2}\) (?:[2-8]|9[1-9])[0-9]{3}\-[0-9]{4}$", ErrorMessage = "Forneça o número do telefone no formato (00) 00000-0000")]
        public string ClienteTel { get; set; }

        [Display(Name = "CEP")]
        public string ClienteCep { get; set; }

        [Display(Name = "E-mail")]
        public string ClienteEmail { get; set; }

        [Display(Name = "Endereço")]
        public string ClienteEndereco { get; set; }

        [Display(Name = "Usuário")]
        public string usuario { get; set; }

        [Display(Name = "Senha")]
        public string senha { get; set; }
    }
}