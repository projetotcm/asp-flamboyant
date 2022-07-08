using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Flamboyant.Dados
{
    public class conexao
    {
        MySqlConnection cn = new MySqlConnection("Server=localhost; DataBase=bdFlamboyant3; User=root;pwd=1234567");
        public static string msg;

        //Abrindo a conexão
        public MySqlConnection MyConectarBD()
        {

            try
            {
                cn.Open();
            }

            catch (Exception erro)
            {
                msg = "Ocorreu um erro ao se conectar" + erro.Message;
            }
            return cn;
        }

        //fechando a conexão
        public MySqlConnection MyDesconectarBD()
        {

            try
            {
                cn.Close();
            }

            catch (Exception erro)
            {
                msg = "Ocorreu um erro ao se conectar" + erro.Message;
            }
            return cn;
        }
    }
}