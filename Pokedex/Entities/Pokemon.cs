using System;
using System.Collections.Generic;

namespace Pokedex
{
    public partial class Pokemon
    {
        public Pokemon()
        {
            AlternateForms = new HashSet<AlternateForm>();
            EvolutionEvolvesFromNavigations = new HashSet<Evolution>();
            EvolutionEvolvesIntoNavigations = new HashSet<Evolution>();
        }

        public int NationalNumber { get; set; }
        public int? RegionalNumber { get; set; }
        public string? Name { get; set; }
        public double? Height { get; set; }
        public double? Weight { get; set; }

        public virtual BaseStat? BaseStat { get; set; }
        public virtual Evolution? EvolutionNationalNumberNavigation { get; set; }
        public virtual PokemonType? PokemonType { get; set; }
        public virtual ICollection<AlternateForm> AlternateForms { get; set; }
        public virtual ICollection<Evolution> EvolutionEvolvesFromNavigations { get; set; }
        public virtual ICollection<Evolution> EvolutionEvolvesIntoNavigations { get; set; }
    }
}
