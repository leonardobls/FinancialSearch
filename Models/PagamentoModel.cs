namespace FinancialSearch.Models
{
    public class PagamentoModel
    {
        public string? Id { get; set; }

        public DateTime? Data { get; set; }

        public string? CodigoProduto { get; set; }

        public string? Valor { get; set; }

        public string? Pago { get; set; }
    }
}