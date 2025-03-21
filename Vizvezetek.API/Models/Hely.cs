﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Vizvezetek.API.Models;
using Vizvezetek.API.DTOs;


namespace Vizvezetek.API.Models;

public partial class Hely
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    [StringLength(50)]
    public string telepules { get; set; } = null!;

    [StringLength(75)]
    public string utca { get; set; } = null!;

    [InverseProperty("hely")]
    public virtual ICollection<Munkalap> munkalap { get; set; } = new List<Munkalap>();
}
