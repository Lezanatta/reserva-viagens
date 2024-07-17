namespace Viagens.WEB.Models;

public enum EnumPagamento
{
    Pagar = 1,
    AtualizarInfoPagamento
}

public class OpcaoPagamento
{
    public EnumPagamento EnumPagamento { get; set; }
    public Pagamento? Pagamento{ get; set; }
}
