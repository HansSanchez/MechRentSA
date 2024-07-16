using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechRentSA.Shared
{
    public class AddWorkHoursDTO
    {
        public int PublicWorkId { get; set; }
        public int ExcavatorId { get; set; }
        public int HoursWorked { get; set; }
    }

}
