using projetoSistemaVocacao.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class EFCoreContext : DbContext
{
    public EFCoreContext(DbContextOptions<EFCoreContext> options) : base(options)
    // public EFCoreContext( )
    {
        
    }

    public DbSet<CandidatoModeloModel>  Candidatos { get; set; }
    public DbSet<EleitorModeloModel>  Eleitores { get; set; }
    public DbSet<EnqueteModeloModel>  Enquetes { get; set; }
    public DbSet<UsuarioModeloModel>  Usuarios { get; set; }
    public DbSet<VotacaoModeloModel>  Votacoes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=../utfpr.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder
        .Entity<CandidatoModeloModel>(
            eb =>
            {
                eb.HasKey(pk => pk.CandidatoID);
            });
        modelBuilder
        .Entity<EleitorModeloModel>(
            eb =>
            {
                eb.HasKey(pk => pk.EleitorID);
            });
        modelBuilder
        .Entity<EnqueteModeloModel>(
            eb =>
            {
                eb.HasKey(pk => pk.EnqueteID);
            });
        modelBuilder
        .Entity<UsuarioModeloModel>(
            eb =>
            {
                eb.HasKey(pk => pk.UsuarioID);
            });
        modelBuilder
        .Entity<VotacaoModeloModel>(
            eb =>
            {
                eb.HasKey(pk => pk.VotacaoID);
            });
    }
}
