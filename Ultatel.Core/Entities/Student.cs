﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ultatel.Core.Entities.Common;
using Ultatel.Core.Enums;

namespace Ultatel.Core.Entities
{
    public class Student :AudiatableEntity
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Country { get; set; }
        public GenderValue Gender { get; set; }
        public string AddedByUserId { get; set; }
        public ApplicationUser AddedByUser { get; set; }
    }
}
