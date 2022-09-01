using Microsoft.VisualStudio.Debugger.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Web.API.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }

        public string InvoiceNumber { get; set; }

        public decimal Total { get; set; }

        public List<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();

        public Customer Customer { get; set; }

        public Supplier Suppier { get; set; }
    }
}
