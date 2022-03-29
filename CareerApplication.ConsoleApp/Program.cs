// Import firebase settings
var firebaseSettings = new FirebaseSettings();
var firebaseApiKey = new FirebaseConfig(firebaseSettings.ApiKey);

// Initialize providers
var authProvider = new AuthProvider(new FirebaseAuthProvider(firebaseApiKey));
var dbProvider = new DatabaseProvider(new FirebaseClient(firebaseSettings.RealtimeDatabaseUrl));



// Insert master data
async Task InsertMasterData()
{
    if (dbProvider == null)
        return;

    await dbProvider.Add(SectorEntity.Node, new SectorEntity
    {
        Id = Guid.NewGuid().ToString(),
        Name = "Information Technology",
        CreatedBy = "Amani"
    });

    await dbProvider.Add(SectorEntity.Node, new SectorEntity
    {
        Id = Guid.NewGuid().ToString(),
        Name = "Finance",
        CreatedBy = "Amani"
    });

    await dbProvider.Add(SectorEntity.Node, new SectorEntity
    {
        Id = Guid.NewGuid().ToString(),
        Name = "Human Resources",
        CreatedBy = "Amani"
    });
}

await InsertMasterData();
Console.WriteLine("Data Inserted");