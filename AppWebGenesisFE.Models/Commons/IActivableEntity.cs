﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppWebGenesisFE.Models.Common
{
    public interface IActivableEntity
    {
        bool IsActive { get; set; }
    }
}
