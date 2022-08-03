using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkExercice.Models.Enums;
using System.Linq;

namespace EntityFrameworkExercice.Models
{
    public class Vendedor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public VendedorStatus Ativo { get; set; }
        public ICollection<Venda> Vendas { get; set; } = new List<Venda>();

        public Vendedor()
        {

        }
        public Vendedor(string nome, VendedorStatus ativo)
        {            
            Nome = nome;
            Ativo = ativo;
        }

        public Vendedor(int id, string nome, VendedorStatus ativo) : this(nome, ativo)
        {
            Id = id;
        }

        public double TotalVendas(DateTime? dataInicio, DateTime? dataFim)
        {
            var result = (from v in Vendas
                         select new { Date = v.Date, TotalGeral = v.TotalGeral() });
            if (dataInicio.HasValue)
                result = result.Where(x => x.Date >= dataInicio);

            if (dataFim.HasValue)
                result = result.Where(x => x.Date >= dataFim);

            return result.Select(p => p.TotalGeral).DefaultIfEmpty(0.00).Sum();
        }
    }
}
