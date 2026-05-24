using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolinAPI.Data;
using SolinAPI.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SolinAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EventoController : ControllerBase
{
    private readonly ApplicationContext _context;

    public EventoController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Lista eventos", Description = "Retorna a lista completa de eventos registrados.")]
    [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<EventoEntity>))]
    public async Task<IActionResult> Get()
    {
        var result = await _context.Evento.ToListAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Busca evento por ID", Description = "Retorna um evento pelo seu ID.")]
    [SwaggerResponse(200, "Evento encontrado", typeof(EventoEntity))]
    [SwaggerResponse(404, "Evento não encontrado")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _context.Evento.FindAsync(id);
        if (result == null)
            return NotFound(new { mensagem = "Evento não encontrado." });
        return Ok(result);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cria evento", Description = "Registra um novo evento para um pet (check-in, passeio, sensor IoT, etc).")]
    [SwaggerResponse(201, "Evento criado com sucesso", typeof(EventoEntity))]
    [SwaggerResponse(400, "Requisição inválida")]
    public async Task<IActionResult> Post(EventoEntity entity)
    {
        var petExists = await _context.Pet.FindAsync(entity.IdPet);
        if (petExists == null)
            return BadRequest(new { mensagem = "Pet informado não encontrado." });

        entity.DataEvento = DateTime.Now;
        _context.Evento.Add(entity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualiza evento", Description = "Atualiza os dados de um evento existente.")]
    [SwaggerResponse(200, "Evento atualizado com sucesso", typeof(EventoEntity))]
    [SwaggerResponse(404, "Evento não encontrado")]
    public async Task<IActionResult> Put(int id, EventoEntity eventoEntity)
    {
        var eventoExists = await _context.Evento.FindAsync(id);
        if (eventoExists is not null)
        {
            eventoExists.Tipo = eventoEntity.Tipo;
            eventoExists.Descricao = eventoEntity.Descricao;
            eventoExists.Origem = eventoEntity.Origem;

            _context.Evento.Update(eventoExists);
            await _context.SaveChangesAsync();
            return Ok(eventoExists);
        }
        return NotFound(new { mensagem = "Evento não encontrado." });
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Remove evento", Description = "Remove um evento do sistema pelo ID.")]
    [SwaggerResponse(204, "Evento removido com sucesso")]
    [SwaggerResponse(404, "Evento não encontrado")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Evento.FindAsync(id);
        if (entity is null)
            return NotFound(new { mensagem = "Evento não encontrado." });

        _context.Evento.Remove(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
