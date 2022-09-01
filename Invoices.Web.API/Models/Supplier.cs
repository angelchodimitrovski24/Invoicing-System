using System.ComponentModel.DataAnnotations;

namespace Invoices.Web.API.Models
{
    public class Supplier
    {
        [Key]
        public int SupplierId { get; set; } 
        public string SupplierName { get; set; }
    }
}