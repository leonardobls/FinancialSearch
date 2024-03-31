using System.Globalization;
using FinancialSearch.Models;

namespace FinancialSearch.Controllers
{
    internal class AcoesController
    {
        public void ProcuraPeloNome(List<ClienteModel> clientes, List<PagamentoModel> pagamentos)
        {
            Console.WriteLine("Você deseja:\n0-Consultar apenas as dívidas do cliente\n1-Consultar as dívidas e pagamentos");
            string result = Console.ReadLine();
            bool show = false;
            if (result == "1") show = true;

            Console.WriteLine(@"Digite o nome do cliente a ser consultado: ");
            string nome = Console.ReadLine();


            ClienteModel? cliente = clientes.Where(cliente => cliente.Nome == nome.ToLower()).FirstOrDefault();

            if (cliente != null)
            {
                var pagamentosEncontrados = pagamentos.Where(pagamento => pagamento.Id == cliente.Id).ToList();

                if (pagamentosEncontrados.Count > 0)
                {
                    bool entered = false;
                    foreach (var p in pagamentosEncontrados)
                    {
                        if ((show == true && p.Pago == "t") || p.Pago == "f")
                        {
                            result = p.Pago == "t" ? "realizado" : "em dívida";


                            Console.WriteLine($"Data: {p.Data}\tValor: {p.Valor}\tPagamento {result}");
                            entered = true;
                        }
                    }

                    if (entered == false)
                        Console.WriteLine("Cliente sem dívidas");
                }
                else
                {
                    Console.WriteLine("Nenhum valor pendente!");
                }
            }
            else
            {
                Console.WriteLine("Cliente não encontrado na base de dados!");
            }

        }

        public void ProcuraPeloValorSuperior(List<ClienteModel> clientes, List<PagamentoModel> pagamentos)
        {
            Console.WriteLine("Digite o valor mínimo da dívida (se quiser visualizar todas as dívidas no sistema, digite 0): ");
            string valor = Console.ReadLine();

            var pagamentosEncontrados = pagamentos.Where(pagamento => double.Parse(pagamento.Valor, CultureInfo.InvariantCulture.NumberFormat) >= double.Parse(valor, CultureInfo.InvariantCulture.NumberFormat) && pagamento.Pago == "f").ToList();

            if (pagamentosEncontrados.Count > 0)
            {
                foreach (var p in pagamentosEncontrados)
                {
                    var clienteEncontrado = clientes.Where(c => c.Id == p.Id).FirstOrDefault();

                    if (clienteEncontrado != null)
                    {
                        Console.WriteLine($"Cliente: {clienteEncontrado.Nome}\tValor: {double.Parse(p.Valor, CultureInfo.InvariantCulture.NumberFormat)}\tData: {p.Data}");
                    }
                    else
                    {
                        Console.WriteLine($"Cliente: Não identificado\tValor: {String.Format("{0:0.00}", p.Valor)}\tData: {p.Data}");
                    }
                }
                Console.WriteLine($"Há um total de {pagamentosEncontrados.Count} dívidas acima desse valor");
            }
        }

        public void RelatorioCompleto(List<ClienteModel> clientes, List<PagamentoModel> pagamentos)
        {

            Console.WriteLine("Você deseja:\n0-Consultar apenas as dívidas\n1-Consultar apenas os pagamentos");
            string result = Console.ReadLine();
            bool pagos = false;
            if (result == "1") pagos = true;


            var grupoPorDia = pagamentos.OrderBy(pagamento => pagamento.Data).GroupBy(pagamento => pagamento.Data).ToList();

            foreach (var grupo in grupoPorDia)
            {
                float soma = 0;

                foreach (var dia in grupo)
                {
                    if (dia.Pago == "f" && pagos == false)
                    {
                        soma += float.Parse(dia.Valor);
                    }
                    else if (dia.Pago == "t" && pagos == true)
                    {
                        soma += float.Parse(dia.Valor);
                    }
                }
                if (pagos == false)
                    Console.WriteLine($"\tDia: {grupo.First().Data}\tValor total das dívidas {soma}");
                else
                    Console.WriteLine($"\tDia: {grupo.First().Data}\tValor total dos pagamentos {soma}");


            }

            Console.WriteLine("\n");
        }
    }
}