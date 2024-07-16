using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MechRentSA.Shared
{
    public class ExcavatorWorkLogDTO
    {
        /// <summary>
        /// Identificador único del registro de trabajo.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Identificador de la obra pública asociada.
        /// </summary>
        public int PublicWorkId { get; set; }

        /// <summary>
        /// Identificador de la retroexcavadora asociada.
        /// </summary>
        public int ExcavatorId { get; set; }

        /// <summary>
        /// Horas trabajadas por la retroexcavadora en esta obra pública.
        /// </summary>
        public int HoursWorked { get; set; }

        /// <summary>
        /// Fecha en que se realizó el trabajo.
        /// </summary>
        public DateTime WorkDate { get; set; }

        /// <summary>
        /// Excavadora con la que se realizó el trabajo.
        /// </summary>
        public ExcavatorDTO ExcavatorDTO { get; set; } = new ExcavatorDTO();

        /// <summary>
        /// Obra pública en la que se realizó el trabajo.
        /// </summary>
        public PublicWorkDTO PublicWorkDTO { get; set; } = new PublicWorkDTO();
    }

}
