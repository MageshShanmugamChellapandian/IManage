using IManage.Repositories.V1.EFModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace IManage.Repositories.V1.DbContexts
{
    public partial class IManageContext : DbContext
    {
        public IManageContext()
        {
        }

        private readonly IConfiguration _configuration;

        public IManageContext(DbContextOptions<IManageContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<FunctionRight> FunctionRights { get; set; }
        public virtual DbSet<FunctionRightType> FunctionRightTypes { get; set; }
        public virtual DbSet<FunctionRightTypeMapping> FunctionRightTypeMappings { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<RoleFunctionRightMapping> RoleFunctionRightMappings { get; set; }
        public virtual DbSet<RoleType> RoleTypes { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserRoleMapping> UserRoleMappings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=localhost; Initial Catalog=IManage; User ID=sa;Password=IManage@123; Trust Server Certificate=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FunctionRight>(entity =>
            {
                entity.ToTable("function_right");



                entity.Property(e => e.Id)
                    .HasColumnType("int")
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<FunctionRightType>(entity =>
            {
                entity.ToTable("function_right_type");



                entity.Property(e => e.Id)
                    .HasColumnType("int")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<FunctionRightTypeMapping>(entity =>
            {
                entity.HasKey(e => new { e.FunctionrightId, e.TypeId })
                    .HasName("FRTMPRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("function_right_type_mapping");



                entity.HasIndex(e => e.TypeId, "fk_typeid_idx");

                entity.Property(e => e.FunctionrightId)
                    .HasColumnType("int")
                    .HasColumnName("functionrightID");

                entity.Property(e => e.TypeId)
                    .HasColumnType("int")
                    .HasColumnName("typeID");

                entity.HasOne(d => d.Functionright)
                    .WithMany(p => p.FunctionRightTypeMappings)
                    .HasForeignKey(d => d.FunctionrightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_id");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.FunctionRightTypeMappings)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_typeid");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");



                entity.HasIndex(e => e.TypeId, "fk_role_type_idx");

                entity.Property(e => e.Id)
                    .HasColumnType("int")
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description");

                entity.Property(e => e.IsExfactory).HasColumnName("is_exfactory");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

                entity.Property(e => e.TypeId)
                    .HasColumnType("int")
                    .HasColumnName("type_id");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Roles)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_role_type");
            });

            modelBuilder.Entity<RoleFunctionRightMapping>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.FunctionRightId })
                    .HasName("RFRMPRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("role_function_right_mapping");



                entity.HasIndex(e => e.FunctionRightId, "IX_RoleFunctionRightMapping_function_right_id");

                entity.Property(e => e.RoleId)
                    .HasColumnType("int")
                    .HasColumnName("role_id");

                entity.Property(e => e.FunctionRightId)
                    .HasColumnType("int")
                    .HasColumnName("function_right_id");

                entity.HasOne(d => d.FunctionRight)
                    .WithMany(p => p.RoleFunctionRightMappings)
                    .HasForeignKey(d => d.FunctionRightId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleFunctionRightMapping_FunctionRight");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleFunctionRightMappings)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RoleFunctionRightMapping_Role");
            });

            modelBuilder.Entity<RoleType>(entity =>
            {
                entity.ToTable("role_type");



                entity.Property(e => e.Id)
                    .HasColumnType("int")
                    .HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");



                entity.Property(e => e.Id)
                    .HasColumnType("int")
                    .HasColumnName("id");

                entity.Property(e => e.AvatarUrl)
                    .HasMaxLength(1000)
                    .HasColumnName("avatar_url");

                entity.Property(e => e.EmailId)
                    .HasMaxLength(200)
                    .HasColumnName("email_id");

                entity.Property(e => e.FullName)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("full_name");

                entity.Property(e => e.IsExfactory)
                    .HasColumnName("is_exfactory")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name");

            });

            modelBuilder.Entity<UserRoleMapping>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("user_role_mapping");



                entity.HasIndex(e => e.RoleId, "IX_UserRoleMapping_role_id");

                entity.Property(e => e.UserId)
                    .HasColumnType("int")
                    .HasColumnName("user_id");

                entity.Property(e => e.RoleId)
                    .HasColumnType("int")
                    .HasColumnName("role_id");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.UserRoleMappings)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoleMapping_Role");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserRoleMappings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserRoleMapping_User");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
