using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace libraryWebApp.Models;

[Table("booking_sak")]
[Index("BookId", Name = "BookID_idx")]
[Index("BookingId", Name = "BookingID_UNIQUE", IsUnique = true)]
[Index("DepId", Name = "DepID_idx")]
[Index("ReaderId", Name = "ReaderID_idx")]
public partial class BookingSak
{
    [Key]
    [Column("BookingID")]
    public int BookingId { get; set; }

    [Column("DepID")]
    public int DepId { get; set; }

    [Column("ReaderID")]
    public int ReaderId { get; set; }

    [Column("BookID")]
    public int BookId { get; set; }

    [Column(TypeName = "date")]
    public DateTime LoanDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime ReturnDate { get; set; }

    [ForeignKey("BookId")]
    [InverseProperty("BookingSaks")]
    public virtual BookSak Book { get; set; } = null!;

    [ForeignKey("DepId")]
    [InverseProperty("BookingSaks")]
    public virtual BookdepartmentSak Dep { get; set; } = null!;

    public virtual ReaderSak Reader { get; set; } = null!;
}
