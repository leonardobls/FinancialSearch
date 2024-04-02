using System.Globalization;
using CsvHelper.Configuration.Attributes;

namespace FinancialSearch.Models
{
    public class PagamentoModel
    {
        public string? Id { get; set; }

        public string? CodigoProduto { get; set; }

        public double? Valor { get; set; }

        public string? Pago { get; set; }

        [Name("Data")]
        public string DataString { get; set; }

        public DateTime? Data
        {
            get => DateTime.TryParseExact(DataString, "ddMMyyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var resultado) ? resultado : (DateTime?)null;
            set => DataString = value?.ToString("ddMMyyyy");
        }
    }
}