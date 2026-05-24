using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolinAPI.Models;

[Table("tb_pet")]
public class PetEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column("id_tutor")]
    public int IdTutor { get; set; }

    [Required]
    [StringLength(80, MinimumLength = 2)]
    [Column("s_nome")]
    public string Nome { get; set; }

    [Required]
    [StringLength(50)]
    [Column("s_especie")]
    public string Especie { get; set; }

    [StringLength(50)]
    [Column("s_raca")]
    public string? Raca { get; set; }

    [Column("dt_nascimento")]
    public DateTime? DataNascimento { get; set; }

    [StringLength(10)]
    [Column("s_sexo")]
    public string? Sexo { get; set; }

    [Column("n_peso")]
    public decimal? Peso { get; set; }

    [Column("dt_cadastro")]
    public DateTime DataCadastro { get; set; } = DateTime.Now;
}
