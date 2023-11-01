using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace CSV_Parser;

public partial class CsvParserDataContext : DbContext
{
    readonly string connectionString;
    public CsvParserDataContext(string ConnectionString)
    {
        this.connectionString = ConnectionString;
    }

    public CsvParserDataContext(DbContextOptions<CsvParserDataContext> options)
        : base(options)
    {
    }

    public virtual DbSet<OrdersHistory> OrdersHistories { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer(/*"Server=(localdb)\\mssqllocaldb;Database=CsvParserData;Trusted_Connection=True;"*/ connectionString);

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<OrdersHistory>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrdersHi__3214EC273BA22607");

            entity.ToTable("OrdersHistory");

            entity.HasIndex(e => new { e.PulocationId, e.TripDistance }, "IX_PULocationID_TripDistance_TpepDuration");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DolocationId).HasColumnName("DOLocationID");
            entity.Property(e => e.FareAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.PulocationId).HasColumnName("PULocationID");
            entity.Property(e => e.StoreAndFwdFlag).HasMaxLength(3);
            entity.Property(e => e.TipAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.TpepDropoffDatetime).HasColumnType("datetime");
            entity.Property(e => e.TpepPickupDatetime).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
