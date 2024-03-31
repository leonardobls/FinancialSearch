using FinancialSearch.Controllers;
using FinancialSearch.Models;

namespace FinancialSystem
{
    internal class Program
    {

        static void Main(string[] args)
        {
            List<dynamic> clientes = FileReaderController.GetContent("Arquivos/clientes.txt");
            List<dynamic> pagamentos = FileReaderController.GetContent("Arquivos/pagamentos.txt");
            Tratador t = new Tratador();
            List<ClienteModel> newClients = t.trataClientes(clientes);
            List<PagamentoModel> newPagamentos = t.trataPagamentos(pagamentos);
            AcoesController acoes = new AcoesController();

            Console.WriteLine("Bem vindo ao sitema de consulta de dívidas da sua empresa!");
            while (true)
            {
                Console.WriteLine(@" Escolha a operação a ser realizada:
                1-Busca pagamentos de um cliente específico
                2-Busca dívidas maiores a partir de um valor especificado
                3-Relatório completo ordenado por data
                0-Sair");
                string? caso = Console.ReadLine();

                switch (caso)
                {
                    case "1":
                        acoes.ProcuraPeloNome(newClients, newPagamentos);
                        break;
                    case "2":
                        acoes.ProcuraPeloValorSuperior(newClients, newPagamentos);
                        break;
                    case "3":
                        acoes.RelatorioCompleto(newClients, newPagamentos);
                        break;
                    case "0":
                        Console.WriteLine("Saindo!");
                        return;
                        break;
                    default:
                        Console.WriteLine("Valor inválido");
                        break;
                }
            }

        }




    }
}

