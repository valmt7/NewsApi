using Microsoft.AspNetCore.Mvc;
using NewsApi.Service.PGSQL;
using System.Threading.Tasks;

namespace NewsApi.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase 
{
    private readonly IPGSQL _db;

    public UserController(IPGSQL db)
    {
        _db = db;
    }

    
    [HttpPost("add")]
    public async Task<IActionResult> Add(string id)
    {
        await _db.Add(id); 
        return Ok(new { Message = "Користувача успішно додано" }); 
    }

   
    [HttpPut("editgame")]
    public async Task<IActionResult> EditGame(string id, int lastGameId)
    {
        await _db.EditLastGameId(id, lastGameId); 
        return Ok(new { Message = "Останню гру успішно оновлено" }); 
    }

    
    [HttpGet("isreg")]
    public async Task<IActionResult> IsRegistered(string id)
    {
        bool isReg = await _db.IsRegistered(id); 
        return Ok(isReg); 
    }
    [HttpGet("get")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _db.GetAllUsers();
    
        
        if (users == null || users.Count == 0)
        {
            return NotFound(new { Message = "У базі ще немає користувачів." });
        }

        
        return Ok(users);
    }
}