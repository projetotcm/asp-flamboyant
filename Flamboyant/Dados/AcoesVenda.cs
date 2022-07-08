using Flamboyant.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flamboyant.Dados
{
    public class AcoesVenda
    {
        conexao con = new conexao();

        public void inserirVenda(ModelVendas cm)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tblVenda values(default, @ClienteId, @datavenda, @horaVenda , @valorFinal)", con.MyConectarBD());

            cmd.Parameters.Add("@ClienteId", MySqlDbType.VarChar).Value = cm.UsuarioID;
            cmd.Parameters.Add("@datavenda", MySqlDbType.VarChar).Value = cm.DtVenda;
            cmd.Parameters.Add("@horaVenda", MySqlDbType.VarChar).Value = cm.horaVenda;
            cmd.Parameters.Add("@valorFinal", MySqlDbType.VarChar).Value = cm.ValorTotal;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        MySqlDataReader dr;
        public void buscaIdVenda(ModelVendas vend)
        {
            MySqlCommand cmd = new MySqlCommand("SELECT codVenda FROM tblVenda ORDER BY codVenda DESC limit 1", con.MyConectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                vend.codVenda = dr[0].ToString();
            }
            con.MyDesconectarBD();
        }
    }
}