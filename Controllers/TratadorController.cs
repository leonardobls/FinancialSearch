using System.Globalization;
using FinancialSearch.Models;

namespace FinancialSearch.Controllers
{
    internal class Tratador
    {
        public List<ClienteModel> trataClientes(List<dynamic> clientes)
        {

            List<ClienteModel> newClients = new List<ClienteModel>();

            foreach (var client in clientes)
            {

                var clientToAdd = new ClienteModel();

                clientToAdd.Id = client.Id;
                clientToAdd.Data = client.Data;
                clientToAdd.Valor = client.Valor;
                clientToAdd.Cpf = client.Cpf;
                clientToAdd.Nome = client.Nome.ToString().ToLower();

                newClients.Add(clientToAdd);
            }

            return newClients;
        }

        public List<PagamentoModel> trataPagamentos(List<dynamic> pagamentos)
        {
            List<PagamentoModel> newPagamentos = new List<PagamentoModel>();

            foreach (var pagamento in pagamentos)
            {

                if (pagamento.Data.Length < 8)
                {
                    string ano = pagamento.Data.Substring(pagamento.Data.Length - 4);
                    string mes = pagamento.Data.Substring(pagamento.Data.Length - 6, 2);
                    string dia = pagamento.Data.Length == 7 ? pagamento.Data.Substring(0, 1) : pagamento.Data.Substring(0, 2);
                    if (dia.Length == 1)
                    {
                        dia = "0" + dia;
                        var teste = 0;
                    }
                    pagamento.Data = dia + mes + ano;
                }

                DateTime data = DateTime.ParseExact(pagamento.Data, "ddMMyyyy", CultureInfo.InvariantCulture);

                newPagamentos.Add(new PagamentoModel()
                {
                    Id = pagamento.Id,
                    Data = data,
                    CodigoProduto = pagamento.CodigoProduto,
                    Valor = !string.IsNullOrEmpty(pagamento.Valor) ? Math.Round(double.Parse(pagamento.Valor, CultureInfo.InvariantCulture), 2) : (double?)null,
                    Pago = pagamento.Pago,
                });
            }

            return newPagamentos;
        }
    }
}