using Azure.Identity;
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace Exercises_API.Infrastructure.Services;

public class BlobContainerService
{
    private readonly BlobServiceClient blobServiceClient;
    private readonly BlobContainerClient blobContainerClient;

    public BlobContainerService()
    {
        this.blobServiceClient = new BlobServiceClient
        (
            new Uri("https://4fitbodystorage.blob.core.windows.net/"),
            new DefaultAzureCredential()
        );

        this.blobContainerClient = this.blobServiceClient.GetBlobContainerClient("exercise-images");
    }

    public async Task UploadAsync(Stream stream, string path)
    {
        BlobClient blobClient = this.blobContainerClient.GetBlobClient(path);

        using (Stream file = stream)
        {
            await blobClient.UploadAsync(file);
        }
    }
}