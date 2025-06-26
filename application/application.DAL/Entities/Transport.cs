using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace application.DAL.Entities;

[Table("Transport")]
public partial class Transport
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("id_cp")]
    public int IdCp { get; set; }

    [Column("id_vehicle")]
    public int IdVehicle { get; set; }

    [ForeignKey("IdCp")]
    [InverseProperty("Transports")]
    public virtual Cp IdCpNavigation { get; set; } = null!;

    [ForeignKey("IdVehicle")]
    [InverseProperty("Transports")]
    public virtual Vehicle IdVehicleNavigation { get; set; } = null!;
}
