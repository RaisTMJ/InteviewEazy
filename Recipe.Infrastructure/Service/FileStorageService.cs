using Azure.Storage.Blobs;
using Recipe.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipe.Infrastructure.Service
{
    public class FileStorageService : IFileStorageService
    {

        private readonly BlobServiceClient _blobServiceClient;


        public FileStorageService(BlobServiceClient blobServiceClient) {


            _blobServiceClient = blobServiceClient;
        }


        Task IFileStorageService.DeleteFileAsync(string filePath)
        {


            throw new NotImplementedException();
        }

        async Task<string> IFileStorageService.SaveFileAsync(byte[] content, string containerName, string fileName)
        {

            var containerClient = _blobServiceClient.GetBlobContainerClient("profile-images");
            await containerClient.CreateIfNotExistsAsync();

            var blobClient = containerClient.GetBlobClient(fileName);

            using (var stream = new MemoryStream(content))
            {
               await blobClient.UploadAsync(stream, overwrite: true);
            
            }
            return blobClient.Uri.ToString();
        }
    }
}
