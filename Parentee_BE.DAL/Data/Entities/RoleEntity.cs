using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Parentee_BE.DAL.Data.Entities;

[Table("roles")]
public sealed class RoleEntity
{
    public const string Admin = "Admin";
    public const string User = "User";

    internal const int AdminId = 1;
    internal const int UserId = 2;
    
    [Key]
    [Column("id")]
    public int Id { get; set; }
    
    [Required]
    [Column("name")]
    [MaxLength(50)]
    public string Name { get; set; }
}