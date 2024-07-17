using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Compartilhado.Models;
[Table("Reserva")]
public class Reserva
{
    [Key]
    public int IdReserva { get; set; }
    public int IdEstado { get; set; }
    public int Checkin { get; set; }
    public int Checkout { get; set; }
    public int IdPagamento { get; set; }
    public DateTime? DataReserva { get; set; }
    public DateTime? DataCheckin { get; set; }
}
