﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Vizvezetek.API.Models;
using Vizvezetek.API.DTOs;


namespace Vizvezetek.API.Models;

[Index(nameof(hely_id), Name = "hely_id")]
[Index(nameof(szerelo_id), Name = "szerelo_id")]
public partial class Munkalap
{
    [Key]
    [Column(TypeName = "int(11)")]
    public int id { get; set; }

    public DateTime beadas_datum { get; set; }

    public DateTime javitas_datum { get; set; }

    [Column(TypeName = "int(11)")]
    public int hely_id { get; set; }

    [Column(TypeName = "int(11)")]
    public int szerelo_id { get; set; }

    [Column(TypeName = "int(11)")]
    public int munkaora { get; set; }

    [Column(TypeName = "int(11)")]
    public int anyagar { get; set; }

    [ForeignKey(nameof(hely_id))]
    [InverseProperty(nameof(Hely.Munkalapok))] // A megfelelő névnek kell lennie
    public virtual Hely hely { get; set; } = null!;

    [ForeignKey(nameof(szerelo_id))]
    [InverseProperty(nameof(Szerelo.munkalap))]
    public virtual Szerelo szerelo { get; set; } = null!;
}
