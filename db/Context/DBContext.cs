using Microsoft.EntityFrameworkCore;

namespace EngineMonitoring.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Climb> Climbs { get; set; } = null!;
        public virtual DbSet<Crew> Crews { get; set; } = null!;
        public virtual DbSet<Cruise> Cruises { get; set; } = null!;
        public virtual DbSet<Decline> Declines { get; set; } = null!;
        public virtual DbSet<DetailsInfo> DetailsInfos { get; set; } = null!;
        public virtual DbSet<EngineDetail> EngineDetails { get; set; } = null!;
        public virtual DbSet<EngineEvent> EngineEvents { get; set; } = null!;
        public virtual DbSet<Operation> Operations { get; set; } = null!;
        public virtual DbSet<SwitchedOn> SwitchedOns { get; set; } = null!;
        public virtual DbSet<TakeOff> TakeOffs { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Climb>(entity =>
            {
                entity.ToTable("Climb");

                entity.HasOne(d => d.DetailsInfo)
                    .WithMany(p => p.Climbs)
                    .HasForeignKey(d => d.DetailsInfoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Climb__DetailsIn__3F466844");

                entity.HasOne(d => d.EngineEvent)
                    .WithMany(p => p.Climbs)
                    .HasForeignKey(d => d.EngineEventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Climb__EngineEve__3E52440B");

                entity.HasOne(d => d.Operation)
                    .WithMany(p => p.Climbs)
                    .HasForeignKey(d => d.OperationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Climb__Operation__3D5E1FD2");
            });

            modelBuilder.Entity<Crew>(entity =>
            {
                entity.ToTable("Crew");

                entity.Property(e => e.Captain)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Fo)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("FO");

                entity.Property(e => e.Technical)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Operation)
                    .WithMany(p => p.Crews)
                    .HasForeignKey(d => d.OperationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Crew__OperationI__4E88ABD4");
            });

            modelBuilder.Entity<Cruise>(entity =>
            {
                entity.ToTable("Cruise");

                entity.HasOne(d => d.DetailsInfo)
                    .WithMany(p => p.Cruises)
                    .HasForeignKey(d => d.DetailsInfoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cruise__DetailsI__44FF419A");

                entity.HasOne(d => d.EngineEvent)
                    .WithMany(p => p.Cruises)
                    .HasForeignKey(d => d.EngineEventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cruise__EngineEv__440B1D61");

                entity.HasOne(d => d.Operation)
                    .WithMany(p => p.Cruises)
                    .HasForeignKey(d => d.OperationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cruise__Operatio__4316F928");
            });

            modelBuilder.Entity<Decline>(entity =>
            {
                entity.ToTable("Decline");

                entity.HasOne(d => d.DetailsInfo)
                    .WithMany(p => p.Declines)
                    .HasForeignKey(d => d.DetailsInfoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Decline__Details__4AB81AF0");

                entity.HasOne(d => d.EngineEvent)
                    .WithMany(p => p.Declines)
                    .HasForeignKey(d => d.EngineEventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Decline__EngineE__49C3F6B7");

                entity.HasOne(d => d.Operation)
                    .WithMany(p => p.Declines)
                    .HasForeignKey(d => d.OperationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Decline__Operati__48CFD27E");
            });

            modelBuilder.Entity<DetailsInfo>(entity =>
            {
                entity.ToTable("DetailsInfo");

                entity.Property(e => e.ConfAtoff).HasColumnName("ConfAToff");

                entity.Property(e => e.ConfAton).HasColumnName("ConfATon");

                entity.Property(e => e.IsolationValve)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Mach).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.PackValve1)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PackValve2)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Paltitude)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("PAltitude");

                entity.Property(e => e.Tat)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("TAT");

                entity.Property(e => e.WingAi).HasColumnName("WingAI");
            });

            modelBuilder.Entity<EngineDetail>(entity =>
            {
                entity.ToTable("EngineDetail");

                entity.Property(e => e.Egt)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("EGT");

                entity.Property(e => e.Ff)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("FF");

                entity.Property(e => e.InletAi).HasColumnName("InletAI");

                entity.Property(e => e.N1).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.N2).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.OilPressure).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.OilTemp).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Vib)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.EngineEvent)
                    .WithMany(p => p.EngineDetails)
                    .HasForeignKey(d => d.EngineEventId)
                    .HasConstraintName("FK__EngineDet__Engin__2C3393D0");

                entity.HasOne(d => d.Operation)
                    .WithMany(p => p.EngineDetails)
                    .HasForeignKey(d => d.OperationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EngineDet__Opera__2B3F6F97");
            });

            modelBuilder.Entity<EngineEvent>(entity =>
            {
                entity.ToTable("EngineEvent");

                entity.Property(e => e.EventName)
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.ToTable("Operation");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Fuel)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Oat).HasColumnName("OAT");

                entity.Property(e => e.Pbarometrica)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Tow)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("TOW");
            });

            modelBuilder.Entity<SwitchedOn>(entity =>
            {
                entity.ToTable("SwitchedOn");

                entity.HasOne(d => d.EngineEvent)
                    .WithMany(p => p.SwitchedOns)
                    .HasForeignKey(d => d.EngineEventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SwitchedO__Engin__33D4B598");

                entity.HasOne(d => d.Operation)
                    .WithMany(p => p.SwitchedOns)
                    .HasForeignKey(d => d.OperationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SwitchedO__Opera__32E0915F");
            });

            modelBuilder.Entity<TakeOff>(entity =>
            {
                entity.ToTable("Takeoff");

                entity.HasOne(d => d.DetailsInfo)
                    .WithMany(p => p.TakeOffs)
                    .HasForeignKey(d => d.DetailsInfoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Takeoff__Details__398D8EEE");

                entity.HasOne(d => d.EngineEvent)
                    .WithMany(p => p.TakeOffs)
                    .HasForeignKey(d => d.EngineEventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Takeoff__EngineE__38996AB5");

                entity.HasOne(d => d.Operation)
                    .WithMany(p => p.TakeOffs)
                    .HasForeignKey(d => d.OperationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Takeoff__Operati__37A5467C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
