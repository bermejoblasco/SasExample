
namespace SaSApp
{
    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.Blob;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            //Storage connectionstring
            var storageAccount = CloudStorageAccount.Parse("Your Blob Storage Connection String");
            var cloudBlobClient = storageAccount.CreateCloudBlobClient();
            //GetContainer on insert blob. Create reference if no exist
            var blobContainer = cloudBlobClient.GetContainerReference("ContainerName");
            blobContainer.CreateIfNotExists();
            //Create referennce to file
            var blockBlob =  blobContainer.GetBlockBlobReference("filename.extension");
            //Create Permisions.
            var sasToken = blockBlob.Container.GetSharedAccessSignature(new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read | SharedAccessBlobPermissions.Write,
                SharedAccessStartTime = DateTime.UtcNow.AddMinutes(-15),
                SharedAccessExpiryTime = DateTime.UtcNow.AddMinutes(15)                
            });

            Console.WriteLine($"token SAS: {sasToken}");
            Console.ReadLine();
        }
    }
}
