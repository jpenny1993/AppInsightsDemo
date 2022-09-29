using System;
using System.Collections.Generic;

namespace Pokedex
{
    public partial class Move
    {
        public int? TypeId { get; set; }
        public int MoveId { get; set; }
        public string? MoveName { get; set; }
        public string? MoveCategory { get; set; }
        public int? MovePower { get; set; }
        public int? MovePp { get; set; }
        public int? MoveAccuracy { get; set; }

        public virtual Type? Type { get; set; }
    }
}
