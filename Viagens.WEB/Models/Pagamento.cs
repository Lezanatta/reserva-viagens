using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Viagens.WEB.Models;
public class Pagamento
{
    [Key]
    public int IdPagamento { get; set; }
    public DateTime? DataPagamento{ get; set; }
    public int Parcelas{ get; set; }
    [Column("Valor_total")]
    public decimal ValorTotal{ get; set; }
    [Column("Valor_pago")]
    public decimal ValorPago{ get; set; }
    [Column("Parcela_atual")]
    public int ParcelaAtual { get; set; }
}
