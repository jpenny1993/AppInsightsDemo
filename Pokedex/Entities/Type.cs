using System;
using System.Collections.Generic;

namespace Pokedex
{
    public partial class Type
    {
        public Type()
        {
            Moves = new HashSet<Move>();
            PokemonTypeSubtypes = new HashSet<PokemonType>();
            PokemonTypeTypes = new HashSet<PokemonType>();
        }

        public int TypeId { get; set; }
        public string? TypeName { get; set; }

        public virtual ICollection<Move> Moves { get; set; }
        public virtual ICollection<PokemonType> PokemonTypeSubtypes { get; set; }
        public virtual ICollection<PokemonType> PokemonTypeTypes { get; set; }
    }
}
