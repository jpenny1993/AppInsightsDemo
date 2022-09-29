using System;
using System.Collections.Generic;

namespace Pokedex
{
    public partial class Evolution
    {
        public int NationalNumber { get; set; }
        public int? EvolvesFrom { get; set; }
        public int? EvolvesAtLevel { get; set; }
        public int? EvolvesInto { get; set; }
        public bool? AlternateForms { get; set; }

        public virtual Pokemon? EvolvesFromNavigation { get; set; }
        public virtual Pokemon? EvolvesIntoNavigation { get; set; }
        public virtual Pokemon NationalNumberNavigation { get; set; } = null!;
    }
}
