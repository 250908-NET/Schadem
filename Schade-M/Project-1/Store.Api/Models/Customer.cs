using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // to customize the column names

namespace Store.Models;

public class Customer
{
    // Fields
    [Key]
    //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    

    [Required]
    [MaxLength(50)]
    //[Column("FirstName")] // we could, but it's not necessary here
    public string FirstName { get; set; }

    [Required, MaxLength(50)]
    public string LastName { get; set; }
    
    [Required]
    public string Email { get; set; }

    public List<Order> Orders { get; set; } = new();
}