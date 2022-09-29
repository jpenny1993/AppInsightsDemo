using System;
using System.Collections.Generic;

namespace Pokedex
{
    public partial class PokemonAbility
    {
        public int? NationalNumber { get; set; }
        public int? AbilityId { get; set; }
        public bool? HiddenAbility { get; set; }

        public virtual Ability? Ability { get; set; }
        public virtual Pokemon? NationalNumberNavigation { get; set; }
    }
}
