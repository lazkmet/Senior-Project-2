using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace VideoShareData.Models;

public partial class WebAppDbContext : DbContext
{
    public WebAppDbContext()
    {
    }

    public WebAppDbContext(DbContextOptions<WebAppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AllCourse> AllCourses { get; set; }

    public virtual DbSet<AllUser> AllUsers { get; set; }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseStudent> CourseStudents { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserxCourse> UserxCourses { get; set; }

    public virtual DbSet<UserxVideo> UserxVideos { get; set; }

    public virtual DbSet<Video> Videos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:VideoShare");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AllCourse>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("AllCourses");

            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.DateCreated).HasColumnType("date");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<AllUser>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("AllUsers");

            entity.Property(e => e.DateJoined).HasColumnType("date");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(254)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnName("UserID");
        });

        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.HasKey(e => new { e.VideoId, e.FileName }).HasName("Attachments_PK_VideoID_FileName");

            entity.HasIndex(e => e.FileGuid, "Attachments_Unique_FileGUID").IsUnique();

            entity.Property(e => e.VideoId).HasColumnName("VideoID");
            entity.Property(e => e.FileName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.FileGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("FileGUID");
            entity.Property(e => e.UploadDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");

            entity.HasOne(d => d.Video).WithMany(p => p.Attachments)
                .HasForeignKey(d => d.VideoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Attachments_FK_VideoID_Ref_VideoID");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("Courses_PK_CourseID");

            entity.HasIndex(e => e.CourseCode, "Courses_Unique_CourseCode").IsUnique();

            entity.HasIndex(e => e.FileGuid, "Courses_Unique_FileGUID").IsUnique();

            entity.HasIndex(e => e.CourseCode, "Courses_idx_CourseCode");

            entity.HasIndex(e => e.CourseName, "Courses_idx_CourseName");

            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.CourseCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CourseDescription)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CourseName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.FileGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("FileGUID");
            entity.Property(e => e.OwnerId).HasColumnName("OwnerID");

            entity.HasOne(d => d.Owner).WithMany(p => p.Courses)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Courses_FK_OwnerID_Ref_UserID");
        });

        modelBuilder.Entity<CourseStudent>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("CourseStudents");

            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.FileGuid).HasColumnName("FileGUID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.MessageId).HasName("Messages_PK_MessageID");

            entity.HasIndex(e => e.DateCreated, "Messages_idx_DateCreated");

            entity.Property(e => e.MessageId).HasColumnName("MessageID");
            entity.Property(e => e.AdditionalText)
                .HasMaxLength(8000)
                .IsUnicode(false);
            entity.Property(e => e.CourseCode)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.RecipientEmail)
                .HasMaxLength(254)
                .IsUnicode(false);
            entity.Property(e => e.TimeSent).HasColumnType("datetime");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("Users_PK_UserID");

            entity.HasIndex(e => e.EmailAddress, "Users_Unique_EmailAddress").IsUnique();

            entity.HasIndex(e => e.FileGuid, "Users_Unique_FileGUID").IsUnique();

            entity.HasIndex(e => e.EmailAddress, "Users_idx_EmailAddress");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CourseOrdering).HasDefaultValueSql("((2))");
            entity.Property(e => e.DateCreated)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.EmailAddress)
                .HasMaxLength(254)
                .IsUnicode(false);
            entity.Property(e => e.FileGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("FileGUID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LatestLogin).HasColumnType("datetime");
            entity.Property(e => e.StudentOrdering).HasDefaultValueSql("((1))");
        });

        modelBuilder.Entity<UserxCourse>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.CourseId }).HasName("UserxCourse_PK_UserID_CourseID");

            entity.ToTable("UserxCourse");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.DateJoined)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");

            entity.HasOne(d => d.Course).WithMany(p => p.UserxCourses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserxCourse_FK_CourseID_Ref_CourseID");

            entity.HasOne(d => d.User).WithMany(p => p.UserxCourses)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserxCourse_FK_UserID_Ref_UserID");
        });

        modelBuilder.Entity<UserxVideo>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.VideoId }).HasName("UserxVideo_PK_UserID_VideoID");

            entity.ToTable("UserxVideo");

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.VideoId).HasColumnName("VideoID");

            entity.HasOne(d => d.User).WithMany(p => p.UserxVideos)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserxVideo_FK_UserID_Ref_UserID");

            entity.HasOne(d => d.Video).WithMany(p => p.UserxVideos)
                .HasForeignKey(d => d.VideoId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UserxVideo_FK_VideoID_Ref_VideoID");
        });

        modelBuilder.Entity<Video>(entity =>
        {
            entity.HasKey(e => e.VideoId).HasName("Videos_PK_VideoID");

            entity.HasIndex(e => e.FileGuid, "Videos_Unique_FileGUID").IsUnique();

            entity.HasIndex(e => e.CourseId, "Videos_idx_CourseID");

            entity.Property(e => e.VideoId).HasColumnName("VideoID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.FileGuid)
                .HasDefaultValueSql("(newid())")
                .HasColumnName("FileGUID");
            entity.Property(e => e.VideoDescription)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.VideoTitle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.YtuseDescription).HasColumnName("YTUseDescription");
            entity.Property(e => e.YtvideoUrl)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("YTVideoURL");

            entity.HasOne(d => d.Course).WithMany(p => p.Videos)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Videos_FK_CourseID_Ref_CourseID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
