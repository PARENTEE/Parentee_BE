using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Parentee_BE.DAL.Data.Entities;

[Table("auth_identity")]
[Index("Provider", "ProviderUid", Name = "auth_identity_provider_provider_uid_key", IsUnique = true)]
public partial class AuthIdentityEntity
{
    [Key]
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("provider")]
    public string Provider { get; set; } = null!;

    [Column("provider_uid")]
    public string? ProviderUid { get; set; }

    [Column("password_hash")]
    public string? PasswordHash { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("AuthIdentities")]
    public virtual UserEntity User { get; set; } = null!;
}
