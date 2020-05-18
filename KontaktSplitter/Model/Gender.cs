﻿using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KontaktSplitter.Model
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Gender
    {
        UNKNOWN,
        MALE,
        FEMALE,
        DIVERSE
    }
}
