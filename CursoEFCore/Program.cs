using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;

namespace CursoEFCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // InserirDados();
            // InserirDadosEmMassa();
            // CadastrarPedido();

            // ConsultarDados();
            // ConsultaPedidoCA();

            // AtualizarDados();

            RemoveRegistro();

        }
        private static void InserirDados()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "12345678910",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var produto2 = new Produto
            {
                Descricao = "Produto Teste 2",
                CodigoBarras = "0808221409",
                Valor = 25.5m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = false
            };

            using var db = new Data.ApplicationContext();

            // db.Produtos.Add(produto);
            // db.Set<Produto>().Add(produto);
            // db.Entry(produto).State = EntityState.Added;
            //db.Add(produto);
            //db.Add(produto2);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total Registro(s): {registros}");
        }

        private static void InserirDadosEmMassa()
        {
            var produto = new Produto
            {
                Descricao = "Produto Teste 3",
                CodigoBarras = "0808221434",
                Valor = 32.15m,
                TipoProduto = TipoProduto.MercadoriaParaRevenda,
                Ativo = true
            };

            var cliente = new Cliente
            {
                Nome = "Pedro",
                CEP = "29260000",
                Cidade = "Domingos Martins",
                Estado = "ES",
                Telefone = "27999999999"
            };

            //Passando Array de clientes

            var listaClientes = new[]
            {
                new Cliente
                {
                Nome= "João Paulo",
                CEP = "29260000",
                Cidade ="Domingos Martins",
                Estado = "ES",
                Telefone = "27999999999"
                },
                new Cliente
                {
                Nome= "Marcos",
                CEP = "29260000",
                Cidade ="Domingos Martins",
                Estado = "ES",
                Telefone = "27999999999"
                },
            };

            using var db = new Data.ApplicationContext();
            //db.AddRange(produto, cliente);

            //Grava Array de Clientes
            db.Clientes.AddRange(listaClientes);

            var registros = db.SaveChanges();
            Console.WriteLine($"Total Registro(s): {registros}");
        }

        private static void ConsultarDados()
        {
            using var db = new Data.ApplicationContext();
            //var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToListAsync();
            var consultaPorMetodos = db.Clientes.AsNoTracking().Where(p => p.Id > 0).ToList();
            foreach(var cliente in consultaPorMetodos)
            {
                Console.BackgroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Consultado Cliente: {cliente.Id} - {cliente.Nome}");
                Console.BackgroundColor = ConsoleColor.Black;
                Console.WriteLine($"Telefone: {cliente.Telefone}\nEndereço: {cliente.Cidade}, {cliente.Estado}, {cliente.CEP}.");
                db.Clientes.Find(cliente.Id);
            }
        }

        private static void CadastrarPedido()
        {
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var pedido = new Pedido
            {
                ClienteID = cliente.Id,
                IniciadoEm = DateTime.Now,
                FinalizadoEm = DateTime.Now,
                Observacao = "Pedido Teste",
                Status = StatusPedido.Analise,
                TipoFrete = TipoFrete.SemFrete,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem  
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10,
                    }
                }
            };

            db.Pedidos.Add(pedido);
            db.SaveChanges();

        }

        private static void ConsultaPedidoCA()
        {
            using var db = new Data.ApplicationContext();
            var pedidos = db.Pedidos
                .Include(p => p.Itens)
                .ThenInclude(p => p.Produto)
                .ToList();

            Console.WriteLine(pedidos.Count);
        }

        private static void AtualizarDados()
        {
            using var db = new Data.ApplicationContext();
            var cliente = db.Clientes.Find(3);
            cliente.Nome = "Cliente Alterado p#1";

            db.Clientes.Update(cliente);
            db.SaveChanges();
        }

        private static void RemoveRegistro()
        {
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.Find(4);
            db.Clientes.Remove(cliente);
            
           // db.Remove(cliente);
           // db.Entry(cliente).State = EntityState.Deleted;

            Console.WriteLine($"O Cliente {cliente.Nome} foi removido do banco de dados !");
            db.SaveChanges();
        }

    }
}