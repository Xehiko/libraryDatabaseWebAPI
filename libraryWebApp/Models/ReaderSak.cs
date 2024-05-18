using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace libraryWebApp.Models;

[PrimaryKey("ReaderId", "UserId")]
[Table("reader_sak")]
[Index("ReaderId", Name = "ReaderID_UNIQUE", IsUnique = true)]
[Index("UserId", Name = "fk_Reader_SAK_User1_idx")]
public partial class ReaderSak
{
    [Key]
    [Column("ReaderID")]
    public int ReaderId { get; set; }

    [StringLength(30)]
    public string FirstName { get; set; } = null!;

    [StringLength(30)]
    public string LastName { get; set; } = null!;

    [StringLength(30)]
    public string MiddleName { get; set; } = null!;

    public int NumberOfBookLoans { get; set; }

    [Column(TypeName = "date")]
    public DateTime BirthDate { get; set; }

    [StringLength(255)]
    public string HomeAddress { get; set; } = null!;

    [StringLength(20)]
    public string PhoneNumber { get; set; } = null!;

    [Key]
    [Column("User_ID")]
    public int UserId { get; set; }

    public virtual ICollection<BookingSak> BookingSaks { get; set; } = new List<BookingSak>();

    [ForeignKey("UserId")]
    [InverseProperty("ReaderSaks")]
    public virtual UserSak User { get; set; } = null!;
}
