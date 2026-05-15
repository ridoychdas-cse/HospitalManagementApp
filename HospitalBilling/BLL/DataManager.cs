using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Web;

namespace HospitalBilling.BLL
{
    public class DataManager
    {
        public static string ConnectionString()
        {
            //const string connectionString = @"Server=localhost; Database=HospitalBillingDb; User Id= sa; Password=sapassword; Trusted_Connection=False; pooling=false;";
            const string connectionString =
      @"Server=DESKTOP-SV3HLMJ\SQLEXPRESS;
      Database=HospitalBillingDb;
      Integrated Security=True;
      pooling=false;";
            return connectionString;



            //const string connectionString =
            //    "Data Source=SQL5026.site4now.net;Initial Catalog=DB_A61505_HospitalBilling;User Id=DB_A61505_HospitalBilling_admin;Password=Netsoft2020#;";
            //return connectionString;



            //return String.Format("Server=DESKTOP-25UCJ5C;Database=HospitalBillingDb;Trusted_Connection=True;");
            // return String.Format("Server=localhost;Database=HospitalBillingDb;User ID=sa;Password=sapassword;pooling=False;Trusted_Connection=False;");
        }

        public static DataTable ExecuteQuery(string ConnectionString, string query, string tableName)
        {
            using (SqlConnection myConnection = new SqlConnection(ConnectionString))
            {

                using (SqlDataAdapter myAdapter = new SqlDataAdapter(query, myConnection))
                {

                    DataSet ds = new DataSet();
                    myAdapter.Fill(ds, tableName);
                    ds.Tables[0].TableName = tableName;
                    return ds.Tables[0];
                }
            }
        }

        /// <summary>
        /// ExecuteNonQuery for Insert, Update, Delete In Database
        /// </summary>
        /// <param name="query">Command Text</param>
        /// <param name="connectionString">Sql Connection String</param>
        /// <returns>Return 0 or 1</returns>
        public static int ExecuteNonQuery(string query, string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand(query, connection);
            connection.Open();
            int rowAffected = command.ExecuteNonQuery();
            return rowAffected;
        }
        public static int ExecuteNonQuerySP(string spName, SqlParameter[] parameters, string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(spName, connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }

                    connection.Open();

                    return command.ExecuteNonQuery();
                }
            }
        }
        /// <summary>
        /// SqlDataReader for Read Data From Database
        /// </summary>
        /// <param name="query">Command Text</param>
        /// <param name="connectionString">Sql Connection String</param>
        /// <returns>Return Database Table Row Data</returns>
        public static SqlDataReader SqlDataReader(string query, string connectionString)
        {
            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand(query, connection);
            connection.Open();
            var reader = command.ExecuteReader();
            return reader;
        }

        /// <summary>
        /// DataTable for Read Data From Database
        /// </summary>
        /// <param name="query">Command Text</param>
        /// <param name="connectionString">Sql Connection String</param>
        /// <param name="tableName">Database Table Name Table Name</param>
        /// <returns></returns>
        public static DataTable DataTable(string query, string connectionString, string tableName)
        {
            var connection = new SqlConnection(connectionString);
            var dataAdapter = new SqlDataAdapter(query, connection);
            var dataSet = new DataSet();
            dataAdapter.Fill(dataSet, tableName);
            dataSet.Tables[0].TableName = tableName;
            return dataSet.Tables[0];
        }

        public static int Transaction(string connectionString, string query1, string query2)
        {
            var connection = new SqlConnection(connectionString);
            connection.Open();
            var aTransaction = connection.BeginTransaction();


            var command1 = new SqlCommand(query1, connection, aTransaction);
            command1.ExecuteNonQuery();

            command1 = new SqlCommand(query2, connection, aTransaction);
            int mstId = Convert.ToInt32(command1.ExecuteScalar());

            aTransaction.Commit();
            return mstId;
        }

        /// <summary>
        /// Get Auto Id Form Table Name 
        /// </summary>
        /// <param name="tableName">Auto Id Table Name</param>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static string AutoId(string tableName, string connectionString)
        {
            string autoId = "";
            var connection = new SqlConnection(connectionString);

            string query = "SELECT TOP(1) Id FROM " + tableName + " ORDER BY Id DESC";

            var command = new SqlCommand(query, connection);
            connection.Open();

            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                reader.Read();
                autoId = reader["Id"].ToString();
            }
            if (autoId == "")
            {
                autoId = "0001";
            }
            else
            {
                int idIncrement = Convert.ToInt32(autoId);
                idIncrement++;
                autoId = "000" + idIncrement;
            }

            return autoId;
        }

        public enum ResizeOptions
        {
            // Use fixed width & height without keeping the proportions
            ExactWidthAndHeight,

            // Use maximum width (as defined) and keeping the proportions
            MaxWidth,

            // Use maximum height (as defined) and keeping the proportions
            MaxHeight,

            // Use maximum width or height (the biggest) and keeping the proportions
            MaxWidthAndHeight
        }

        public static Bitmap DoResize(Bitmap originalImg, int widthInPixels, int heightInPixels)
        {
            try
            {
                var bitmap = new Bitmap(widthInPixels, heightInPixels);
                using (System.Drawing.Graphics graphic = Graphics.FromImage(bitmap))
                {
                    // Quality properties
                    graphic.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    graphic.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    graphic.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
                    graphic.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                    graphic.DrawImage(originalImg, 0, 0, widthInPixels, heightInPixels);
                    return bitmap;
                }
            }
            finally
            {
                if (originalImg != null)
                {
                    originalImg.Dispose();
                }
            }
        }
        public static System.Drawing.Bitmap ResizeImage(System.Drawing.Bitmap image, int width, int height, ResizeOptions resizeOptions)
        {
            float fWidth;
            float fHeight;
            float dim;
            switch (resizeOptions)
            {
                case ResizeOptions.ExactWidthAndHeight:
                    return DoResize(image, width, height);

                case ResizeOptions.MaxHeight:
                    fWidth = image.Width;
                    fHeight = image.Height;

                    if (fHeight <= height)
                        return DoResize(image, (int)fWidth, (int)fHeight);

                    dim = fWidth / fHeight;
                    width = (int)((float)(height) * dim);
                    return DoResize(image, width, height);

                case ResizeOptions.MaxWidth:
                    fWidth = image.Width;
                    fHeight = image.Height;

                    if (fWidth <= width)
                        return DoResize(image, (int)fWidth, (int)fHeight);

                    dim = fWidth / fHeight;
                    height = (int)((float)(width) / dim);
                    return DoResize(image, width, height);

                case ResizeOptions.MaxWidthAndHeight:
                    int tmpHeight = height;
                    int tmpWidth = width;
                    fWidth = image.Width;
                    fHeight = image.Height;

                    if (fWidth <= width && fHeight <= height)
                        return DoResize(image, (int)fWidth, (int)fHeight);

                    dim = fWidth / fHeight;

                    // Check if the width is ok
                    if (fWidth < width)
                        width = (int)fWidth;
                    height = (int)((float)(width) / dim);
                    // The width is too width
                    if (height > tmpHeight)
                    {
                        if (fHeight < tmpHeight)
                            height = (int)fHeight;
                        else
                            height = tmpHeight;
                        width = (int)((float)(height) * dim);
                    }
                    return DoResize(image, width, height);
                default:
                    return image;
            }
        }
        public static string ConvertBytesToString(byte[] bytes)
        {
            string output = String.Empty;
            var stream = new MemoryStream(bytes);
            stream.Position = 0;
            using (var reader = new StreamReader(stream))
            {
                output = reader.ReadToEnd();
            }
            return output;
        }
        public static byte[] ConvertStringToBytes(string input)
        {
            var stream = new MemoryStream();
            using (var writer = new StreamWriter(stream))
            {
                writer.Write(input);
                writer.Flush();
            }
            return stream.ToArray();
        }
        public static byte[] ConvertImageToByteArray(Image imageToConvert,
                                       System.Drawing.Imaging.ImageFormat formatOfImage)
        {
            byte[] ret;
            try
            {
                using (var ms = new MemoryStream())
                {
                    imageToConvert.Save(ms, formatOfImage);
                    ret = ms.ToArray();
                }
            }
            catch (Exception) { throw; }
            return ret;
        }

        public static Image ByteArrayToImage(byte[] byteArrayIn)
        {
            var fs = new FileStream(HttpContext.Current.Server.MapPath("~/img/noimage.jpg"), FileMode.Open, FileAccess.Read);
            var br = new BinaryReader(fs);
            byte[] bt = br.ReadBytes((int)fs.Length);
            if (byteArrayIn.Length == 0)
            {
                byteArrayIn = bt;
            }
            var ms = new MemoryStream(byteArrayIn);
            var returnImage = Image.FromStream(ms);
            return returnImage;
        }
        // End Image Section
    }
}