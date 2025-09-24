using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models;

public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }

    // Foreign key to Customer
    public int CustomerId { get; set; }
    public Customer Customer { get; set; }

    //public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    public List<Product> Products { get; set; } = new();
}