using System;
using System.Collections.Generic;

namespace MechRentSA.Server.Models;

/// <summary>
/// Representa una retroexcavadora en la flota.
/// </summary>
public partial class Excavator
{
    /// <summary>
    /// Obtiene o establece el identificador único de la retroexcavadora.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Obtiene o establece el tipo de la retroexcavadora.
    /// </summary>
    public string Type { get; set; } = string.Empty;

    /// <summary>
    /// Obtiene o establece el costo por hora de la retroexcavadora.
    /// </summary>
    public decimal HourlyRate { get; set; }

    /// <summary>
    /// Obtiene o establece el intervalo de mantenimiento en horas.
    /// </summary>
    public int MaintenanceInterval { get; set; }

    /// <summary>
    /// Obtiene o establece el total de horas trabajadas por la retroexcavadora.
    /// </summary>
    public int TotalHoursWorked { get; set; }

    /// <summary>
    /// Obtiene o establece las horas en las que se realizó el último mantenimiento.
    /// </summary>
    public int LastMaintenanceHours { get; set; }

    /// <summary>
    /// Obtiene o establece la lista de registros de trabajo de retroexcavadoras asociadas a la obra.
    /// </summary>
    public virtual ICollection<ExcavatorWorkLog> ExcavatorWorkLogs { get; set; } = new List<ExcavatorWorkLog>();


}