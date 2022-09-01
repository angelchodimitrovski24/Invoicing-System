using System.ComponentModel.DataAnnotations;

namespace Invoices.Web.API.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }

        public string CustomerName { get; set; }
    }
}