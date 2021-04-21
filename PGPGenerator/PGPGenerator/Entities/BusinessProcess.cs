using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;


namespace PGPGenerator.Entities
{
    class BusinessProcess
    {
        internal DataTable ExecuteBrand(string Connection, string Query, out string ErrorMessage)
        {
            DataTable dt = new DataTable("Get");
            ErrorMessage = string.Empty;
            string ConStr = Common.DecodeFrom64(Connection);

            using (SqlConnection con = new SqlConnection(ConStr))
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter();
                    SqlCommand sqlcom = new SqlCommand();
                    sqlcom.CommandType = CommandType.Text;
                    sqlcom.CommandTimeout = 0;
                    sqlcom.CommandText = Query;

                    sqlcom.Connection = con;
                    da.ReturnProviderSpecificTypes = true;
                    da.SelectCommand = sqlcom;
                    da.Fill(dt);
                }
                catch (Exception ex)
                {
                    ErrorMessage = ex.Message;
                }
                finally
                {
                    con.Close();
                }
                return dt;
            }

        }

        internal void UpdateBrand(string Connection, string Query, out string ErrorMessage)
        {
            SqlCommand comm = null;
            ErrorMessage = string.Empty;

            SqlTransaction sqltran = null;
            string ConStr = Common.DecodeFrom64(Connection);

            using (SqlConnection con = new SqlConnection(ConStr))
            {
                try
                {
                    con.Open();
                    sqltran = con.BeginTransaction();
                    comm = new SqlCommand(Query, con);
                    comm.Transaction = sqltran;
                    comm.CommandType = CommandType.Text;
                    comm.CommandTimeout = 18000;
                    comm.ExecuteNonQuery();

                    sqltran.Commit();



                }
                catch (Exception ex)
                {
                    sqltran.Rollback();
                    ErrorMessage = ex.Message;
                }
                finally
                {
                    comm = null;
                    con.Close();
                }
            }


        }

    }

}
