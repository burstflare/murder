﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murder.Utilities.Attributes
{
    public class CustomNameAttribute : Attribute
    {
        public string Name;

        public CustomNameAttribute(string name)
        {
            Name = name;
        }
    }
}
