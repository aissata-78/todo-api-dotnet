using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoApi.Models;
using TodoApi.Services;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // Tous les endpoints nécessitent une authentification
public class TodosController : ControllerBase
{
    private readonly TodoService _todoService;

    public TodosController(TodoService todoService)
    {
        _todoService = todoService;
    }

    /// <summary>Récupérer tous les todos — accessible à tous les utilisateurs connectés</summary>
    [HttpGet]
    public async Task<ActionResult<List<Todo>>> GetAll() =>
        Ok(await _todoService.GetAllAsync());

    /// <summary>Récupérer un todo par ID — accessible à tous les utilisateurs connectés</summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<Todo>> GetById(string id)
    {
        var todo = await _todoService.GetByIdAsync(id);
        if (todo is null)
            return NotFound(new { message = $"Todo avec l'id '{id}' introuvable." });
        return Ok(todo);
    }

    /// <summary>Créer un todo — réservé aux admins</summary>
    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Create([FromBody] Todo newTodo)
    {
        newTodo.CreatedAt = DateTime.UtcNow;
        await _todoService.CreateAsync(newTodo);
        return CreatedAtAction(nameof(GetById), new { id = newTodo.Id }, newTodo);
    }

    /// <summary>Modifier un todo — réservé aux admins</summary>
    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Update(string id, [FromBody] Todo updatedTodo)
    {
        var todo = await _todoService.GetByIdAsync(id);
        if (todo is null)
            return NotFound(new { message = $"Todo avec l'id '{id}' introuvable." });

        updatedTodo.Id = id;
        await _todoService.UpdateAsync(id, updatedTodo);
        return NoContent();
    }

    /// <summary>Supprimer un todo — réservé aux admins</summary>
    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(string id)
    {
        var todo = await _todoService.GetByIdAsync(id);
        if (todo is null)
            return NotFound(new { message = $"Todo avec l'id '{id}' introuvable." });

        await _todoService.DeleteAsync(id);
        return NoContent();
    }
}
