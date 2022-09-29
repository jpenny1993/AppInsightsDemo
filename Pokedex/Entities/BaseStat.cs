using System;
using System.Collections.Generic;

namespace Pokedex
{
    public partial class BaseStat
    {
        public int NationalNumber { get; set; }
        public int? Hp { get; set; }
        public int? Attack { get; set; }
        public int? Defence { get; set; }
        public int? SpecialAttack { get; set; }
        public int? SpecialDefence { get; set; }
        public int? Speed { get; set; }

        public virtual Pokemon NationalNumberNavigation { get; set; } = null!;
    }
}
