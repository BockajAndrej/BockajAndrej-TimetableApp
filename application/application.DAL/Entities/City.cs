using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace application.DAL.Entities;

[Table("City")]
public partial class City
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("latitude", TypeName = "decimal(9, 6)")]
    public decimal Latitude { get; set; }

    [Column("longitude", TypeName = "decimal(9, 6)")]
    public decimal Longitude { get; set; }

    [Column("cityName")]
    [StringLength(100)]
    public string CityName { get; set; } = null!;

    [Column("stateName")]
    [StringLength(50)]
    public string StateName { get; set; } = null!;

    [InverseProperty("IdEndCityNavigation")]
    public virtual ICollection<Cp> CpIdEndCityNavigations { get; set; } = new List<Cp>();

    [InverseProperty("IdStartCityNavigation")]
    public virtual ICollection<Cp> CpIdStartCityNavigations { get; set; } = new List<Cp>();
}
