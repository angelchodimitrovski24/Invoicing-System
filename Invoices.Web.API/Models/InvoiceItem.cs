using System.ComponentModel.DataAnnotations;

namespace Invoices.Web.API.Models
{
    public class InvoiceItem
    {
        [Key]
        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public decimal ItemPrice { get; set; }
    }
}