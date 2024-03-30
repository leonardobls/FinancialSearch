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
                //clientToAdd.Data = ConverteData(client.Data);
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
                var pagamentoToAdd = new PagamentoModel()
                {
                    Id = pagamento.Id,
                    Data = ConverteData(pagamento.Data),
                    //Data = pagamento.Data,
                    CodigoProduto = pagamento.CodigoProduto,
                    Valor = pagamento.Valor,
                    Pago = pagamento.Pago,
                };

                newPagamentos.Add(pagamentoToAdd);
            }

            return newPagamentos;
        }

        static string ConverteData(string data)
        {
            if (data.Length > 0)
            {
                string ano = data.Substring(data.Length - 4);
                string mes = data.Substring(data.Length - 6, 2);
                string dia = data.Length == 7 ? data.Substring(0, 1) : data.Substring(0, 2);

                Console.WriteLine(ano + mes + String.Format("{0:00}", int.Parse(dia)));


                return ano + mes + String.Format("{0:00}", int.Parse(dia)); ;
            }
            else
                return data;
        }
    }
}