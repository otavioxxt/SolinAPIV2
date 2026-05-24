using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolinAPI.Models;

[Table("tb_historico")]
public class HistoricoEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column("id_pet")]
    public int IdPet { get; set; }

    [Required]
    [StringLength(100)]
    [Column("s_titulo")]
    public string Titulo { get; set; }

    [StringLength(1000)]
    [Column("s_descricao")]
    public string? Descricao { get; set; }

    [Required]
    [StringLength(50)]
    [Column("s_categoria")]
    // CONSULTA, VACINA, CIRURGIA, EXAME, MEDICAMENTO
    public string Categoria { get; set; }

    [StringLength(100)]
    [Column("s_veterinario")]
    public string? Veterinario { get; set; }

    [Column("dt_registro")]
    public DateTime DataRegistro { get; set; } = DateTime.Now;
}
