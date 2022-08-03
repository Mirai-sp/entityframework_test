using EntityFrameworkExercice.Models;
using EntityFrameworkExercice.Models.Enums;
using System;

namespace EntityFrameworkExercice
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            
            IncluirVendedor("Denis", VendedorStatus.Ativo);
        }

        private static void IncluirVendedor(string nome, VendedorStatus status)
        {
            using (var contexto = new AppDbContext())
            {
                var vendedor = new Vendedor(nome, status);
                contexto.Add(vendedor);
                contexto.SaveChanges();
            }
        }

        public static void IncluirVenda()
    }
}
