using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.Core.Enums;

namespace Ultatel.Core.Dtos
{
    public class StudentDto
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public GenderValue Gender { get; set; }
        public int Age { get; set; }
    }
}
