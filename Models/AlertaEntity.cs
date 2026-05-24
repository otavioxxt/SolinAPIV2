using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolinAPI.Models;

[Table("tb_alerta")]
public class AlertaEntity
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

    [Required]
    [StringLength(500)]
    [Column("s_mensagem")]
    public string Mensagem { get; set; }

    [Required]
    [StringLength(20)]
    [Column("s_severidade")]
    // BAIXA, MEDIA, ALTA
    public string Severidade { get; set; } = "BAIXA";

    [Column("b_lido")]
    public bool Lido { get; set; } = false;

    [Column("dt_criacao")]
    public DateTime DataCriacao { get; set; } = DateTime.Now;
}
