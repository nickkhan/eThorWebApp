﻿using System;
using System.Collections.Generic;
using System.Text;

namespace eThorWebApp.Shared.Models
{
    public class UserInfo
    {
        public bool IsAuthenticated { get; set; }

        public string Name { get; set; }
        public string Claims { get; set; }

    }
}
