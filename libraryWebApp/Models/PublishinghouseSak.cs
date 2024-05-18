using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace libraryWebApp.Models;

[Table("publishinghouse_sak")]
[Index("PublishingHouseId", Name = "PublisherHouseID_UNIQUE", IsUnique = true)]
public partial class PublishinghouseSak
{
    [Key]
    [Column("PublishingHouseID")]
    public int PublishingHouseId { get; set; }

    [StringLength(66)]
    public string Name { get; set; } = null!;

    [StringLength(20)]
    public string HousePhoneNumber { get; set; } = null!;

    [StringLength(255)]
    public string Address { get; set; } = null!;

    [InverseProperty("PublishingHouse")]
    public virtual ICollection<BookSak> BookSaks { get; set; } = new List<BookSak>();

    [ForeignKey("PublishingHouseId")]
    [InverseProperty("PublishingHouses")]
    public virtual ICollection<AuthorSak> Authors { get; set; } = new List<AuthorSak>();
}
