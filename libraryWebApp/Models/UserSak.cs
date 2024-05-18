using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace libraryWebApp.Models;

[Table("user_sak")]
[Index("Id", Name = "ID_UNIQUE", IsUnique = true)]
[Index("Login", Name = "Login_UNIQUE", IsUnique = true)]
public partial class UserSak
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    [StringLength(25)]
    public string Login { get; set; } = null!;

    [StringLength(25)]
    public string Password { get; set; } = null!;

    [StringLength(15)]
    public string Role { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<ReaderSak> ReaderSaks { get; set; } = new List<ReaderSak>();
}
