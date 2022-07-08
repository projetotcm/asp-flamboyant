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
    public class ClienteController : Controller
    {
        // GET: Cliente
        AcoesLogin acl = new AcoesLogin();

        AcoesCliente acCli = new AcoesCliente();

        public void carregaCliente()
        {
            List<SelectListItem> cli = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bdFlamboyant3;User=root;pwd=1234567"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tblCliente", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    cli.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();

            }
            ViewBag.Cliente = new SelectList(cli, "Value", "Text");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoginCliente()
        {  
            return View();
        }

        [HttpPost]
        public ActionResult LoginCliente(ModelCliente cmCliente)
        {

            acCli.TestarCliente(cmCliente);

            //se usuario for diferente de null = vazio
            if (cmCliente.ClienteNome != null && cmCliente.senha != null)
            {
                //Autenticando o usuário
                FormsAuthentication.SetAuthCookie(cmCliente.ClienteNome, false);

                //Variavel de sessão
                Session["Cliente"] = cmCliente.ClienteNome.ToString();
                Session["CliSenha"] = cmCliente.senha.ToString();
                Session["Clienteid"] = cmCliente.ClienteId.ToString();
                Session["ClienteNome"] = cmCliente.ClienteNome.ToString();
                Session["ClienteEndereco"] = cmCliente.ClienteEndereco.ToString();
                Session["usuario"] = cmCliente.usuario.ToString();
                Session["tipo"] = "0";

                return RedirectToAction("Index", "Cardapio"); //(true) se tiver algum conteudo vai ser redirecionado para o about 
            }
            else
            {
                ViewBag.msgLogar = "Usuário e/ou senha incorreto(s)"; //mensagem de erro
                return View(); //(false) senão volte para a tela index se não logar corretamente
            }
        }


        public ActionResult LoginCliente2()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoginCliente2(ModelCliente cmCliente)
        {

            acCli.TestarCliente(cmCliente);

            //se usuario for diferente de null = vazio
            if (cmCliente.ClienteNome != null && cmCliente.senha != null)
            {
                //Autenticando o usuário
                FormsAuthentication.SetAuthCookie(cmCliente.ClienteNome, false);

                //Variavel de sessão
                Session["ClienteNome"] = cmCliente.ClienteNome.ToString();
                Session["ClienteSenha"] = cmCliente.senha.ToString();
                Session["ClienteId"] = cmCliente.ClienteId.ToString();
                return RedirectToAction("Carrinho", "Venda"); //(true) se tiver algum conteudo vai ser redirecionado para o carrinho
            }
            else
            {
                ViewBag.msgLogar = "Usuário e/ou senha incorreto(s)"; //mensagem de erro
                return View(); //(false) senão volte para a tela index se não logar corretamente
            }
        }

        public ActionResult CadCliente()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CadCliente(ModelCliente cmCliente)
        {
            carregaCliente();
            acCli.inserirCliente(cmCliente);
            Response.Write("<script>alert('Cadastro efetuado com sucesso')</script>");
            return View();
        }

        //listando o Cliente 
        public ActionResult ListarCliente()
        {
            return View(acCli.GetCliente());
        }

        //Deletando o cliente
        public ActionResult excluirCliente(int id)
        {
            acCli.DeleteCliente(id);
            return RedirectToAction("ListarCliente");
        }

        //Editando o Cliente
        public ActionResult editarCliente(string id)
        {
            return View(acCli.GetCliente().Find(model => model.ClienteId == id));
        }

        [HttpPost]
        public ActionResult editarCliente(int id, ModelCliente cmCliente)
        {
            cmCliente.ClienteId = id.ToString();
            
            acCli.atualizaCliente(cmCliente);
            ViewBag.msg = "Atualização realizada com sucesso";
            return View();
        }
    }
}