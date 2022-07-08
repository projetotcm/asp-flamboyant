using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Flamboyant.Models
{
    public class ModelCardapio
    {
        [Display(Name = "")]
        public string CardapioId { get; set; }

        [Display(Name = "Nome do Cardapio")]
        public string NomeCardapio { get; set; }

        [Display(Name = "Descrição do Cardapio")]
        public string DesCardapio { get; set; }

        [Display(Name = "Valor do Cardapio")]
        public string VlCardapio { get; set; }
        
        [Display(Name = "Foto do Cardapio")]
        public string FotoCardapio { get; set; }

    }
}