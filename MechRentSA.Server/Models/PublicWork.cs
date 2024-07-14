using System;
using System.Collections.Generic;

namespace MechRentSA.Server.Models;

/// <summary>
/// Representa una obra pública.
/// </summary>
public partial class PublicWork
{
    /// <summary>
    /// Obtiene o establece el identificador único de la obra pública.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Obtiene o establece el nombre de la obra pública.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Obtiene o establece las horas estimadas para la obra pública.
    /// </summary>
    public int EstimatedHours { get; set; }

    /// <summary>
    /// Obtiene o establece la lista de registros de trabajo de retroexcavadoras asociadas a la obra.
    /// </summary>
    public virtual ICollection<ExcavatorWorkLog> ExcavatorWorkLogs { get; set; } = new List<ExcavatorWorkLog>();
}
