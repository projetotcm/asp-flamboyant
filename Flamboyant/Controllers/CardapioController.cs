using Flamboyant.Dados;
using Flamboyant.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flamboyant.Controllers
{
    public class CardapioController : Controller
    {
        // GET: Cardapio
        AcoesCardapio acCard = new AcoesCardapio();

        public void carregaCardapio()
        {
            List<SelectListItem> card = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bdFlamboyant3;User=root;pwd=1234567"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tblCardapio", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    card.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();

            }
            ViewBag.Cardapio = new SelectList(card, "Value", "Text");
        }


        public ActionResult Index()
        {
            ViewBag.usu = "Olá, " + Session["usuario"];
            Response.Write("<script>alert(ViewBag.usu)</script>");
            return View(acCard.GetCardapio());
        }

        public ActionResult CadCardapio()
        {
            if(Session["tipo"].ToString() == "1")
            {
                return View();
            }
            else
            {
                return RedirectToAction("SemAcesso", "Home");
            }
        }


        [HttpPost]
        public ActionResult CadCardapio(ModelCardapio cmCardapio, HttpPostedFileBase file)
        {
            string arquivo = Path.GetFileName(file.FileName);
            string file2 = "/img/" + Path.GetFileName(file.FileName);
            string _path = Path.Combine(Server.MapPath("~/img"), arquivo);
            file.SaveAs(_path);
            cmCardapio.FotoCardapio = file2;
            acCard.inserirCardapio(cmCardapio);
            Response.Write("<script>alert('Cadastro efetuado com sucesso')</script>");

            return View();
        }

        //listando o Cardapio
        public ActionResult ListarCardapio()
        {
            return View(acCard.GetCardapio());
        }

        //Deletando o cliente
        public ActionResult excluirCardapio(int id)
        {
            if (Session["tipo"].ToString() == "1")
            {
                acCard.DeleteCardapio(id);
                return RedirectToAction("ListarCardapio");
            }
            else
            {
                return RedirectToAction("SemAcesso", "Home");
            }
        }

        //Editando o Cliente
        public ActionResult editarCardapio(string id)
        {
            if (Session["tipo"].ToString() == "1")
            {
                return View(acCard.GetCardapio().Find(model => model.CardapioId == id));
            }
            else
            {
                return RedirectToAction("SemAcesso", "Home");
            }
        }

        [HttpPost]
        public ActionResult editarCardapio(int id, ModelCardapio cmCardapio, HttpPostedFileBase file)
        {
            cmCardapio.CardapioId = id.ToString();
            string arquivo = Path.GetFileName(file.FileName);
            string file2 = "/img/" + Path.GetFileName(file.FileName);
            string _path = Path.Combine(Server.MapPath("~/img"), arquivo);
            file.SaveAs(_path);
            cmCardapio.FotoCardapio = file2;
            acCard.atualizaCardapio(cmCardapio);
            ViewBag.msg = "Alteração realizada com sucesso";
            return View();
        }

        public ActionResult Detalhes(string id)
        {
            return View(acCard.GetCardapio().Find((smodel => smodel.CardapioId == id)));
        }
    }
}