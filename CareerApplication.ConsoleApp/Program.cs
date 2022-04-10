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

    var id1 = await dbProvider.GenerateNewIdAsync<SectorEntity>(SectorEntity.Node);

    await dbProvider.AddAsync(SectorEntity.Node, new SectorEntity
    {
        Id = id1,
        Name = "Information Technology",
        CreatedBy = "Amani"
    });

    var id2 = await dbProvider.GenerateNewIdAsync<SectorEntity>(SectorEntity.Node);

    await dbProvider.AddAsync(SectorEntity.Node, new SectorEntity
    {
        Id = id2,
        Name = "Finance",
        CreatedBy = "Amani"
    });

    var id3 = await dbProvider.GenerateNewIdAsync<SectorEntity>(SectorEntity.Node);

    await dbProvider.AddAsync(SectorEntity.Node, new SectorEntity
    {
        Id = id3,
        Name = "Human Resources",
        CreatedBy = "Amani"
    });
}

await InsertMasterData();
Console.WriteLine("Data Inserted");