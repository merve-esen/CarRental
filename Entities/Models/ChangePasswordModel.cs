﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class ChangePasswordModel
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string OldPassword { get; set; }
    }
}
