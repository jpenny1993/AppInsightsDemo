using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Pokedex.Entities;

namespace Pokedex
{
    public partial class PokedexDbContext : DbContext
    {
        public PokedexDbContext()
        {
        }

        public PokedexDbContext(DbContextOptions<PokedexDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ability> Abilities { get; set; } = null!;
        public virtual DbSet<AlternateForm> AlternateForms { get; set; } = null!;
        public virtual DbSet<BaseStat> BaseStats { get; set; } = null!;
        public virtual DbSet<Evolution> Evolutions { get; set; } = null!;
        public virtual DbSet<Move> Moves { get; set; } = null!;
        public virtual DbSet<Pokemon> Pokemons { get; set; } = null!;
        public virtual DbSet<PokemonAbility> PokemonAbilities { get; set; } = null!;
        public virtual DbSet<PokemonMove> PokemonMoves { get; set; } = null!;
        public virtual DbSet<PokemonType> PokemonTypes { get; set; } = null!;
        public virtual DbSet<Type> Types { get; set; } = null!;
        public virtual DbSet<TypeDamage> TypeDamages { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("REMOVED");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ability>(entity =>
            {
                entity.ToTable("Ability");

                entity.Property(e => e.AbilityId).ValueGeneratedNever();

                entity.Property(e => e.AbilityName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<AlternateForm>(entity =>
            {
                entity.Property(e => e.AlternateFormId).ValueGeneratedNever();

                entity.Property(e => e.AlternateFormName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ItemRequired)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.NationalNumberNavigation)
                    .WithMany(p => p.AlternateForms)
                    .HasForeignKey(d => d.NationalNumber)
                    .HasConstraintName("FK__Alternate__Natio__6A30C649");
            });

            modelBuilder.Entity<BaseStat>(entity =>
            {
                entity.HasKey(e => e.NationalNumber)
                    .HasName("PK__BaseStat__FEA173C393D6FFB9");

                entity.Property(e => e.NationalNumber).ValueGeneratedNever();

                entity.Property(e => e.Hp).HasColumnName("HP");

                entity.HasOne(d => d.NationalNumberNavigation)
                    .WithOne(p => p.BaseStat)
                    .HasForeignKey<BaseStat>(d => d.NationalNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BaseStats__Natio__5EBF139D");
            });

            modelBuilder.Entity<Evolution>(entity =>
            {
                entity.HasKey(e => e.NationalNumber)
                    .HasName("PK__Evolutio__FEA173C35C525D08");

                entity.ToTable("Evolution");

                entity.Property(e => e.NationalNumber).ValueGeneratedNever();

                entity.HasOne(d => d.EvolvesFromNavigation)
                    .WithMany(p => p.EvolutionEvolvesFromNavigations)
                    .HasForeignKey(d => d.EvolvesFrom)
                    .HasConstraintName("FK__Evolution__Evolv__66603565");

                entity.HasOne(d => d.EvolvesIntoNavigation)
                    .WithMany(p => p.EvolutionEvolvesIntoNavigations)
                    .HasForeignKey(d => d.EvolvesInto)
                    .HasConstraintName("FK__Evolution__Evolv__6754599E");

                entity.HasOne(d => d.NationalNumberNavigation)
                    .WithOne(p => p.EvolutionNationalNumberNavigation)
                    .HasForeignKey<Evolution>(d => d.NationalNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Evolution__Natio__656C112C");
            });

            modelBuilder.Entity<Move>(entity =>
            {
                entity.Property(e => e.MoveId).ValueGeneratedNever();

                entity.Property(e => e.MoveCategory)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MoveName)
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.MovePp).HasColumnName("MovePP");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Moves)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK__Moves__TypeId__778AC167");
            });

            modelBuilder.Entity<Pokemon>(entity =>
            {
                entity.HasKey(e => e.NationalNumber)
                    .HasName("PK__Pokemon__FEA173C3F513238C");

                entity.ToTable("Pokemon");

                entity.Property(e => e.NationalNumber).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .HasMaxLength(11)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PokemonAbility>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("PokemonAbility");

                entity.HasOne(d => d.Ability)
                    .WithMany()
                    .HasForeignKey(d => d.AbilityId)
                    .HasConstraintName("FK__PokemonAb__Abili__6D0D32F4");

                entity.HasOne(d => d.NationalNumberNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.NationalNumber)
                    .HasConstraintName("FK__PokemonAb__Natio__6C190EBB");
            });

            modelBuilder.Entity<PokemonMove>(entity =>
            {
                entity.HasNoKey();

                entity.HasOne(d => d.Move)
                    .WithMany()
                    .HasForeignKey(d => d.MoveId)
                    .HasConstraintName("FK__PokemonMo__MoveI__797309D9");
            });

            modelBuilder.Entity<PokemonType>(entity =>
            {
                entity.HasKey(e => e.NationalNumber)
                    .HasName("PK__PokemonT__FEA173C3AE58DA04");

                entity.Property(e => e.NationalNumber).ValueGeneratedNever();

                entity.HasOne(d => d.NationalNumberNavigation)
                    .WithOne(p => p.PokemonType)
                    .HasForeignKey<PokemonType>(d => d.NationalNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PokemonTy__Natio__6FE99F9F");

                entity.HasOne(d => d.Subtype)
                    .WithMany(p => p.PokemonTypeSubtypes)
                    .HasForeignKey(d => d.SubtypeId)
                    .HasConstraintName("FK__PokemonTy__Subty__71D1E811");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.PokemonTypeTypes)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("FK__PokemonTy__TypeI__70DDC3D8");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.ToTable("Type");

                entity.Property(e => e.TypeId).ValueGeneratedNever();

                entity.Property(e => e.TypeName)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TypeDamage>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("TypeDamage");

                entity.HasOne(d => d.AttackerType)
                    .WithMany()
                    .HasForeignKey(d => d.AttackerTypeId)
                    .HasConstraintName("FK__TypeDamag__Attac__74AE54BC");

                entity.HasOne(d => d.DamagedType)
                    .WithMany()
                    .HasForeignKey(d => d.DamagedTypeId)
                    .HasConstraintName("FK__TypeDamag__Damag__73BA3083");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
