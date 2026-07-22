using Microsoft.AspNetCore.Mvc;
using UsersApi.Models;

namespace UsersApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private static List<User> users = new()
    {
        new User { Id = 1, Name = "Alice", Email = "alice@test.com" },
        new User { Id = 2, Name = "Bob", Email = "bob@test.com" }
    };

    // GET: api/users
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(users);
    }

    // GET: api/users/1
    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = users.FirstOrDefault(x => x.Id == id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    // POST: api/users
    [HttpPost]
    public IActionResult Create(User user)
    {
        user.Id = users.Max(x => x.Id) + 1;

        users.Add(user);

        return CreatedAtAction(
            nameof(GetById),
            new { id = user.Id },
            user);
    }

    // PUT: api/users/1
    [HttpPut("{id}")]
    public IActionResult Update(int id, User updatedUser)
    {
        var user = users.FirstOrDefault(x => x.Id == id);

        if (user == null)
            return NotFound();

        user.Name = updatedUser.Name;
        user.Email = updatedUser.Email;

        return NoContent();
    }

    // DELETE: api/users/1
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var user = users.FirstOrDefault(x => x.Id == id);

        if (user == null)
            return NotFound();

        users.Remove(user);

        return NoContent();
    }
}