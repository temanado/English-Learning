using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace English_Learning.Models
{
    public class Language
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public CultureInfo CultureInfo { get; set; }
    }
}
