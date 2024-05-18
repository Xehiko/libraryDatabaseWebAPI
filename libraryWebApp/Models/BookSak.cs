using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace libraryWebApp.Models;

[Table("book_sak")]
[Index("BookId", Name = "BookID_UNIQUE", IsUnique = true)]
[Index("BookDepartmentId", Name = "fk_Book_SAK_BookDepartment_SAK1_idx")]
[Index("PublishingHouseId", Name = "fk_Book_SAK_PublishingHouse_SAK1_idx")]
public partial class BookSak
{
    [Key]
    [Column("BookID")]
    public int BookId { get; set; }

    [Column("BookDepartmentID")]
    public int BookDepartmentId { get; set; }

    [Column("PublishingHouseID")]
    public int PublishingHouseId { get; set; }

    [StringLength(66)]
    public string Name { get; set; } = null!;

    public int Price { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ReceiptDate { get; set; }

    public int InventoryNumber { get; set; }

    [ForeignKey("BookDepartmentId")]
    [InverseProperty("BookSaks")]
    public virtual BookdepartmentSak BookDepartment { get; set; } = null!;

    [InverseProperty("Book")]
    public virtual ICollection<BookingSak> BookingSaks { get; set; } = new List<BookingSak>();

    [ForeignKey("PublishingHouseId")]
    [InverseProperty("BookSaks")]
    public virtual PublishinghouseSak PublishingHouse { get; set; } = null!;

    [ForeignKey("BookId")]
    [InverseProperty("Books")]
    public virtual ICollection<AuthorSak> Authors { get; set; } = new List<AuthorSak>();
}
