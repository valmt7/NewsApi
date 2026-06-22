namespace NewsApi.Service.PGSQL;

public class Constant
{
    // Делаем свойство статическим, чтобы обращаться к нему Constant.Connect
    public static string Connect { get; private set; }

    // Как только ты вызовешь new Constant(config) где-то при старте приложения, 
    // строка соберется и запишется в статику.
    public Constant(IConfiguration config)
    {
        var host = config["SQL_Host"];
        var username = config["SQL_Username"];
        var password = config["SQL_Password"];
        var database = config["SQL_Database"];
        
        Connect = $"Host={host}; Username={username}; Password={password}; Database={database}";
    }
}