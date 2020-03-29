using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace istp_laba1
{
    public partial class MydbContext : DbContext
    {
        public MydbContext()
        {
        }

        public MydbContext(DbContextOptions<MydbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AbonementTypes> AbonementTypes { get; set; }
        public virtual DbSet<Abonements> Abonements { get; set; }
        public virtual DbSet<Classrooms> Classrooms { get; set; }
        public virtual DbSet<LessonTeacher> LessonTeacher { get; set; }
        public virtual DbSet<Lessons> Lessons { get; set; }
        public virtual DbSet<StudentAbonements> StudentAbonements { get; set; }
        public virtual DbSet<Students> Students { get; set; }
        public virtual DbSet<Styles> Styles { get; set; }
        public virtual DbSet<TeacherStyles> TeacherStyles { get; set; }
        public virtual DbSet<Teachers> Teachers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-STSUHLQ\\SQLEXPRESS; Database=Mydb; Trusted_connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AbonementTypes>(entity =>
            {
                entity.ToTable("Abonement_types");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Abonements>(entity =>
            {
                entity.Property(e => e.TypeId).HasColumnName("type_id");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Abonements)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Abonements_Abonements_type");
            });

            modelBuilder.Entity<Classrooms>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<LessonTeacher>(entity =>
            {
                entity.ToTable("Lesson_Teacher");

                entity.Property(e => e.LessonId).HasColumnName("Lesson_id");

                entity.Property(e => e.TeacherId).HasColumnName("Teacher_id");

                entity.HasOne(d => d.Lesson)
                    .WithMany(p => p.LessonTeacher)
                    .HasForeignKey(d => d.LessonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lesson_Teacher_Lesson");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.LessonTeacher)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lesson_Teacher_Teacher");
            });

            modelBuilder.Entity<Lessons>(entity =>
            {
                entity.Property(e => e.ClassroomId).HasColumnName("Classroom_id");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.StyleId).HasColumnName("Style_id");

                entity.HasOne(d => d.Style)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.StyleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lessons_Classroom");

                entity.HasOne(d => d.StyleNavigation)
                    .WithMany(p => p.Lessons)
                    .HasForeignKey(d => d.StyleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Lessons_Style");
            });

            modelBuilder.Entity<StudentAbonements>(entity =>
            {
                entity.ToTable("Student_Abonements");

                entity.Property(e => e.AbonId).HasColumnName("Abon_id");

                entity.Property(e => e.ActivationDate)
                    .HasColumnName("Activation_date")
                    .HasColumnType("date");

                entity.Property(e => e.StudId).HasColumnName("Stud_id");

                entity.HasOne(d => d.Abon)
                    .WithMany(p => p.StudentAbonements)
                    .HasForeignKey(d => d.AbonId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Abonements_Abonements");

                entity.HasOne(d => d.Stud)
                    .WithMany(p => p.StudentAbonements)
                    .HasForeignKey(d => d.StudId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Abonements_Students");
            });

            modelBuilder.Entity<Students>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("ntext");

                entity.Property(e => e.Photo)
                    .HasColumnName("photo")
                    .HasColumnType("image");

                entity.Property(e => e.ProfileDescription)
                    .HasColumnName("profile_description")
                    .HasColumnType("ntext");
            });

            modelBuilder.Entity<Styles>(entity =>
            {
                entity.Property(e => e.Name)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            modelBuilder.Entity<TeacherStyles>(entity =>
            {
                entity.ToTable("Teacher_Styles");

                entity.Property(e => e.StyleId).HasColumnName("Style_id");

                entity.Property(e => e.TeacherId).HasColumnName("Teacher_id");

                entity.HasOne(d => d.Style)
                    .WithMany(p => p.TeacherStyles)
                    .HasForeignKey(d => d.StyleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Teacher_Styles_Styles");

                entity.HasOne(d => d.Teacher)
                    .WithMany(p => p.TeacherStyles)
                    .HasForeignKey(d => d.TeacherId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Teacher_Styles_Teacher");
            });

            modelBuilder.Entity<Teachers>(entity =>
            {
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
