using Flamboyant.Dados;
using Flamboyant.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Flamboyant.Controllers
{
    public class HomeController : Controller
    {

        AcoesLogin acl = new AcoesLogin();

        public ActionResult Index()
        {
            return View();
        }

        public void carregaFunc()
        {
            List<SelectListItem> func = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bdFlamboyant3;User=root;pwd=1234567"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tblLogin", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    func.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();

            }
            ViewBag.func = new SelectList(func, "Value", "Text");
        }


        //nossa tela de login
        public ActionResult TelaLogin()
        { 
            return View();
        }

        [HttpPost]
        public ActionResult TelaLogin(ModelLogin cm)
        {
            acl.TestarUsuario(cm);

            //se usuario for diferente de null = vazio
            if (cm.usuario != null && cm.senha != null && cm.tipo != null)
            {
                //Autenticando o usuário
                FormsAuthentication.SetAuthCookie(cm.usuario, false);
                
                Session["usuario"] = cm.usuario;
                Session["senha"] = cm.senha;
                Session["tipo"] = cm.tipo;

                return RedirectToAction("Administrador", "Home"); //(true) se tiver algum conteudo vai ser redirecionado para o about 
            }
            else
            {
                ViewBag.msgLogar = "Usuário e/ou senha incorreto(s)"; //mensagem de erro
                return View(); //(false) senão volte para a tela index se não logar corretamente
            }
        }

        public ActionResult SemAcesso()
        {
            //função do JavaScript de mensagem para o usuario se quer deslogar ou não
            Response.Write("<script>alert('Sem Acesso - Entre em contato com o Administrador do Sistema')</script>");
            return View();
        }

        //serve para finalizar o login 
        public ActionResult Logout()
        {
            //Quando sair automaticamente ele vai zerar as informações
            Session["usuario"] = null;
            Session["Cliente"] = null;

            //assim que sair vai retornar para o home 
            return RedirectToAction("LoginCliente", "Cliente");
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Administrador()
        {
            if (Session["tipo"].ToString() == "1")
            {
                return View();
            }
            else
            {
                return RedirectToAction("SemAcesso", "Home");
            }
        }
    }
}