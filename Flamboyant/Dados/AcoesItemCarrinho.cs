using Flamboyant.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flamboyant.Dados
{
    public class AcoesItemCarrinho
    {
        conexao con = new conexao();

        public void inserirItem(ModelItemCarrinho cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into itemVenda values(default, @codVenda, @CardapioId, @qtdeVendas , @valorParcial)", con.MyConectarBD());

            cmd.Parameters.Add("@codVenda", MySqlDbType.VarChar).Value = cm.PedidoID;
            cmd.Parameters.Add("@CardapioId", MySqlDbType.VarChar).Value = cm.ProdutoID;
            cmd.Parameters.Add("@qtdeVendas", MySqlDbType.VarChar).Value = cm.Qtd;
            cmd.Parameters.Add("@valorParcial", MySqlDbType.VarChar).Value = cm.valorParcial;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }
    }
}