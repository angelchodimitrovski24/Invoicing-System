

using System.ComponentModel.DataAnnotations;

namespace Invoices.Web.API.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
    }
}
