using Flamboyant.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Flamboyant.Dados
{
    public class AcoesCardapio
    {
        conexao con = new conexao();

        //Metodo de Inserir o Cardapio
        public void inserirCardapio(ModelCardapio cmCardapio)
        {
            MySqlCommand cmd = new MySqlCommand("insert into tblCardapio values (default, @NomeCardapio, @DesCardapio, @VlCardapio, @imagemCardapio)", con.MyConectarBD());
            cmd.Parameters.Add("@NomeCardapio", MySqlDbType.VarChar).Value = cmCardapio.NomeCardapio;
            cmd.Parameters.Add("@DesCardapio", MySqlDbType.VarChar).Value = cmCardapio.DesCardapio;
            cmd.Parameters.Add("@VlCardapio", MySqlDbType.VarChar).Value = cmCardapio.VlCardapio;
            cmd.Parameters.Add("@imagemCardapio", MySqlDbType.VarChar).Value = cmCardapio.FotoCardapio;
            cmd.ExecuteNonQuery();
            con.MyDesconectarBD();
        }



        //Metodo de Listar cardapio
        public List<ModelCardapio> GetCardapio()
        {
            List<ModelCardapio> CardapioList = new List<ModelCardapio>();

            MySqlCommand cmd = new MySqlCommand("select * from tblCardapio", con.MyConectarBD());
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                CardapioList.Add(
                    new ModelCardapio
                    {
                        CardapioId = Convert.ToString(dr["CardapioId"]),
                        NomeCardapio = Convert.ToString(dr["NomeCardapio"]),
                        DesCardapio = Convert.ToString(dr["DesCardapio"]),
                        VlCardapio = Convert.ToString(dr["VlCardapio"]),
                        FotoCardapio = Convert.ToString(dr["imagemCardapio"]),

                    });
            }
            return CardapioList;
        }


        //Metodo de Consultar Cardapio
        public List<ModelCardapio> GetConsultaCardapio(int id)
        {
            List<ModelCardapio> CardapioList = new List<ModelCardapio>();

            MySqlCommand cmd = new MySqlCommand("select * from tblCardapio where CardapioId=@id", con.MyConectarBD());
            cmd.Parameters.AddWithValue("@id", id);
            MySqlDataAdapter sd = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            sd.Fill(dt);
            con.MyDesconectarBD();

            foreach (DataRow dr in dt.Rows)
            {
                 CardapioList.Add(
                    new ModelCardapio
                    {
                        CardapioId = Convert.ToString(dr["CardapioId"]),
                        NomeCardapio = Convert.ToString(dr["NomeCardapio"]),
                        DesCardapio = Convert.ToString(dr["DesCardapio"]),
                        VlCardapio = Convert.ToString(dr["VlCardapio"]),
                        FotoCardapio = Convert.ToString(dr["imagemCardapio"]),
                    });
            }
            return CardapioList;
        }


        //Metodo de deletar cardapio
        public bool DeleteCardapio(int id)
        {
            MySqlCommand cmd = new MySqlCommand("delete from tblCardapio where CardapioId=@id", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@id", id);


            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }


        //Metodo de atualizar cardapio
        public bool atualizaCardapio(ModelCardapio cmCardapio)
        {
            MySqlCommand cmd = new MySqlCommand("update tblCardapio set NomeCardapio=@NomeCardapio, DesCardapio=@DesCardapio, VlCardapio=@VlCardapio, imagemCardapio=@imagemCardapio where CardapioId=@CardapioId", con.MyConectarBD());

            cmd.Parameters.AddWithValue("@CardapioId", cmCardapio.CardapioId);
            cmd.Parameters.AddWithValue("@NomeCardapio", cmCardapio.NomeCardapio);
            cmd.Parameters.AddWithValue("@DesCardapio", cmCardapio.DesCardapio);
            cmd.Parameters.AddWithValue("@VlCardapio", cmCardapio.VlCardapio);
            cmd.Parameters.AddWithValue("@imagemCardapio", cmCardapio.FotoCardapio);

            int i = cmd.ExecuteNonQuery();
            con.MyDesconectarBD();

            if (i >= 1)
                return true;
            else
                return false;
        }
    }
}