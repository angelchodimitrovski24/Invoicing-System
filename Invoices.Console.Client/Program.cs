
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Policy;
using Invoices.Console.Client.Models;
using Invoices.Web.API.Models;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// 
/// </summary>
public class Program
{
    static HttpClient client = new();

    static void ShowInvoices(Invoice invoice)
    {
        InvoiceDTO invoiceDTO = new InvoiceDTO
        {
            InvoiceId = invoice.InvoiceId,
            InvoiceNumber = invoice.InvoiceNumber,
            Total = GetTotal(invoice),
            InvoiceItems = invoice.InvoiceItems,
            Customer = invoice.Customer,
        };

        Console.WriteLine("Invoice Details:");
        Console.WriteLine($" ID {invoiceDTO.InvoiceId} InvoiceNumber {invoiceDTO.InvoiceNumber} " +
                              $" Customer {invoiceDTO.Customer.CustomerName} Total: {invoiceDTO.Total}");

        Console.WriteLine("");
        Console.WriteLine("");

        Console.WriteLine("Invoice Items:");
        foreach (var items in invoiceDTO.InvoiceItems.AsQueryable())
        {
            Console.WriteLine($" Item Name: {items.ItemName}, ItemPrice {items.ItemPrice}");
        }
    }

    static decimal GetTotal(Invoice invoice)
    {
        InvoiceDTO invoiceDTO = new InvoiceDTO
        {
            InvoiceId = invoice.InvoiceId,
            InvoiceNumber = invoice.InvoiceNumber,
            Total = invoice.Total,
            InvoiceItems = invoice.InvoiceItems,
            Customer = invoice.Customer,
        };

        var total = invoiceDTO.InvoiceItems.Select(i => i.ItemPrice).Sum();

        return total;
    }

    #region Customer

    #endregion Customer

    #region InvoiceItems

    #endregion InvoiceItems


    #region Invoices
    static async Task<Uri?> CreateInvoiceAsync(Invoice invoice)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("api/Invoices", invoice);
        response.EnsureSuccessStatusCode();

        // return URI of the created resource
        return response.Headers.Location;
    }

    // samo pri POST and get it back whatever is saved
    static async Task<Invoice?> GetInvoiceAsync(string path)
    {
        Invoice? invoice = null;

        HttpResponseMessage response = await client.GetAsync(path);

        if (response.IsSuccessStatusCode)
        {
            invoice = await response.Content.ReadFromJsonAsync<Invoice>();
        }

        return invoice;
    }

    static async Task<Invoice?> GetInvoicesAsync()
    {
        Invoice? invoice = null;

        HttpResponseMessage response = await client.GetAsync($"api/Invoices");

        if (response.IsSuccessStatusCode)
        {
            invoice = await response.Content.ReadFromJsonAsync<Invoice>();
        }

        return invoice;
    }

    static async Task<Invoice?> GetInvoiceIdAsync(int invoiceId)
    {
        Invoice? invoice = null;

        HttpResponseMessage response = await client.GetAsync($"api/Invoices/{invoiceId}");

        if (response.IsSuccessStatusCode)
        {
            invoice = await response.Content.ReadFromJsonAsync<Invoice>();
        }

        return invoice;
    }

    static async Task<Invoice?> UpdateInvoiceAsync(Invoice invoice)
    {
        HttpResponseMessage response = await client.PutAsJsonAsync($"api/Invoices/{invoice.InvoiceId}", invoice);
        response.EnsureSuccessStatusCode();

        // Deserialize the invoice data
        invoice = await response.Content.ReadFromJsonAsync<Invoice>();
        return invoice;
    }

    static async Task<HttpStatusCode> DeleteInvoiceAsync(int id)
    {
        HttpResponseMessage response = await client.DeleteAsync($"api/Invoices/{id}");
        return response.StatusCode;
    }

    static async Task RunAsync()
    {
        client.BaseAddress = new Uri("https://localhost:7207/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        try
        {
            Invoice invoice = new Invoice()
            {
                InvoiceNumber = "Inv 1",
                InvoiceItems = new List<InvoiceItem>()
                {
                    new InvoiceItem { ItemName="Leb",ItemPrice=45},
                    new InvoiceItem { ItemName="Sol",ItemPrice=55},
                    new InvoiceItem { ItemName="Tikvi",ItemPrice=65.49M},
                },
                Customer = new Customer { CustomerName = "IBM" },
            };

            decimal total = GetTotal(invoice);
            invoice.Total = total;


            // Create an Invoice
            Console.WriteLine("Creating an Invoice....");
            var url = await CreateInvoiceAsync(invoice).ConfigureAwait(false);
            Console.WriteLine($"Created invoice at {url}");
            Console.WriteLine("Invoice created....");

            Console.WriteLine();

            // Get the Invoice
            Console.WriteLine("Showing Invoices....");
            invoice = await GetInvoiceAsync(url.PathAndQuery).ConfigureAwait(false);
            if (invoice != null) ShowInvoices(invoice);
            Console.WriteLine("Invoices displayed....");

            Console.WriteLine();



            //// Update the Invoice
            //Console.WriteLine("Updating an Invoice....");
            //Task.Delay(500);
            //invoice.InvoiceId = 1;
            //invoice.InvoiceNumber = "Inv 2";
            //invoice.Customer = "Customer 2";
            //invoice.Total = 200;
            //await UpdateInvoiceAsync(invoice).ConfigureAwait(false);
            //Console.WriteLine("Invoice Updated....");

            //Console.WriteLine();

            //// Get the Updated Invoice
            //Console.WriteLine("Updated Invoice....");
            //Task.Delay(500);
            //invoice = await GetInvoiceAsync(url.PathAndQuery).ConfigureAwait(false);
            //if (invoice != null) ShowInvoices(invoice);
            //Console.WriteLine("Updated Invoices displayed....");

            //Console.WriteLine();

            //// Delete an Invoice
            //Console.WriteLine("Deleting an Invoice....");
            //Task.Delay(500);
            //if (invoice != null)
            //{
            //    var statusCode = await DeleteInvoiceAsync(invoice.InvoiceId).ConfigureAwait(false);
            //    Console.WriteLine($"Invoice Deleted....HTTPS Status Code{statusCode}");
            //}

            Console.WriteLine();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    private async static Task<HttpResponseMessage> CreateProductAsync()
    {
        client.BaseAddress = new Uri("https://localhost:7207/");
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        // Products

        var products = new List<Product>()
        {
            new Product() {ProductName = "Laptop HP", ProductPrice = 600M},
            new Product() {ProductName = "Table", ProductPrice = 50M },
            new Product() {ProductName = "Table", ProductPrice = 50M },
        };

        HttpResponseMessage? response = null;

        foreach (var items in products)
        {
            response = await client.PostAsJsonAsync("api/Products", items);
        }

        response?.EnsureSuccessStatusCode();

        Console.WriteLine("Product created....");

        return response;
    }
    #endregion Invoices



    static async Task Main()
    {
        //PRODUCTS
        //use only when you 1st create products
        // await CreateProductAsync().ConfigureAwait(false);

        //CUSTOMERS


        //SUPPLIERS
        
        
        //INVOICES
        //var tasks = RunAsync();
        //await Task.WhenAll(tasks);
        Console.ReadLine();
    }
}