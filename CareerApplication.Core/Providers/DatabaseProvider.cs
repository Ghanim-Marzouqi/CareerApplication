using Firebase.Database.Query;
using System.Text.Json;

namespace CareerApplication.Core.Providers;

public class DatabaseProvider
{
    private readonly FirebaseClient _db;

    public DatabaseProvider(FirebaseClient db)
    {
        _db = db;
    }

    public async Task<bool> Add<T>(string resource, T entity) where T : BaseEntity
    {
        if (string.IsNullOrEmpty(resource) || entity == null)
            return false;

        try
        {
            var result = await _db.Child(resource).PostAsync(JsonSerializer.Serialize(entity));

            if (string.IsNullOrEmpty(result.Key))
                return false;
            else
            {
                entity.Key = result.Key;
                return await InsertKey(resource, result.Key, entity);
            }
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<int> GenerateNewId<T>(string resource) where T : BaseEntity
    {
        var items = (await _db.Child(resource).OnceAsync<T>()).ToList();
        return (items != null && items.Count > 0) ? items.Count + 1 : 1;
    }

    public async Task<IEnumerable<T>> GetAll<T>(string resource, Func<FirebaseObject<T>, T> selector) where T : BaseEntity
    {
        return (await _db.Child(resource).OnceAsync<T>()).Select(selector).ToList();
    }

    public async Task<T?> GetById<T>(string resource, Func<T, bool> predicate, Func<FirebaseObject<T>, T> selector) where T : BaseEntity, new()
    {
        var items = (await _db.Child(resource).OnceAsync<T>()).Select(selector).ToList();
        return (items != null && items.Count > 0) ? items.FirstOrDefault(predicate) : new T();
    }

    public async Task<bool> Delete<T>(string resource, string key, Func<T, bool> predicate, Func<FirebaseObject<T>, T> selector) where T : BaseEntity, new()
    {
        try
        {
            var item = await GetById(resource, predicate, selector);

            if (item == null)
                return false;

            await _db.Child(resource).Child(key).DeleteAsync();

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> Update<T>(string resource, string key, Func<T, bool> predicate, Func<FirebaseObject<T>, T> selector, T entity) where T : BaseEntity, new()
    {
        try
        {
            var item = await GetById(resource, predicate, selector);

            if (item == null)
                return false;

            await _db
              .Child(resource)
              .Child(key)
              .PutAsync(entity);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    private async Task<bool> InsertKey<T>(string resource, string key, T entity) where T : BaseEntity
    {
        try
        {
            await _db
              .Child(resource)
              .Child(key)
              .PutAsync(entity);

            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
