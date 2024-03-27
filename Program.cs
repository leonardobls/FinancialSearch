
using System;
using System.Collections;
using System.Collections.Generic;
using RedeNeural.Controllers;
using RedeNeural.DataTransferObjects;
using RedeNeural.Models;

namespace FinancialSearch
{
    internal class Program
    {

        static void Main(string[] args)
        {
            List<dynamic> clientes = FileReaderController.GetContent("Arquivos/clientes.txt");
            List<dynamic> pagamentos = FileReaderController.GetContent("Arquivos/pagamentos.txt");

            List<ClientesModel> newClients = new List<ClientesModel>();
            List<PagamentoModel> newPagamentos = new List<PagamentoModel>();


            foreach (var client in clientes)
            {

                var clientToAdd = new ClientesModel();

                clientToAdd.Id = client.Id;
                clientToAdd.Data = client.Data;
                clientToAdd.Valor = client.Valor;
                clientToAdd.Cpf = client.Cpf;
                clientToAdd.Nome = client.Nome.ToString().ToLower();

                newClients.Add(clientToAdd);
            }

            foreach (var pagamento in pagamentos)
            {
                var pagamentoToAdd = new PagamentoModel()
                {
                    Id = pagamento.Id,
                    Data = pagamento.Data,
                    CodigoProduto = pagamento.CodigoProduto,
                    Valor = pagamento.Valor,
                    Pago = pagamento.Pago,
                };

                newPagamentos.Add(pagamentoToAdd);
            }

            string? caso = Console.ReadLine();

            switch (caso)
            {
                case "1":
                    ProcuraPeloNome(newClients, newPagamentos);
                    break;
                case "2":
                    ProcuraPeloValorSuperior(newClients, newPagamentos);
                    break;
                case "3":
                    Console.WriteLine("Terça-feira");
                    break;
                case "4":
                    Console.WriteLine("Quarta-feira");
                    break;
                case "0":
                    Console.WriteLine("Saindo!");
                    break;
                default:
                    Console.WriteLine("Valor inválido");
                    break;
            }

        }

        static void ProcuraPeloNome(List<ClientesModel> clientes, List<PagamentoModel> pagamentos)
        {

            string nome = Console.ReadLine();

            ClientesModel? cliente = clientes.Where(cliente => cliente.Nome == nome.ToLower()).FirstOrDefault();

            if (cliente != null)
            {
                var pagamentosEncontrados = pagamentos.Where(pagamento => pagamento.Id == cliente.Id && pagamento.Pago == "f").ToList();

                if (pagamentosEncontrados.Count > 0)
                {
                    foreach (var p in pagamentosEncontrados)
                    {
                        Console.WriteLine($"Data: {p.Data}\tValor: {p.Valor}");
                    }
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

        static void ProcuraPeloValorSuperior(List<ClientesModel> clientes, List<PagamentoModel> pagamentos)
        {

            string valor = Console.ReadLine();

            var pagamentosEncontrados = pagamentos.Where(pagamento => float.Parse(pagamento.Valor) >= float.Parse(valor) && pagamento.Pago == "f").ToList();

            if (pagamentosEncontrados.Count > 0)
            {
                foreach (var p in pagamentosEncontrados)
                {
                    var clienteEncontrado = clientes.Where(c => c.Id == p.Id).FirstOrDefault();

                    if (clienteEncontrado != null)
                    {
                        Console.WriteLine($"Cliente: {clienteEncontrado.Nome}\tValor: {String.Format("{0:0.00}", p.Valor)}\tData: {p.Data}");
                    }
                    else
                    {
                        Console.WriteLine($"Cliente: Não identificado\tValor: {String.Format("{0:0.00}", p.Valor)}\tData: {p.Data}");
                    }
                }
            }
        }

        // static void ProcuraPeloValorSuperior(List<ClientesModel> clientes, List<PagamentoModel> pagamentos)
        // {

        //     string valor = Console.ReadLine();

        //     var pagamentosEncontrados = pagamentos.Where(pagamento => float.Parse(pagamento.Valor) >= float.Parse(valor) && pagamento.Pago == "f").ToList();

        //     if (pagamentosEncontrados.Count > 0)
        //     {
        //         foreach (var p in pagamentosEncontrados)
        //         {
        //             var clienteEncontrado = clientes.Where(c => c.Id == p.Id).FirstOrDefault();

        //             if (clienteEncontrado != null)
        //             {
        //                 Console.WriteLine($"Cliente: {clienteEncontrado.Nome}\tValor: {String.Format("{0:0.00}", p.Valor)}\tData: {p.Data}");
        //             }
        //             else
        //             {
        //                 Console.WriteLine($"Cliente: Não identificado\tValor: {String.Format("{0:0.00}", p.Valor)}\tData: {p.Data}");
        //             }
        //         }
        //     }
        // }
    }
}

