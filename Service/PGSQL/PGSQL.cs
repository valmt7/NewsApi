namespace NewsApi.Service.PGSQL;
using Npgsql;
public class PGSQL : IPGSQL
{
    public async Task<bool> IsRegistered(string id)
    {
       
        var sql = "SELECT COUNT(1) FROM \"users\" WHERE \"id\" = @id";

        
        using var con = new NpgsqlConnection(Constant.Connect);
        using var com = new NpgsqlCommand(sql, con);

        com.Parameters.AddWithValue("id", id);

       
        await con.OpenAsync();
    
        
        var count = (long)await com.ExecuteScalarAsync();

        return count > 0;
    }
    
    public async Task Add(string id) 
    {
        var sql = "insert into \"users\"(\"id\", \"lastgame\") values(@id, @lastgame)";
    
        
        using var con = new NpgsqlConnection(Constant.Connect);
        using var com = new NpgsqlCommand(sql, con);

        com.Parameters.AddWithValue("id", id);
        com.Parameters.AddWithValue("lastgame", -1);

        await con.OpenAsync(); 
        await com.ExecuteNonQueryAsync(); 
    }
    public async Task EditLastGameId(string id, int lastGameId)
    {
        
        var sql = "UPDATE \"users\" SET \"lastgame\" = @lastgame WHERE \"id\" = @id";

        using var con = new NpgsqlConnection(Constant.Connect);
        using var com = new NpgsqlCommand(sql, con);

       
        com.Parameters.AddWithValue("id", id);
        com.Parameters.AddWithValue("lastgame", lastGameId);

        
        await con.OpenAsync();
        await com.ExecuteNonQueryAsync();
    }
    public async Task<int> GetLastGameId(string id)
    {
        
        var sql = "SELECT \"lastgame\" FROM \"users\" WHERE \"id\" = @id";

        using var con = new NpgsqlConnection(Constant.Connect);
        using var com = new NpgsqlCommand(sql, con);

        com.Parameters.AddWithValue("id", id);

        await con.OpenAsync();
        var result = await com.ExecuteScalarAsync();

        
        if (result != null && result != DBNull.Value)
        {
            return Convert.ToInt32(result);
        }


        return -1;
    }
    public async Task<List<UserModel>> GetAllUsers()
    {
        var users = new List<UserModel>();
        var sql = "SELECT \"id\", \"lastgame\" FROM \"users\"";

        using var con = new NpgsqlConnection(Constant.Connect);
        using var com = new NpgsqlCommand(sql, con);

        await con.OpenAsync();
    
       
        using var reader = await com.ExecuteReaderAsync();

        
        while (await reader.ReadAsync())
        {
            var user = new UserModel
            {
                
                Id = reader.GetString(0), 
                LastGameId = reader.GetInt32(1)
            };
        
            users.Add(user);
        }

        return users;
    }
}
