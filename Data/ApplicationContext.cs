using Microsoft.EntityFrameworkCore;
using SolinAPI.Models;

namespace SolinAPI.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }

    public DbSet<TutorEntity> Tutor { get; set; }
    public DbSet<PetEntity> Pet { get; set; }
    public DbSet<EventoEntity> Evento { get; set; }
    public DbSet<HistoricoEntity> Historico { get; set; }
    public DbSet<AlertaEntity> Alerta { get; set; }
}
