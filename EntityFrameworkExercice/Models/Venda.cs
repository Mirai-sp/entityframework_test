using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkExercice.Models.Enums;
using System.Linq;

namespace EntityFrameworkExercice.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public VendaStatus Status { get; set; }
        public Vendedor Vendedor { get; set; }
        public int VendedorId { get; set; }
        public ICollection<VendaItem> Items { get; set; }

        public Venda()
        {

        }
        public Venda(DateTime date, VendaStatus status)
        {
            Date = date;
            Status = status;            
        }
        public Venda(DateTime date, VendaStatus status, Vendedor vendedor) : this(date, status)
        {                    
            Vendedor = vendedor;
        }

        public Venda(int id, DateTime date, VendaStatus status, Vendedor vendedor) : this(date, status, vendedor)
        {
            Id = id;
        }

        public double TotalGeral()
        {
            return (from v in Items
                    select v.Total()).DefaultIfEmpty(0.0).Sum();
        }
    }
}
