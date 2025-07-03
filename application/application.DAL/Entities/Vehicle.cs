using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace application.DAL.Entities;

[Table("Vehicle")]
public partial class Vehicle : IEntity<int>
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("vehicleName")]
    [StringLength(100)]
    public string VehicleName { get; set; } = null!;

    [InverseProperty("IdVehicleNavigation")]
    public virtual ICollection<Transport> Transports { get; set; } = new List<Transport>();
}
