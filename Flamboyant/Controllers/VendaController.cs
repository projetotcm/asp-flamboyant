using Flamboyant.Dados;
using Flamboyant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Flamboyant.Controllers
{
    public class VendaController : Controller
    {
        AcoesCardapio acCard = new AcoesCardapio();
        AcoesVenda acVendas = new AcoesVenda();
        AcoesItemCarrinho acItemV = new AcoesItemCarrinho();

        // GET: Venda
        //private Context db = new Context();

        public static string codigo;

        public ActionResult AdicionarCarrinho(int id, double pre)
        {
            ModelVendas carrinho = Session["Carrinho"] != null ? (ModelVendas)Session["Carrinho"] : new ModelVendas();
            var cardapio = acCard.GetConsultaCardapio(id);
            codigo = id.ToString();

            ModelCardapio card= new ModelCardapio();

            if (cardapio != null)
            {
                var itemPedido = new ModelItemCarrinho();
                itemPedido.ItemPedidoID = Guid.NewGuid();
                itemPedido.ProdutoID = id.ToString();
                itemPedido.Produto = cardapio[0].NomeCardapio;
                itemPedido.Qtd = 1;
                itemPedido.valorUnit = pre;

                List<ModelItemCarrinho> x = carrinho.ItensPedido.FindAll(l => l.Produto == itemPedido.Produto);

                if (x.Count != 0)
                {
                    carrinho.ItensPedido.FirstOrDefault(p => p.Produto == cardapio[0].NomeCardapio).Qtd += 1;
                    itemPedido.valorParcial = itemPedido.Qtd * itemPedido.valorUnit;
                    carrinho.ValorTotal += itemPedido.valorParcial;
                    carrinho.ItensPedido.FirstOrDefault(p => p.Produto == cardapio[0].NomeCardapio).valorParcial = carrinho.ItensPedido.FirstOrDefault(p => p.Produto == cardapio[0].NomeCardapio).Qtd * itemPedido.valorUnit;

                }

                else
                {
                    itemPedido.valorParcial = itemPedido.Qtd * itemPedido.valorUnit;
                    carrinho.ValorTotal += itemPedido.valorParcial;
                    carrinho.ItensPedido.Add(itemPedido);
                }

                /*carrinho.ValorTotal = carrinho.ItensPedido.Select(i => i.Produto).Sum(d => d.Valor);*/

                Session["Carrinho"] = carrinho;
            }

            return RedirectToAction("Carrinho");
        }

        public ActionResult Carrinho()
        {
            ModelVendas carrinho = Session["Carrinho"] != null ? (ModelVendas)Session["Carrinho"] : new ModelVendas();

            return View(carrinho);
        }

        public ActionResult ExcluirItem(Guid id)
        {
            var carrinho = Session["Carrinho"] != null ? (ModelVendas)Session["Carrinho"] : new ModelVendas();
            var itemExclusao = carrinho.ItensPedido.FirstOrDefault(i => i.ItemPedidoID == id);

            carrinho.ValorTotal -= itemExclusao.valorParcial;

            carrinho.ItensPedido.Remove(itemExclusao);

            Session["Carrinho"] = carrinho;
            return RedirectToAction("Carrinho");
        }

        public ActionResult SalvarCarrinho(ModelVendas x)
        {

            if ((Session["ClienteNome"] == null) || (Session["ClienteSenha"] == null))
            {
                return RedirectToAction("LoginCliente2", "Cliente");
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

                acVendas.inserirVenda(md);


                acVendas.buscaIdVenda(x);

                for (int i = 0; i < carrinho.ItensPedido.Count; i++)
                {

                    mdV.PedidoID = x.codVenda;
                    mdV.ProdutoID = carrinho.ItensPedido[i].ProdutoID;
                    mdV.Qtd = carrinho.ItensPedido[i].Qtd;
                    mdV.valorParcial = carrinho.ItensPedido[i].valorParcial;
                    acItemV.inserirItem(mdV);
                }

                carrinho.ValorTotal = 0;
                carrinho.ItensPedido.Clear();

                return RedirectToAction("CadPagamento");
            }
        }

        public ActionResult confVenda()
        {
            return View();
        }
    }
}