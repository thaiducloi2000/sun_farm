public interface ISave
{
    public void Save<T>(string key, T Data);
    public T Load<T>(string key);
    public bool KeyExists(string key);
}
