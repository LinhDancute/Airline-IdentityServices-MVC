﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Airline.ModelsService.Models
{
    public record AccountSession(string? Id, string? UserName, string? Email, string? Role);
}
