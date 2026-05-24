using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolinAPI.Models;

[Table("tb_evento")]
public class EventoEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column("id_pet")]
    public int IdPet { get; set; }

    [Required]
    [StringLength(50)]
    [Column("s_tipo")]
    // CHECK_IN, PASSEIO, SENSOR_URINARIO, ALIMENTACAO
    public string Tipo { get; set; }

    [StringLength(500)]
    [Column("s_descricao")]
    public string? Descricao { get; set; }

    [Column("dt_evento")]
    public DateTime DataEvento { get; set; } = DateTime.Now;

    [StringLength(20)]
    [Column("s_origem")]
    // APP ou IOT
    public string Origem { get; set; } = "APP";
}
