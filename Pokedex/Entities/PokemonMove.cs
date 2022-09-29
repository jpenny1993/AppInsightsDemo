using System;
using System.Collections.Generic;

namespace Pokedex
{
    public partial class PokemonMove
    {
        public int? NationalNumber { get; set; }
        public int? MoveId { get; set; }

        public virtual Move? Move { get; set; }
    }
}
