using System;
using System.Collections.Generic;

namespace Pokedex
{
    public partial class PokemonType
    {
        public int NationalNumber { get; set; }
        public int? TypeId { get; set; }
        public int? SubtypeId { get; set; }

        public virtual Pokemon NationalNumberNavigation { get; set; } = null!;
        public virtual Type? Subtype { get; set; }
        public virtual Type? Type { get; set; }
    }
}
