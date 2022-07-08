using Flamboyant.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Flamboyant.Dados
{
    public class AcoesFormaPagamento
    {
        conexao con = new conexao();

        public void inserirFormaPag(ModelFormaPagamento cmFormaPag)//Método para inserir especialidade
        {
            MySqlCommand cmd = new MySqlCommand("insert into tblFormaPagamento values (default, @Especialidade)", con.MyConectarBD());
            cmd.Parameters.Add("@Especialidade", MySqlDbType.VarChar).Value = cmFormaPag.Especialidade;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        public DataTable ConsultaFormaPagamento()//Método de consulta da especialidade
        {
            MySqlCommand cmd = new MySqlCommand("select * from tblFormaPagamento", con.MyDesconectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable FormaPag = new DataTable();
            da.Fill(FormaPag);
            con.MyDesconectarBD();
            return FormaPag;
        }
    }
}