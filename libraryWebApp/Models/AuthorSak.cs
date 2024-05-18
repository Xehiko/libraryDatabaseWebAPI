using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace libraryWebApp.Models;

[Table("author_sak")]
[Index("AuthorId", Name = "AuthorID_UNIQUE", IsUnique = true)]
public partial class AuthorSak
{
    [Key]
    [Column("AuthorID")]
    public int AuthorId { get; set; }

    [StringLength(30)]
    public string FirstName { get; set; } = null!;

    [StringLength(30)]
    public string LastName { get; set; } = null!;

    [StringLength(30)]
    public string MiddleName { get; set; } = null!;

    [Column(TypeName = "date")]
    public DateTime? BirthDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime? DeathDate { get; set; }

    [StringLength(1200)]
    public string? AuthorBriefBiography { get; set; }

    [ForeignKey("AuthorId")]
    [InverseProperty("Authors")]
    public virtual ICollection<BookSak> Books { get; set; } = new List<BookSak>();

    [ForeignKey("AuthorId")]
    [InverseProperty("Authors")]
    public virtual ICollection<PublishinghouseSak> PublishingHouses { get; set; } = new List<PublishinghouseSak>();
}
