using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.Core.Enums;

namespace Ultatel.Core.Dtos
{
    public class StudentSearchDto
    {
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? Gender { get; set; }
        public int? AgeFrom { get; set; }
        public int? AgeTo { get; set; }
    }
}
