using System;
using System.Collections.Generic;

namespace Pokedex
{
    public partial class AlternateForm
    {
        public int? NationalNumber { get; set; }
        public int AlternateFormId { get; set; }
        public string? AlternateFormName { get; set; }
        public int? AlternateFormType { get; set; }
        public string? ItemRequired { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }

        public virtual Pokemon? NationalNumberNavigation { get; set; }
    }
}
