using Microsoft.EntityFrameworkCore;
using backend.Entities;

namespace backend.DBContext;

public partial class MovieCatalogContext : DbContext
{
    public MovieCatalogContext()
    {
    }

    public MovieCatalogContext(DbContextOptions<MovieCatalogContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cast> Casts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MovieCast> MovieCasts { get; set; }

    public virtual DbSet<MovieCategory> MovieCategories { get; set; }

    public virtual DbSet<MovieGenre> MovieGenres { get; set; }

    public virtual DbSet<MovieLike> MovieLikes { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Watchlist> Watchlists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cast>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Cast__3214EC0776E0C069");

            entity.ToTable("Cast");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC070FAB056E");

            entity.ToTable("Category");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Genre__3214EC07F7DD8936");

            entity.ToTable("Genre");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Movie__3214EC074B4DC7FA");

            entity.ToTable("Movie");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Director)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Duration)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Premiere).HasColumnType("datetime");
            entity.Property(e => e.Trailer)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MovieCast>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MovieCas__3214EC07A505C2AB");

            entity.ToTable("MovieCast");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.IdCastNavigation).WithMany(p => p.MovieCasts)
                .HasForeignKey(d => d.IdCast)
                .HasConstraintName("FK__MovieCast__IdCas__3D5E1FD2");

            entity.HasOne(d => d.IdMovieNavigation).WithMany(p => p.MovieCasts)
                .HasForeignKey(d => d.IdMovie)
                .HasConstraintName("FK__MovieCast__IdMov__3C69FB99");
        });

        modelBuilder.Entity<MovieCategory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MovieCat__3214EC07A0D7A05C");

            entity.ToTable("MovieCategory");

            entity.HasOne(d => d.IdCategoryNavigation).WithMany(p => p.MovieCategories)
                .HasForeignKey(d => d.IdCategory)
                .HasConstraintName("FK__MovieCate__IdCat__3B75D760");

            entity.HasOne(d => d.IdMovieNavigation).WithMany(p => p.MovieCategories)
                .HasForeignKey(d => d.IdMovie)
                .HasConstraintName("FK__MovieCate__IdMov__3A81B327");
        });

        modelBuilder.Entity<MovieGenre>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MovieGen__3214EC078D42D59A");

            entity.ToTable("MovieGenre");

            entity.HasOne(d => d.IdGenreNavigation).WithMany(p => p.MovieGenres)
                .HasForeignKey(d => d.IdGenre)
                .HasConstraintName("FK__MovieGenr__IdGen__4316F928");

            entity.HasOne(d => d.IdMovieNavigation).WithMany(p => p.MovieGenres)
                .HasForeignKey(d => d.IdMovie)
                .HasConstraintName("FK__MovieGenr__IdMov__4222D4EF");
        });

        modelBuilder.Entity<MovieLike>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MovieLik__3214EC0711DF89D4");

            entity.ToTable("MovieLike");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.IdMovieNavigation).WithMany(p => p.MovieLikes)
                .HasForeignKey(d => d.IdMovie)
                .HasConstraintName("FK__MovieLike__IdMov__44FF419A");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.MovieLikes)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__MovieLike__IdUse__440B1D61");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Review__3214EC071B386D36");

            entity.ToTable("Review");

            entity.Property(e => e.Comment)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.IdMovieNavigation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.IdMovie)
                .HasConstraintName("FK__Review__IdMovie__3E52440B");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Reviews)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__Review__IdUser__3F466844");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Rol__3214EC07B58A0EF6");

            entity.ToTable("Rol");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07C715992C");

            entity.ToTable("User");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.IdRol)
                .HasConstraintName("FK__User__IdRol__398D8EEE");
        });

        modelBuilder.Entity<Watchlist>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Watchlis__3214EC0757F4923D");

            entity.ToTable("Watchlist");

            entity.Property(e => e.CreatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.IdMovieNavigation).WithMany(p => p.Watchlists)
                .HasForeignKey(d => d.IdMovie)
                .HasConstraintName("FK__Watchlist__IdMov__412EB0B6");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Watchlists)
                .HasForeignKey(d => d.IdUser)
                .HasConstraintName("FK__Watchlist__IdUse__403A8C7D");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
