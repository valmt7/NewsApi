public interface IPGSQL
{
    Task<bool> IsRegistered(string id);
    Task Add(string id);
    Task EditLastGameId(string id, int lastGameId);
    Task<int> GetLastGameId(string id);
    
    Task<List<UserModel>> GetAllUsers(); 
}