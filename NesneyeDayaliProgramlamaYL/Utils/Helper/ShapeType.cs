﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NesneyeDayaliProgramlamaYL.Utils.Helper
{
    public enum ShapeType
    {
        [JsonConverter(typeof(StringEnumConverter))]
        None,
        Circle,
        Rectangle,
        Triangle,
        Hexagon
    }
}
