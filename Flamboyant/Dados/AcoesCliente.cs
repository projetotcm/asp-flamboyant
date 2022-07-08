using Flamboyant.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Flamboyant.Dados
{
    public class AcoesCliente
    {
        //estabelendo conexão com o banco
        conexao con = new conexao();


        public void TestarCliente(ModelCliente user)
        {
            //está fazendo uma busca do que está cadastrado no banco
            MySqlCommand cmd = new MySqlCommand("select * from tblCliente where usuario = @usuario and senha = @senha", con.MyConectarBD());
            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = user.ClienteNome;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = user.senha;

            MySqlDataReader leitor;

            leitor = cmd.ExecuteReader();

            if (leitor.HasRows)
            {
                while (leitor.Read())
                {
                    //se for true ele vai atribuir o usuario e senha 
                    user.ClienteNome = Convert.ToString(leitor["ClienteNome"]);
                    user.usuario = Convert.ToString(leitor["usuario"]);
                    user.senha = Convert.ToString(leitor["senha"]);
                    user.ClienteId = Convert.ToString(leitor["ClienteId"]);
                    user.ClienteEndereco = Convert.ToString(leitor["ClienteEndereco"]);
                    
                }
            }
            else
            {
                //se for false não vai atribuir essas variaveis ficara vazia
                user.ClienteNome = null;
                user.senha = null;
                
            }

            con.MyDesconectarBD();
        }

        //Metodo de Inserir o Cliente
        public void inserirCliente(ModelCliente cmCliente)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tblCliente values (default, @ClienteNome, @ClienteTel, @ClienteCep, @ClienteEmail, @ClienteEndereco, @usuario, @senha)", con.MyConectarBD());
            cmd.Parameters.Add("@ClienteNome", MySqlDbType.VarChar).Value = cmCliente.ClienteNome;
            cmd.Parameters.Add("@ClienteTel", MySqlDbType.VarChar).Value = cmCliente.ClienteTel;
            cmd.Parameters.Add("@ClienteCep", MySqlDbType.VarChar).Value = cmCliente.ClienteCep;
            cmd.Parameters.Add("@ClienteEmail", MySqlDbType.VarChar).Value = cmCliente.ClienteEmail;
            cmd.Parameters.Add("@ClienteEndereco", MySqlDbType.VarChar).Value = cmCliente.ClienteEndereco;
            cmd.Parameters.Add("@usuario", MySqlDbType.VarChar).Value = cmCliente.usuario;
            cmd.Parameters.Add("@senha", MySqlDbType.VarChar).Value = cmCliente.senha;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }

        //Metodo de Listar cliente

        public List<ModelCliente> GetCliente()
        {
            List<ModelCliente> ClienteList = new List<ModelCliente>();

            MySqlCommand cmd = new MySqlCommand("select * from tblCliente", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                ClienteList.Add(
                    new ModelCliente
                    {
                        ClienteId = Convert.ToString(dr["ClienteId"]),
                        ClienteNome = Convert.ToString(dr["ClienteNome"]),
                        ClienteTel = Convert.ToString(dr["ClienteTel"]),
                        ClienteCep = Convert.ToString(dr["ClienteCep"]),
                        ClienteEmail = Convert.ToString(dr["ClienteEmail"]),
                        ClienteEndereco = Convert.ToString(dr["ClienteEndereco"]),
                        usuario = Convert.ToString(dr["usuario"]),
                        senha = Convert.ToString(dr["senha"]),
                    });
            }
            return ClienteList;
        }


        //Metodo de Consultar Cliente
        public DataTable ConsultaCliente()
        {
            MySqlCommand cmd = new MySqlCommand("select * from tblCliente", con.MyDesconectarBD());
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable cli = new DataTable();
            da.Fill(cli);
            con.MyDesconectarBD();
            return cli;
        }


        //Metodo de deletar cliente
        public bool DeleteCliente(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tblCliente where ClienteId=@id", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@id", id);

            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }


        //Metodo de atualizar cliente
        public bool atualizaCliente(ModelCliente cm)
        {
            MySqlCommand cmd = new MySqlCommand("update tblCliente set ClienteNome=@ClienteNome, ClienteTel=@ClienteTel, ClienteCep=@ClienteCep, ClienteEmail=@ClienteEmail, ClienteEndereco=@ClienteEndereco, usuario=@usuario, senha=@senha where ClienteId=@ClienteId", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@ClienteId", cm.ClienteId);
            cmd.Parameters.AddWithValue("@ClienteNome", cm.ClienteNome);
            cmd.Parameters.AddWithValue("@ClienteTel", cm.ClienteTel);
            cmd.Parameters.AddWithValue("@ClienteCep", cm.ClienteCep);
            cmd.Parameters.AddWithValue("@ClienteEmail", cm.ClienteEmail);
            cmd.Parameters.AddWithValue("@ClienteEndereco", cm.ClienteEndereco);
            cmd.Parameters.AddWithValue("@usuario", cm.usuario);
            cmd.Parameters.AddWithValue("@Senha", cm.senha);

            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}