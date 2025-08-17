using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic_Manager.Core.Entities
{
    public class TradeRepresentative
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Neighborhood { get; set; }
        public string Cep { get; set; }
        public string? Uf { get; set; }
        public string? Cnpj { get; set; }
        public string? Cpf { get; set; }
        public string? Cellfone { get; set; }
        public string BankName { get;}
        public string Bank { get;}
        public string Agency { get; set; }
        public string Account { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
