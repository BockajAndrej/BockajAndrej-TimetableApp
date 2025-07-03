using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace application.DAL.Entities;

[Table("Employee")]
public partial class Employee : IEntity<string>
{
    [Key]
    [Column("id")]
    [StringLength(10)]
    public string Id { get; set; } = null!;

    [Column("firstName")]
    [StringLength(50)]
    public string FirstName { get; set; } = null!;

    [Column("lastName")]
    [StringLength(50)]
    public string LastName { get; set; } = null!;

    [Column("birthNumber")]
    [StringLength(11)]
    [Unicode(false)]
    public string BirthNumber { get; set; } = null!;

    [Column("birthDay")]
    public DateOnly BirthDay { get; set; }

    [InverseProperty("IdEmployeeNavigation")]
    public virtual ICollection<Cp> Cps { get; set; } = new List<Cp>();
}
