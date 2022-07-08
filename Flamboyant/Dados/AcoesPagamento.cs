using Flamboyant.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Flamboyant.Dados
{
    public class AcoesPagamento
    {
        conexao con = new conexao();

        public void inserirPagamento(ModelPagamento cmPag)//Método para inserir Pagamento
        {
            MySqlCommand cmd = new MySqlCommand("insert into itemVenda values (default, @codVenda, @CardapioId, @qtdeVendas, @valorParcial)", con.MyConectarBD());
            cmd.Parameters.Add("@codVenda", MySqlDbType.VarChar).Value = cmPag.ClienteId;
            cmd.Parameters.Add("@CardapioId", MySqlDbType.VarChar).Value = cmPag.CodEspecialidade;
            cmd.Parameters.Add("@qtdeVendas", MySqlDbType.VarChar).Value = cmPag.CodVenda;
            cmd.Parameters.Add("@valorParcial", MySqlDbType.VarChar).Value = cmPag.CoditemVenda;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public DataTable ConsultaPagamento()//Método de consulta do Pagamento
        {
            MySqlCommand cmd = new MySqlCommand("select * from tblPagamento", con.MyDesconectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable pagamento = new DataTable();
            da.Fill(pagamento);
            con.MyDesconectarBD();
            return pagamento;
        }
    }
}