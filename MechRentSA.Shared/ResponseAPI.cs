using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MechRentSA.Shared
{
    public class ResponseAPI<T>
    {
        public bool IsSuccessful { get; set; }
        public T? Value { get; set; }
        public string? Message { get; set; }

    }
}
