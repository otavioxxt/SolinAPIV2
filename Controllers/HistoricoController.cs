using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolinAPI.Data;
using SolinAPI.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SolinAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HistoricoController : ControllerBase
{
    private readonly ApplicationContext _context;

    public HistoricoController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Lista históricos", Description = "Retorna todos os registros de histórico de saúde.")]
    [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<HistoricoEntity>))]
    public async Task<IActionResult> Get()
    {
        var result = await _context.Historico.ToListAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Busca histórico por ID", Description = "Retorna um registro de histórico pelo seu ID.")]
    [SwaggerResponse(200, "Histórico encontrado", typeof(HistoricoEntity))]
    [SwaggerResponse(404, "Histórico não encontrado")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _context.Historico.FindAsync(id);
        if (result == null)
            return NotFound(new { mensagem = "Histórico não encontrado." });
        return Ok(result);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cria histórico", Description = "Registra um novo histórico de saúde para um pet.")]
    [SwaggerResponse(201, "Histórico criado com sucesso", typeof(HistoricoEntity))]
    [SwaggerResponse(400, "Requisição inválida")]
    public async Task<IActionResult> Post(HistoricoEntity entity)
    {
        var petExists = await _context.Pet.FindAsync(entity.IdPet);
        if (petExists == null)
            return BadRequest(new { mensagem = "Pet informado não encontrado." });

        entity.DataRegistro = DateTime.Now;
        _context.Historico.Add(entity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualiza histórico", Description = "Atualiza um registro de histórico existente.")]
    [SwaggerResponse(200, "Histórico atualizado com sucesso", typeof(HistoricoEntity))]
    [SwaggerResponse(404, "Histórico não encontrado")]
    public async Task<IActionResult> Put(int id, HistoricoEntity historicoEntity)
    {
        var historicoExists = await _context.Historico.FindAsync(id);
        if (historicoExists is not null)
        {
            historicoExists.Titulo = historicoEntity.Titulo;
            historicoExists.Descricao = historicoEntity.Descricao;
            historicoExists.Categoria = historicoEntity.Categoria;
            historicoExists.Veterinario = historicoEntity.Veterinario;

            _context.Historico.Update(historicoExists);
            await _context.SaveChangesAsync();
            return Ok(historicoExists);
        }
        return NotFound(new { mensagem = "Histórico não encontrado." });
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Remove histórico", Description = "Remove um registro de histórico pelo ID.")]
    [SwaggerResponse(204, "Histórico removido com sucesso")]
    [SwaggerResponse(404, "Histórico não encontrado")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Historico.FindAsync(id);
        if (entity is null)
            return NotFound(new { mensagem = "Histórico não encontrado." });

        _context.Historico.Remove(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
