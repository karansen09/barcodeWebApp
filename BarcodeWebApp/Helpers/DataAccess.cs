using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace BarcodeWebApp.Helpers
{
    public class DataAccess
    {
        private static IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetParent(Directory.GetCurrentDirectory()) + "/BarcodeWebApp").AddJsonFile("appsettings.json", false).Build();

        private static string ConnectionString = configuration.GetConnectionString("Default");

        public static int DirectQuery(string str)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand(str, con);
            con.Open();
            int add = cmd.ExecuteNonQuery();
            con.Close();
            con.Dispose();
            return add;
        }

        public static System.Data.DataTable Select(string sqlstr)
        {
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(ConnectionString);
            SqlDataAdapter sda = new SqlDataAdapter(sqlstr, con);
            System.Data.DataSet ds = new DataSet();
            sda.Fill(ds);
            DataTable dt = ds.Tables[0];
            con.Close();
            con.Dispose();
            return (dt);
        }

        public static SqlCommand GetCommand()
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = con;
            return cmd;
        }

        public static string ExecureScaler(SqlCommand cmd)
        {
            string str = null;
            try
            {
                cmd.Connection.Open();
                str = Convert.ToString(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            finally
            {
                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
                cmd.Connection.Dispose();
            }

            return str;
        }


        public static string ExecuteNonQuery(SqlCommand cmd)
        {
            string str = null;
            try
            {
                cmd.Connection.Open();
                str = cmd.ExecuteNonQuery().ToString();
            }

            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
            return str;
        }

        public static DataTable ExecuteTable(SqlCommand cmd)
        {
            DataTable dt = new DataTable();
            try
            {
                //cmd.Connection.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        public static DataSet ExecuteDataSet(SqlCommand cmd)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return ds;
        }

        public static string JsonResultExecute(SqlCommand cmd)
        {
            string JSONresult;
            DataTable dt = new DataTable();
            try
            {
                //cmd.Connection.Open();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);

                DataSet ds = new DataSet();
                adp.Fill(ds);
                dt = ds.Tables[0];
                JSONresult = Newtonsoft.Json.JsonConvert.SerializeObject(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                //if (cmd.Connection.State == ConnectionState.Open)
                //    cmd.Connection.Close();
                //cmd.Connection.Dispose();
            }
            return JSONresult;
        }
    }
}
