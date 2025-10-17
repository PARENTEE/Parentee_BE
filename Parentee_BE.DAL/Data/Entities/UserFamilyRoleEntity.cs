using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Parentee_BE.DAL.Data.Enums;

namespace Parentee_BE.DAL.Data.Entities;

[Table("user_family_role")]
[Index("UserId", "FamilyId", Name = "user_family_role_user_id_family_id_key", IsUnique = true)]
public partial class UserFamilyRoleEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("family_id")]
    public Guid FamilyId { get; set; }

    [Column("role")]
    public FamilyRole Role { get; set; }
    
    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("FamilyId")]
    [InverseProperty("UserFamilyRoles")]
    public virtual FamilyEntity Family { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserFamilyRole")]
    public virtual UserEntity User { get; set; } = null!;
}
