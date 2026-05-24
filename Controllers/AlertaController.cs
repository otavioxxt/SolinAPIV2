using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolinAPI.Data;
using SolinAPI.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SolinAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AlertaController : ControllerBase
{
    private readonly ApplicationContext _context;

    public AlertaController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Lista alertas", Description = "Retorna a lista completa de alertas gerados.")]
    [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<AlertaEntity>))]
    public async Task<IActionResult> Get()
    {
        var result = await _context.Alerta.ToListAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Busca alerta por ID", Description = "Retorna um alerta pelo seu ID.")]
    [SwaggerResponse(200, "Alerta encontrado", typeof(AlertaEntity))]
    [SwaggerResponse(404, "Alerta não encontrado")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _context.Alerta.FindAsync(id);
        if (result == null)
            return NotFound(new { mensagem = "Alerta não encontrado." });
        return Ok(result);
    }

    [HttpGet("pet/{idPet}/nao-lidos")]
    [SwaggerOperation(Summary = "Alertas não lidos do pet", Description = "Retorna todos os alertas não lidos de um pet específico.")]
    [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<AlertaEntity>))]
    [SwaggerResponse(404, "Pet não encontrado")]
    public async Task<IActionResult> GetNaoLidos(int idPet)
    {
        var petExists = await _context.Pet.FindAsync(idPet);
        if (petExists == null)
            return NotFound(new { mensagem = "Pet não encontrado." });

        var alertas = await _context.Alerta
            .Where(a => a.IdPet == idPet && !a.Lido)
            .OrderByDescending(a => a.DataCriacao)
            .ToListAsync();
        return Ok(alertas);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cria alerta", Description = "Cria um novo alerta para um pet.")]
    [SwaggerResponse(201, "Alerta criado com sucesso", typeof(AlertaEntity))]
    [SwaggerResponse(400, "Requisição inválida")]
    public async Task<IActionResult> Post(AlertaEntity entity)
    {
        var petExists = await _context.Pet.FindAsync(entity.IdPet);
        if (petExists == null)
            return BadRequest(new { mensagem = "Pet informado não encontrado." });

        entity.DataCriacao = DateTime.Now;
        entity.Lido = false;
        _context.Alerta.Add(entity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualiza alerta", Description = "Atualiza um alerta existente, por exemplo para marcar como lido.")]
    [SwaggerResponse(200, "Alerta atualizado com sucesso", typeof(AlertaEntity))]
    [SwaggerResponse(404, "Alerta não encontrado")]
    public async Task<IActionResult> Put(int id, AlertaEntity alertaEntity)
    {
        var alertaExists = await _context.Alerta.FindAsync(id);
        if (alertaExists is not null)
        {
            alertaExists.Titulo = alertaEntity.Titulo;
            alertaExists.Mensagem = alertaEntity.Mensagem;
            alertaExists.Severidade = alertaEntity.Severidade;
            alertaExists.Lido = alertaEntity.Lido;

            _context.Alerta.Update(alertaExists);
            await _context.SaveChangesAsync();
            return Ok(alertaExists);
        }
        return NotFound(new { mensagem = "Alerta não encontrado." });
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Remove alerta", Description = "Remove um alerta do sistema pelo ID.")]
    [SwaggerResponse(204, "Alerta removido com sucesso")]
    [SwaggerResponse(404, "Alerta não encontrado")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Alerta.FindAsync(id);
        if (entity is null)
            return NotFound(new { mensagem = "Alerta não encontrado." });

        _context.Alerta.Remove(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
