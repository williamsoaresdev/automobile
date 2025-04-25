using AutomobileRentalManagementAPI.Domain.HttpRepositories;
using Azure.Storage.Blobs;

namespace AutomobileRentalManagementAPI.Infra.HttpRepositories
{
    public class BlobHttpRepository : IBlobHttpRepository
    {
        private readonly string _connectionString = "UseDevelopmentStorage=true";
        private readonly string _containerName = "images";

        public string UploadBase64FileAndReturnPublicUrl(string base64img)
        {
            string response = string.Empty;

            try
            {
                var task = UploadImageAsync(base64img);
                task.Wait();
                response = task.Result;
            }
            catch (Exception ex)
            {
                response = "https://i.pravatar.cc/150?img=33";
            }

            return response;
        }

        private async Task<string> UploadImageAsync(string base64img)
        {
            byte[] imageBytes = Convert.FromBase64String(base64img);
            string fileName = Guid.NewGuid().ToString() + ".jpg";

            var blobServiceClient = new BlobServiceClient(_connectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(_containerName);
            await containerClient.CreateIfNotExistsAsync();

            var blobClient = containerClient.GetBlobClient(fileName);

            using (var stream = new MemoryStream(imageBytes))
            {
                await blobClient.UploadAsync(stream, overwrite: true);
            }

            return $"http://127.0.0.1:10000/devstoreaccount1/{_containerName}/{fileName}";
        }
    }
}