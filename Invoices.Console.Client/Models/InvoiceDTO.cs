using Invoices.Web.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Invoices.Console.Client.Models
{
    public class InvoiceDTO
    {
        public int InvoiceId { get; set; }

        public string InvoiceNumber { get; set; }

        public decimal Total { get; set; }

        public List<InvoiceItem> InvoiceItems { get; set; }

        public Customer Customer { get; set; }
    }
}
