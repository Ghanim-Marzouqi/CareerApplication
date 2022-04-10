using Firebase.Storage;

namespace CareerApplication.Core.Providers;

public class StorageProvider
{
    private readonly string _storageBucket;

    public StorageProvider(string storageBucket)
    {
        _storageBucket = storageBucket;
    }

    public async Task<string> UploadFileAsync(string resource, Models.File file)
    {
        if (string.IsNullOrEmpty(resource) || file.FileStream == null)
        {
            return string.Empty;
        }
        else
        {
            try
            {
                var uploadTask = new FirebaseStorage(_storageBucket)
                    .Child(resource)
                    .Child(file.FileName);

                var url = await uploadTask.PutAsync(file.FileStream);

                return url;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}
