using Flamboyant.Dados;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Flamboyant.Controllers
{
    public class FormaPagamentoController : Controller
    {
        // GET: FormaPagamento

        AcoesFormaPagamento acFP = new AcoesFormaPagamento();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ConsFormPag()
        {
                GridView dgv = new GridView();
                dgv.DataSource = acFP.ConsultaFormaPagamento();
                dgv.DataBind();
                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);
                dgv.RenderControl(htw);
                ViewBag.GridViewString = sw.ToString();
                return View();
        }
    }
}