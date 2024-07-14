using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MechRentSA.Shared
{
    public class ExcavatorDTO
    {
        /// <summary>
        /// Identificador único de la retroexcavadora.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Tipo de la retroexcavadora.
        /// </summary>
        [Required(ErrorMessage ="El tipo es un campo de carácter obligatorio")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Tarifa por hora de la retroexcavadora.
        /// </summary>
        [Required(ErrorMessage = "La tarifa por hora de la retroexcavadora es un campo de carácter obligatorio")]
        public decimal HourlyRate { get; set; }

        /// <summary>
        /// Intervalo de mantenimiento en horas.
        /// </summary>
        [Required(ErrorMessage = "El intervalo de mantenimiento en horas es un campo de carácter obligatorio")]
        public int MaintenanceInterval { get; set; }

        /// <summary>
        /// Total de horas trabajadas por la retroexcavadora.
        /// </summary>
        [Required(ErrorMessage = "El total de horas trabajadas por la retroexcavadora es un campo de carácter obligatorio")]
        public int TotalHoursWorked { get; set; }

        /// <summary>
        /// Horas en las que se realizó el último mantenimiento.
        /// </summary>
        [Required(ErrorMessage = "Las horas en las que se realizó el último mantenimiento es un campo de carácter obligatorio")]
        public int LastMaintenanceHours { get; set; }
    }

}
