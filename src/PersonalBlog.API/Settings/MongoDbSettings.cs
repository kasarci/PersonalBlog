namespace PersonalBlog.API.Settings;

public class MongoDbSettings
{
    public string Host { get; set; }
    public string Port { get; set; }
    public string User { get; set; }
    public string Password { get; set; }
    public string DatabaseName { get; set; }
    public string ConnectionString
    {
        get
        {
            return $"mongodb://{User}:{Password}@{Host}:{Port}";
            // return $"mongodb://{Host}:{Port}";
        }
    }
}