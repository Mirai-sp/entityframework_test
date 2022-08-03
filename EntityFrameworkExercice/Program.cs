using EntityFrameworkExercice.Models;
using EntityFrameworkExercice.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Globalization;

namespace EntityFrameworkExercice
{
    class Program
    {
        // private delegate DelegateVendedor<TSource> (Expression<Func<TSource, bool>>);
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            //IncluirVendedor("Denis 2", VendedorStatus.Ativo);
            //InsertVenda();

            //QueryVenda(1);
            //QueryVenda(2);
            //QueryVenda(3);
            QueryVendedores();


        }

        private static void IncluirVendedor(string nome, VendedorStatus status)
        {
            using (var contexto = new AppDbContext())
            {
                var vendedor = new Vendedor(nome, status);
                //contexto.vend
                contexto.Add(vendedor);
                contexto.SaveChanges();
            }
        }

        //public static void IncluirVenda(Venda venda, Expression<Func<Vendedor, bool>> vendedor)
        public static void IncluirVenda(Venda venda, List<VendaItem> vendaItems, int vendedorId)
        {
            using (var contexto = new AppDbContext())
            {

                if (!contexto.Vendedores.Any(x => x.Id == vendedorId))
                    throw new ArgumentException("Vendedor não encontrado");

                Vendedor vendedor = contexto.Vendedores.Find(vendedorId);
                venda.Vendedor = vendedor;
                venda.Items = vendaItems;

                contexto.Vendas.Add(venda);
                contexto.SaveChanges();
            }
        }

        public static void InsertVenda()
        {
            try
            {
                //IncluirVenda(new Venda(DateTime.Now, VendaStatus.Aberto), vend => vend.Id == 1);

                List<VendaItem> Items = new List<VendaItem>();
                // Items.Add(new VendaItem { Descricao = "Racao", Unidade = "KG", Preco = 5.00, Quantidade = 2 });
                // Items.Add(new VendaItem { Descricao = "Racao Premium", Unidade = "KG", Preco = 10.00, Quantidade = 3 });
                // Items.Add(new VendaItem { Descricao = "Racao Moida", Unidade = "KG", Preco = 3.00, Quantidade = 4 });

                // Items.Add(new VendaItem { Descricao = "Acucar", Unidade = "KG", Preco = 5.00, Quantidade = 2 });
                // Items.Add(new VendaItem { Descricao = "Arroz", Unidade = "KG", Preco = 10.00, Quantidade = 3 });
                // Items.Add(new VendaItem { Descricao = "Feijao", Unidade = "KG", Preco = 3.00, Quantidade = 4 });

                //Items.Add(new VendaItem { Descricao = "Tomate", Unidade = "KG", Preco = 5.00, Quantidade = 2 });
                //Items.Add(new VendaItem { Descricao = "Cebola", Unidade = "KG", Preco = 10.00, Quantidade = 3 });
                //Items.Add(new VendaItem { Descricao = "Maionese", Unidade = "KG", Preco = 3.00, Quantidade = 4 });

                // IncluirVenda(new Venda(DateTime.Now, VendaStatus.Aberto), Items, 2);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
        }

        public static void QueryVenda(int vendaId)
        {
            using (var contexto = new AppDbContext())
            {
                Venda venda = contexto.Vendas.Include(x => x.Vendedor).Include(x => x.Items).FirstOrDefault(x => x.Id == vendaId);               

               
                Console.WriteLine("Data: " + venda.Date.ToString("dd/MM/yyyy hh:mm:ss"));
                Console.WriteLine("Vendedor: " + venda.Vendedor.Nome);
                Console.WriteLine("Status: " + venda.Status);
                Console.WriteLine("Total: " + venda.TotalGeral().ToString("F2", CultureInfo.InvariantCulture));
                Console.WriteLine();
                Console.WriteLine("Items:");
                foreach (VendaItem item in venda.Items)
                    Console.WriteLine("   Descrição: " + item.Descricao + ", Und: " + item.Unidade + ", Preco: " + item.Preco.ToString("F2", CultureInfo.InvariantCulture) + ", Qtd: " + item.Quantidade + " Total: " + item.Total().ToString("F2", CultureInfo.InvariantCulture));
            }
        }

        public static void QueryVendedores()
        {
            using (var contexto = new AppDbContext())
            {
                List<IGrouping<Vendedor, Venda>> vendas = (from obj in contexto.Vendas
                                                               select obj).Include(x => x.Vendedor).Include(x => x.Items)
                                                               .OrderBy(x => x.Vendedor.Nome)
                                                               .GroupBy(x => x.Vendedor).ToList();
                
                foreach (var vendaGroup in vendas)
                {
                    Console.WriteLine("Vendedor: " + vendaGroup.Key.Nome + " Total Vendido: " + vendaGroup.Key.TotalVendas(null, null).ToString("F2", CultureInfo.InvariantCulture));
                    foreach (var venda in vendaGroup)
                    {
                        Console.WriteLine("  Data: " + venda.Date.ToString("dd/MM/yyyy hh:mm:ss") + ", Total: " + venda.TotalGeral().ToString("F2", CultureInfo.InvariantCulture));
                        foreach (var item in venda.Items)
                            Console.WriteLine("      Descrição: " + item.Descricao + ", Unidade: " + item.Unidade + " , Qtd:" + item.Quantidade + " preco: " + item.Preco + ", total:" +  item.Total().ToString("F2", CultureInfo.InvariantCulture));
                    }
                }
                                                              
            }
        }
    }
}
