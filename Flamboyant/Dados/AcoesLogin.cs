using Flamboyant.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flamboyant.Dados
{
    public class AcoesLogin
    {
        //estabelendo conexão com o banco
        conexao con = new conexao();

        public void TestarUsuario(ModelLogin user)
        {
            //está fazendo uma busca do que está cadastrado no banco
            MySqlCommand cmd = new MySqlCommand("select * from tblLogin where usuario = @usuario and senha = @senha", con.MyConectarBD());
            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = user.usuario;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = user.senha;

            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    //se for true ele vai atribuir o usuario e senha 
                    user.usuario = Convert.ToString(leitor["usuario"]);
                    user.senha = Convert.ToString(leitor["senha"]);
                    user.tipo = Convert.ToString(leitor["tipo"]);
                }
            }
            else
            {
                //se for false não vai atribuir essas variaveis ficara vazia
                user.usuario = null;
                user.senha = null;
                user.tipo = null;
            }

            con.MyDesconectarBD();
        }

        //Metodo de Inserir o Login
        public void inserirLogin(ModelLogin cmLogin)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tblLogin values (@usuario, @senha, @tipo)", con.MyConectarBD());
            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = cmLogin.usuario;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = cmLogin.senha;
            cmd.Parameters.Add("@tipo", MySqlDbType.VarChar).Value = cmLogin.tipo;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }
    }
}