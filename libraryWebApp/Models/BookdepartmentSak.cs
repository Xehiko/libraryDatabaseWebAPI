using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace libraryWebApp.Models;

[Table("bookdepartment_sak")]
[Index("DepId", Name = "DepID_UNIQUE", IsUnique = true)]
public partial class BookdepartmentSak
{
    [Key]
    [Column("DepID")]
    public int DepId { get; set; }

    [StringLength(66)]
    public string DepName { get; set; } = null!;

    [StringLength(20)]
    public string DepPhoneNumber { get; set; } = null!;

    [InverseProperty("BookDepartment")]
    public virtual ICollection<BookSak> BookSaks { get; set; } = new List<BookSak>();

    [InverseProperty("Dep")]
    public virtual ICollection<BookingSak> BookingSaks { get; set; } = new List<BookingSak>();
}
