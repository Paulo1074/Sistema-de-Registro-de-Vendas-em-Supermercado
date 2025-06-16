using System;
using System.Collections.Generic;
using System.Linq;

namespace Supermercado
{           
    class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        public Produto(int id, string nome, decimal preco)
        {
            Id = id;
            Nome = nome;
            Preco = preco;
        }
    }

    class ItemVenda
    {
        public Produto Produto { get; set; }
        public int Quantidade { get; set; }

        public decimal Total => Produto.Preco * Quantidade;
    }

    class Program
    {
        static List<Produto> produtos = new List<Produto>();
        static void Main(string[] args)
        {
            bool executando = true;

            while (executando)
            {
                Console.WriteLine("\n=== SISTEMA DE VENDAS DO SUPERMERCADO ===");
                Console.WriteLine("1 - Cadastrar Produto");
                Console.WriteLine("2 - Listar Produtos");
                Console.WriteLine("3 - Realizar Venda");
                Console.WriteLine("0 - Sair");
                Console.Write("Escolha uma opção: ");

                string opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        CadastrarProduto();
                        break;
                    case "2":
                        ListarProdutos();
                        break;
                    case "3":
                        RealizarVenda();
                        break;
                    case "0":
                        executando = false;
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }
            }
        }

        static void CadastrarProduto()
        {
            Console.Write("\nInforme o ID do produto: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Informe o nome do produto: ");
            string nome = Console.ReadLine();

            Console.Write("Informe o preço do produto: ");
            decimal preco = decimal.Parse(Console.ReadLine());

            produtos.Add(new Produto(id, nome, preco));

            Console.WriteLine("Produto cadastrado com sucesso!");
        }

        static void ListarProdutos()
        {
            Console.WriteLine("\n--- Lista de Produtos ---");
            if (produtos.Count == 0)
            {
                Console.WriteLine("Nenhum produto cadastrado.");
                return;
            }

            foreach (var p in produtos)
            {
                Console.WriteLine($"ID: {p.Id} | Nome: {p.Nome} | Preço: R$ {p.Preco:F2}");
            }
        }

        static void RealizarVenda()
        {
            if (produtos.Count == 0)
            {
                Console.WriteLine("\nNenhum produto cadastrado. Cadastre produtos antes de realizar uma venda.");
                return;
            }

            List<ItemVenda> itensVenda = new List<ItemVenda>();
            bool adicionandoItens = true;

            while (adicionandoItens)
            {
                ListarProdutos();

                Console.Write("\nInforme o ID do produto para adicionar na venda: ");
                int idProduto = int.Parse(Console.ReadLine());

                var produto = produtos.FirstOrDefault(p => p.Id == idProduto);
                if (produto == null)
                {
                    Console.WriteLine("Produto não encontrado.");
                    continue;
                }

                Console.Write("Informe a quantidade: ");
                int quantidade = int.Parse(Console.ReadLine());

                itensVenda.Add(new ItemVenda { Produto = produto, Quantidade = quantidade });

                Console.Write("Deseja adicionar mais itens? (S/N): ");
                string resposta = Console.ReadLine().ToUpper();
                if (resposta != "S")
                {
                    adicionandoItens = false;
                }
            }

            GerarRecibo(itensVenda);
        }

        static void GerarRecibo(List<ItemVenda> itens)
        {
            Console.WriteLine("\n=== RECIBO DA VENDA ===");
            decimal totalGeral = 0;

            foreach (var item in itens)
            {
                Console.WriteLine($"{item.Produto.Nome} | Qtd: {item.Quantidade} | " +
                                  $"Unitário: R$ {item.Produto.Preco:F2} | " +
                                  $"Total: R$ {item.Total:F2}");

                totalGeral += item.Total;
            }

            Console.WriteLine("------------------------------");
            Console.WriteLine($"TOTAL GERAL: R$ {totalGeral:F2}");
            Console.WriteLine("==============================\n");
        }
    }
}
