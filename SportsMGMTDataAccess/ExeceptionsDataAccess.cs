
namespace SportsMGMTDataAccess
{
    using Interfaces.IDataAccess;
    using Microsoft.Azure.Storage;
    using Microsoft.Azure.Storage.Auth;
    using Microsoft.Azure.Storage.Blob;
    using Microsoft.WindowsAzure.Storage.Blob;
    using SportsMGMTCommon;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.IO;
    using CloudBlobClient = Microsoft.Azure.Storage.Blob.CloudBlobClient;
    using CloudBlobContainer = Microsoft.Azure.Storage.Blob.CloudBlobContainer;
    using CloudBlockBlob = Microsoft.Azure.Storage.Blob.CloudBlockBlob;

    //Creates the connection to store dev exceptions
    public class ExeceptionDataAccess:IExceptions
        {
        public string Connection = AppSettings.Default.ConnectionString;
        //Make a method to store exceptions into the database
        public bool StoreExceptions(Exception ex)
            {
                try
                {
                    //store into the database using my sp_LogException stored procedure
                    using (SqlConnection con = new SqlConnection(Connection))
                    {
                        using (SqlCommand command = new SqlCommand("sp_AddExceptions", con))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.CommandTimeout = 10;
                        //if exception properties do not equal null store them otherwise leave as null
                        if (ex.Message != null)
                        {
                            command.Parameters.AddWithValue("@message", ex.Message);
                        }
                            command.Parameters.AddWithValue("@date", DateTime.Now);
                            con.Open();
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception exe)
                {
                    WriteToAzureBlob(exe);
                    return false;
                }
            return true;
            }
        //writes exceptions to azure cloud blob creating the file in my documents folder of any user
        //need to set in windows cmd setx CONNECT_STR "<yourconnectionstring>" which is provided by admin to save value globally on local machines windows instructions
        public  void WriteToAzureBlob(Exception exe)
        {
            //set that cmd connection string env variable to string named the same

            //Try parse
            CloudStorageAccount storageAccount;
            if (CloudStorageAccount.TryParse(AppSettings.Default.blob, out storageAccount))
            {
                //write exception to cloud storage if successfull terniary operator
               StoreInContainer(storageAccount,exe.Message);
            }
            else
            {
            }


        }
        public async void FileWriting(string message,CloudBlobContainer container)
        {
            //write exception to cloud storage
            string localPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string localFileName = "Exceptions" + Guid.NewGuid().ToString() + ".txt";
            string sourceFile = Path.Combine(localPath, localFileName);
            File.WriteAllText(sourceFile, DateTime.Now.ToString() + " " + message);
            //write to cloud
            CloudBlockBlob cloudBlockBlob = container.GetBlockBlobReference(localFileName);
            await cloudBlockBlob.UploadFromFileAsync(sourceFile);

        }
        public  void StoreInContainer(CloudStorageAccount account,string message)
        {
            //Create the CloudBlobClient that represents the
            // Blob storage endpoint for the storage account

            // Create a container called 'quickstartblobs' and 
            // append a GUID value to it to make the name unique.
            Uri uri = new Uri("https://sportsappexceptions.blob.core.windows.net/sportsexceptions");
            CloudBlobContainer cloudBlobContainer = new CloudBlobContainer(uri);
                //Write to the new cloud container
                FileWriting(message,cloudBlobContainer);
        }
    }
    }


