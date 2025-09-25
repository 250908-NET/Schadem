using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.Models;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }

    // Navigation property for many-to-many
    //public ICollection<Order> Orders { get; set; }
    public List<Order> Orders { get; set; } = new();
}