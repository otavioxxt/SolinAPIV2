using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolinAPI.Models;

[Table("tb_tutor")]
public class TutorEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    [Column("s_nome")]
    public string Nome { get; set; }

    [Required]
    [StringLength(150)]
    [Column("s_email")]
    public string Email { get; set; }

    [StringLength(20)]
    [Column("s_telefone")]
    public string? Telefone { get; set; }

    [Column("dt_cadastro")]
    public DateTime DataCadastro { get; set; } = DateTime.Now;
}
