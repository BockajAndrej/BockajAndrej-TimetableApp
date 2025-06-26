using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace application.DAL.Entities;

[Table("CP")]
public partial class Cp
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_employee")]
    [StringLength(10)]
    public string IdEmployee { get; set; } = null!;

    [Column("id_startCity")]
    public int IdStartCity { get; set; }

    [Column("id_endCity")]
    public int IdEndCity { get; set; }

    [Column("creationDate")]
    public DateOnly CreationDate { get; set; }

    [Column("startTime")]
    public DateTimeOffset StartTime { get; set; }

    [Column("endTime")]
    public DateTimeOffset EndTime { get; set; }

    [Column("cpState")]
    [StringLength(100)]
    [Unicode(false)]
    public string CpState { get; set; } = null!;

    [ForeignKey("IdEmployee")]
    [InverseProperty("Cps")]
    public virtual Employee IdEmployeeNavigation { get; set; } = null!;

    [ForeignKey("IdEndCity")]
    [InverseProperty("CpIdEndCityNavigations")]
    public virtual City IdEndCityNavigation { get; set; } = null!;

    [ForeignKey("IdStartCity")]
    [InverseProperty("CpIdStartCityNavigations")]
    public virtual City IdStartCityNavigation { get; set; } = null!;

    [InverseProperty("IdCpNavigation")]
    public virtual ICollection<Transport> Transports { get; set; } = new List<Transport>();
}
