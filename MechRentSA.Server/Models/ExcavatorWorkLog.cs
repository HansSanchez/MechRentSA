using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MechRentSA.Server.Models;

/// <summary>
/// Representa un registro de trabajo de una retroexcavadora.
/// </summary>
public partial class ExcavatorWorkLog
{
    /// <summary>
    /// Obtiene o establece el identificador único del registro de trabajo.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Obtiene o establece el identificador de la obra pública asociada.
    /// </summary>
    public int PublicWorkId { get; set; }

    /// <summary>
    /// Obtiene o establece el identificador de la retroexcavadora asociada.
    /// </summary>
    public int ExcavatorId { get; set; }

    /// <summary>
    /// Obtiene o establece las horas trabajadas por la retroexcavadora en esta obra pública.
    /// </summary>
    public int HoursWorked { get; set; }

    /// <summary>
    /// Obtiene o establece la fecha en que se realizó el trabajo.
    /// </summary>
    public DateTime WorkDate { get; set; }

    /// <summary>
    /// Obtiene o establece la retroexcavadora asociada a este registro de trabajo.
    /// </summary>
    public virtual Excavator Excavator { get; set; } = null!;

    /// <summary>
    /// Obtiene o establece la obra pública asociada a este registro de trabajo.
    /// </summary>
    public virtual PublicWork PublicWork { get; set; } = null!;
}