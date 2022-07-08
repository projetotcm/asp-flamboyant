using Flamboyant.Dados;
using Flamboyant.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flamboyant.Controllers
{
    public class PagamentoController : Controller
    {
        // GET: Pagamento

        AcoesFormaPagamento acFP = new AcoesFormaPagamento();
        AcoesPagamento acPag = new AcoesPagamento();
        AcoesCliente acCli = new AcoesCliente();
        AcoesVenda acV = new AcoesVenda();
        AcoesItemCarrinho acI = new AcoesItemCarrinho();

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


        public void carregaFormaPagamento()
        {
            List<SelectListItem> formaPag = new List<SelectListItem>();

            using (MySqlConnection con = new MySqlConnection("Server=localhost;DataBase=bdFlamboyant3;User=root;pwd=1234567"))
            {
                con.Open();
                MySqlCommand cmd = new MySqlCommand("select * from tblFormaPagamento;", con);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    formaPag.Add(new SelectListItem
                    {
                        Text = rdr[1].ToString(),
                        Value = rdr[0].ToString()
                    });
                }
                con.Close();
                con.Open();
            }


            ViewBag.FormaPagamento = new SelectList(formaPag, "Value", "Text");
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CadPagamento(ModelVendas x)
        {
            if(Session["usuario"] == null){
                return RedirectToAction("LoginCliente", "Cliente");
            }
            else
            {           
            var carrinho = Session["Carrinho"] != null ? (ModelVendas)Session["Carrinho"] : new ModelVendas();

            ModelVendas md = new ModelVendas();
            ModelItemCarrinho mdV = new ModelItemCarrinho();

            md.DtVenda = DateTime.Now.ToLocalTime().ToString("dd/MM/yyyy");
            md.horaVenda = DateTime.Now.ToLocalTime().ToString("HH:mm");
            md.UsuarioID = Session["ClienteId"].ToString();
            md.ValorTotal = carrinho.ValorTotal;
            acV.inserirVenda(md);

            acV.buscaIdVenda(x);

                for (int i = 0; i < carrinho.ItensPedido.Count; i++)
                {

                    mdV.PedidoID = x.codVenda;
                    mdV.ProdutoID = carrinho.ItensPedido[i].ProdutoID;
                    mdV.Qtd = carrinho.ItensPedido[i].Qtd;
                    mdV.valorParcial = carrinho.ItensPedido[i].valorParcial;
                    acI.inserirItem(mdV);
                }

                carrinho.ValorTotal = 0;
                carrinho.ItensPedido.Clear();

                ViewBag.nome = Session["ClienteNome"];
                ViewBag.endereco = Session["ClienteEndereco"];
                ViewBag.data = md.DtVenda;
                ViewBag.hora = md.horaVenda;
                ViewBag.total = md.ValorTotal;
                carregaFormaPagamento();
                //carregaCliente();
                return View();
            }
        }
        
        [HttpPost]
        public ActionResult ConfCadPagamento(ModelPagamento cmPag)
        {
            acPag.inserirPagamento(cmPag);
            carregaFormaPagamento();
            //carregaCliente();
            return View();
        }
    }
}