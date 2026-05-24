using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolinAPI.Data;
using SolinAPI.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SolinAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TutorController : ControllerBase
{
    private readonly ApplicationContext _context;

    public TutorController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Lista tutores", Description = "Retorna a lista completa de tutores cadastrados.")]
    [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<TutorEntity>))]
    public async Task<IActionResult> Get()
    {
        var result = await _context.Tutor.ToListAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Busca tutor por ID", Description = "Retorna um tutor pelo seu ID.")]
    [SwaggerResponse(200, "Tutor encontrado", typeof(TutorEntity))]
    [SwaggerResponse(404, "Tutor não encontrado")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _context.Tutor.FindAsync(id);
        if (result == null)
            return NotFound(new { mensagem = "Tutor não encontrado." });
        return Ok(result);
    }

    [HttpGet("{id}/pets")]
    [SwaggerOperation(Summary = "Lista pets do tutor", Description = "Retorna todos os pets de um tutor pelo ID do tutor.")]
    [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<PetEntity>))]
    [SwaggerResponse(404, "Tutor não encontrado")]
    public async Task<IActionResult> GetPets(int id)
    {
        var tutorExists = await _context.Tutor.FindAsync(id);
        if (tutorExists == null)
            return NotFound(new { mensagem = "Tutor não encontrado." });

        var pets = await _context.Pet.Where(p => p.IdTutor == id).ToListAsync();
        return Ok(pets);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cria tutor", Description = "Cadastra um novo tutor no sistema.")]
    [SwaggerResponse(201, "Tutor criado com sucesso", typeof(TutorEntity))]
    [SwaggerResponse(400, "Requisição inválida")]
    public async Task<IActionResult> Post(TutorEntity entity)
    {
        _context.Tutor.Add(entity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualiza tutor", Description = "Atualiza os dados de um tutor existente.")]
    [SwaggerResponse(200, "Tutor atualizado com sucesso", typeof(TutorEntity))]
    [SwaggerResponse(404, "Tutor não encontrado")]
    public async Task<IActionResult> Put(int id, TutorEntity tutorEntity)
    {
        var tutorExists = await _context.Tutor.FindAsync(id);
        if (tutorExists is not null)
        {
            tutorExists.Nome = tutorEntity.Nome;
            tutorExists.Email = tutorEntity.Email;
            tutorExists.Telefone = tutorEntity.Telefone;

            _context.Tutor.Update(tutorExists);
            await _context.SaveChangesAsync();
            return Ok(tutorExists);
        }
        return NotFound(new { mensagem = "Tutor não encontrado." });
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Remove tutor", Description = "Remove um tutor do sistema pelo ID.")]
    [SwaggerResponse(204, "Tutor removido com sucesso")]
    [SwaggerResponse(404, "Tutor não encontrado")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Tutor.FindAsync(id);
        if (entity is null)
            return NotFound(new { mensagem = "Tutor não encontrado." });

        _context.Tutor.Remove(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
