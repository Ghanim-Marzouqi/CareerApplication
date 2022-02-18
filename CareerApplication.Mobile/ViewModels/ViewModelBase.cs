namespace CareerApplication.Mobile.ViewModels;

public class ViewModelBase : INotifyPropertyChanged
{
    public INavigation Navigation { get => Application.Current.MainPage.Navigation; }

    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null) =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public bool IsValidEmail(string email)
    {
        Regex regex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
        return regex.IsMatch(email);
    }

    public bool IsValidPhone(string email)
    {
        Regex regex = new Regex(@"^[79]\d{7}$", RegexOptions.CultureInvariant | RegexOptions.Singleline);
        return regex.IsMatch(email);
    }

    public void StoreData<T>(string key, T value) =>
        Preferences.Set(key, JsonSerializer.Serialize(value));

    public T GetData<T>(string key) =>
        JsonSerializer.Deserialize<T>(Preferences.Get(key, null));

    public void RemoveData(string key) =>
        Preferences.Remove(key);
}
