using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SolinAPI.Data;
using SolinAPI.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace SolinAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PetController : ControllerBase
{
    private readonly ApplicationContext _context;

    public PetController(ApplicationContext context)
    {
        _context = context;
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Lista pets", Description = "Retorna a lista completa de pets cadastrados.")]
    [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<PetEntity>))]
    public async Task<IActionResult> Get()
    {
        var result = await _context.Pet.ToListAsync();
        return Ok(result);
    }

    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Busca pet por ID", Description = "Retorna um pet pelo seu ID.")]
    [SwaggerResponse(200, "Pet encontrado", typeof(PetEntity))]
    [SwaggerResponse(404, "Pet não encontrado")]
    public async Task<IActionResult> Get(int id)
    {
        var result = await _context.Pet.FindAsync(id);
        if (result == null)
            return NotFound(new { mensagem = "Pet não encontrado." });
        return Ok(result);
    }

    [HttpGet("{id}/eventos")]
    [SwaggerOperation(Summary = "Lista eventos do pet", Description = "Retorna todos os eventos registrados de um pet.")]
    [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<EventoEntity>))]
    [SwaggerResponse(404, "Pet não encontrado")]
    public async Task<IActionResult> GetEventos(int id)
    {
        var petExists = await _context.Pet.FindAsync(id);
        if (petExists == null)
            return NotFound(new { mensagem = "Pet não encontrado." });

        var eventos = await _context.Evento
            .Where(e => e.IdPet == id)
            .OrderByDescending(e => e.DataEvento)
            .ToListAsync();
        return Ok(eventos);
    }

    [HttpGet("{id}/historico")]
    [SwaggerOperation(Summary = "Histórico do pet", Description = "Retorna o histórico de saúde de um pet.")]
    [SwaggerResponse(200, "Histórico retornado com sucesso", typeof(IEnumerable<HistoricoEntity>))]
    [SwaggerResponse(404, "Pet não encontrado")]
    public async Task<IActionResult> GetHistorico(int id)
    {
        var petExists = await _context.Pet.FindAsync(id);
        if (petExists == null)
            return NotFound(new { mensagem = "Pet não encontrado." });

        var historico = await _context.Historico
            .Where(h => h.IdPet == id)
            .OrderByDescending(h => h.DataRegistro)
            .ToListAsync();
        return Ok(historico);
    }

    [HttpGet("{id}/alertas")]
    [SwaggerOperation(Summary = "Alertas do pet", Description = "Retorna todos os alertas de um pet.")]
    [SwaggerResponse(200, "Alertas retornados com sucesso", typeof(IEnumerable<AlertaEntity>))]
    [SwaggerResponse(404, "Pet não encontrado")]
    public async Task<IActionResult> GetAlertas(int id)
    {
        var petExists = await _context.Pet.FindAsync(id);
        if (petExists == null)
            return NotFound(new { mensagem = "Pet não encontrado." });

        var alertas = await _context.Alerta
            .Where(a => a.IdPet == id)
            .OrderByDescending(a => a.DataCriacao)
            .ToListAsync();
        return Ok(alertas);
    }

    [HttpGet("{id}/eventos/{tipo}")]
    [SwaggerOperation(Summary = "Eventos do pet por tipo", Description = "Retorna eventos de um pet filtrados por tipo (CHECK_IN, PASSEIO, SENSOR_URINARIO, ALIMENTACAO).")]
    [SwaggerResponse(200, "Lista retornada com sucesso", typeof(IEnumerable<EventoEntity>))]
    [SwaggerResponse(404, "Pet não encontrado")]
    public async Task<IActionResult> GetEventosPorTipo(int id, string tipo)
    {
        var petExists = await _context.Pet.FindAsync(id);
        if (petExists == null)
            return NotFound(new { mensagem = "Pet não encontrado." });

        var eventos = await _context.Evento
            .Where(e => e.IdPet == id && e.Tipo.ToUpper() == tipo.ToUpper())
            .OrderByDescending(e => e.DataEvento)
            .ToListAsync();
        return Ok(eventos);
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Cria pet", Description = "Cadastra um novo pet no sistema.")]
    [SwaggerResponse(201, "Pet criado com sucesso", typeof(PetEntity))]
    [SwaggerResponse(400, "Requisição inválida")]
    public async Task<IActionResult> Post(PetEntity entity)
    {
        var tutorExists = await _context.Tutor.FindAsync(entity.IdTutor);
        if (tutorExists == null)
            return BadRequest(new { mensagem = "Tutor informado não encontrado." });

        entity.DataCadastro = DateTime.Now;
        _context.Pet.Add(entity);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = entity.Id }, entity);
    }

    [HttpPut("{id}")]
    [SwaggerOperation(Summary = "Atualiza pet", Description = "Atualiza os dados de um pet existente.")]
    [SwaggerResponse(200, "Pet atualizado com sucesso", typeof(PetEntity))]
    [SwaggerResponse(404, "Pet não encontrado")]
    public async Task<IActionResult> Put(int id, PetEntity petEntity)
    {
        var petExists = await _context.Pet.FindAsync(id);
        if (petExists is not null)
        {
            petExists.Nome = petEntity.Nome;
            petExists.Especie = petEntity.Especie;
            petExists.Raca = petEntity.Raca;
            petExists.DataNascimento = petEntity.DataNascimento;
            petExists.Sexo = petEntity.Sexo;
            petExists.Peso = petEntity.Peso;

            _context.Pet.Update(petExists);
            await _context.SaveChangesAsync();
            return Ok(petExists);
        }
        return NotFound(new { mensagem = "Pet não encontrado." });
    }

    [HttpDelete("{id}")]
    [SwaggerOperation(Summary = "Remove pet", Description = "Remove um pet do sistema pelo ID.")]
    [SwaggerResponse(204, "Pet removido com sucesso")]
    [SwaggerResponse(404, "Pet não encontrado")]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _context.Pet.FindAsync(id);
        if (entity is null)
            return NotFound(new { mensagem = "Pet não encontrado." });

        _context.Pet.Remove(entity);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
