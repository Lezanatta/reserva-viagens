using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Compartilhado.Models;

[Table("Estados")]
public class Estados
{
    [Key]
    public int IdEstados { get; set; }
    public string? Nome{ get; set; }
}
