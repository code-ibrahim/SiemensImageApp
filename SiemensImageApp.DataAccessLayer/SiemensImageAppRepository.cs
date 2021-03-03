using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using System.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Linq;

namespace SiemensImageApp.DataAccessLayer
{
    public class SiemensImageAppRepository
    {
        private SQLiteConnectionStringBuilder connectionStringBuilder;
        public SiemensImageAppRepository()
        {
            connectionStringBuilder = new SQLiteConnectionStringBuilder();
            connectionStringBuilder.DataSource = GetConnectionString();
        }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder()
                      .SetBasePath(Directory.GetCurrentDirectory())
                      .AddJsonFile("appsettings.json");
            var config = builder.Build();
            var connectionString = config.GetConnectionString("DBConnectionString");
            return connectionString;
        }

        public bool TestConnection()
        {
            
            var conTest = new SQLiteConnection(connectionStringBuilder.ConnectionString);
            bool status = false;
            try
            {
                conTest.Open();
                status = true;
            }
            catch
            {
                status = false;
            }
            finally
            {
                conTest.Close();
            }
            return status;
        }

        public string GetAllImages()
        {
            string data = "";
            try
            {
                DataTable dtObj = new DataTable();
                var conObj = new SQLiteConnection(connectionStringBuilder.ConnectionString);
                SQLiteCommand cmdObj = new SQLiteCommand("select * from tblImages;", conObj);
                SQLiteDataAdapter dAdapter = new SQLiteDataAdapter(cmdObj);
                dAdapter.Fill(dtObj);

                foreach (DataRow i in dtObj.Rows)
                {
                    data = data + i["iamgeName"] + "\n";
                }

            }
            catch (Exception ex)
            {
                data = "";
            }
            finally
            {
                //conTest.Close();
            }
            return data;
        }
                
        public bool InsertImage(string name, DateTime postedTime, byte[] bytes) 
        {
            bool status = false;
            try
            {
                int returnvalue = 0;

                //var conObj = new SQLiteConnection(connectionStringBuilder.ConnectionString);
                using (var conObj = new SQLiteConnection(connectionStringBuilder.ConnectionString)) 
                {
                    conObj.Open();
                    SQLiteCommand cmdObj = new SQLiteCommand("INSERT INTO tblImages(iamgeName, postedTime ,ImageData ) VALUES (@name,@postedTime,@bytes);", conObj);
                    cmdObj.Parameters.AddWithValue("@name", name);
                    cmdObj.Parameters.AddWithValue("@postedTime", postedTime);
                    cmdObj.Parameters.AddWithValue("@bytes", bytes);
                    returnvalue = cmdObj.ExecuteNonQuery();

                }
                if (returnvalue != -1) 
                {
                    status = true;
                }

            }
            catch (Exception ex)
            {
                status = false;
            }
            return status;
        }

        public string GetAllImagesASC()
        {
            string data = "";
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                DataTable dtObj = new DataTable();
                var conObj = new SQLiteConnection(connectionStringBuilder.ConnectionString);
                SQLiteCommand cmdObj = new SQLiteCommand("select * from tblImages;", conObj);
                SQLiteDataAdapter dAdapter = new SQLiteDataAdapter(cmdObj);
                dAdapter.Fill(dtObj);

                foreach (DataRow i in dtObj.Rows)
                {
                    //data = data + i["iamgeName"] + "\n";
                    dic.Add(i["iamgeName"].ToString(), i["postedTime"].ToString());

                }
                var itemsDic = from pair in dic
                            orderby pair.Value ascending
                            select pair;

                foreach (var item in itemsDic)
                {
                    data = data + item.Key + " " + item.Value + "\n";
                }
            }
            catch (Exception ex)
            {
                data = "";
            }
            finally
            {
                //conTest.Close();
            }
            return data;
        }

        public string GetAllImagesDES()
        {
            string data = "";
            try
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                DataTable dtObj = new DataTable();
                var conObj = new SQLiteConnection(connectionStringBuilder.ConnectionString);
                SQLiteCommand cmdObj = new SQLiteCommand("select * from tblImages;", conObj);
                SQLiteDataAdapter dAdapter = new SQLiteDataAdapter(cmdObj);
                dAdapter.Fill(dtObj);

                foreach (DataRow i in dtObj.Rows)
                {
                    //data = data + i["iamgeName"] + "\n";
                    dic.Add(i["iamgeName"].ToString(), i["postedTime"].ToString());

                }
                var itemsDic = from pair in dic
                            orderby pair.Value descending
                            select pair;
                foreach (var item in itemsDic)
                {
                    data = data + item.Key + " " + item.Value + "\n";
                }
            }
            catch (Exception ex)
            {
                data = "";
            }
            finally
            {
                //conTest.Close();
            }
            return data;
        }

    }
}
