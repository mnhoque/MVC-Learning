using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MVCWebSite.DAL
{
    class DB
    {
        public static string conStr = "server=(local)\\sqlexpress;database=NazmulDB;Trusted_Connection=Yes";

        //insert, update, delete
        public static int ExecuteCommand(string cmdStr)
        {
            SqlConnection con = new SqlConnection(conStr);


            SqlCommand cmd = new SqlCommand(cmdStr, con);
            int numberOfRowsEffected = -1;//error
            try
            {
                con.Open();
                numberOfRowsEffected = cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"There is a problem, executing the command:  {cmdStr}\nTechnical Error Message: {ex.Message}");

            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }

            return numberOfRowsEffected;


        }


        public static DataTable Select(string cmdStr)
        {

            SqlDataAdapter da = new SqlDataAdapter(cmdStr, conStr);

            DataTable dt = new DataTable();//in-memory repository

            try
            {
                da.Fill(dt);//<--------------connection oriented
                return dt;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There is a problem, executing the select command:  {cmdStr}\nTechnical Error Message: {ex.Message}");
            }

            return null;//signifies an error


        }

    }
}