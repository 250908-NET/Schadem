using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace Store.Models;

public class Order
{
    [Key]
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }

    // Foreign key to Customer
    public int CustomerId { get; set; }
    [JsonIgnore]
    public Customer Customer { get; set; }

    //public ICollection<OrderProduct> OrderProducts { get; set; } = new List<OrderProduct>();
    public List<Product> Products { get; set; } = new();
}