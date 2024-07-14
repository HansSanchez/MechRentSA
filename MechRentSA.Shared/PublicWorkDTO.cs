using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechRentSA.Shared
{
    public class PublicWorkDTO
    {
        /// <summary>
        /// Identificador único de la obra pública.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la obra pública.
        /// </summary>
        [Required(ErrorMessage = "El nombre es un campo de carácter obligatorio")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Horas estimadas para la obra pública.
        /// </summary>
        [Required(ErrorMessage = "Las horas estimadas para la obra pública es un campo de carácter obligatorio")]
        public int EstimatedHours { get; set; }

        /// <summary>
        /// Lista de registros de trabajo de retroexcavadoras asociadas a la obra pública.
        /// </summary>
        public List<ExcavatorWorkLogDTO> ExcavatorWorkLogs { get; set; } = new List<ExcavatorWorkLogDTO>();
    }

}
