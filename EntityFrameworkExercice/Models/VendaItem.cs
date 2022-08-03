using System;
using System.Collections.Generic;
using System.Text;
using EntityFrameworkExercice.Models;

namespace EntityFrameworkExercice.Models
{
    public class VendaItem
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public string Unidade { get; set; }
        public int Quantidade { get; set; }
        public double Preco { get; set; }
        public Venda Venda { get; set; }
        public int VendaId { get; set; }

        public VendaItem()
        {

        }
        public VendaItem(string descricao, string unidade, double preco, Venda venda)
        {            
            Descricao = descricao;
            Unidade = unidade;
            Preco = preco;
            Venda = venda;
        }

        public VendaItem(int id, string descricao, string unidade, double preco, Venda venda) : this(descricao, unidade, preco, venda) {
            Id = id;
        }

        public double Total()
        {
            return Quantidade * Preco;
        }
    }
}
