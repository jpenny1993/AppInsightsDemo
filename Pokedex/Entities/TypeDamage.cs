using System;
using System.Collections.Generic;

namespace Pokedex.Entities
{
    public partial class TypeDamage
    {
        public int? DamagedTypeId { get; set; }
        public int? AttackerTypeId { get; set; }
        public double? DamageMultiplier { get; set; }

        public virtual Type? AttackerType { get; set; }
        public virtual Type? DamagedType { get; set; }
    }
}
