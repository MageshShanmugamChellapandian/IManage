﻿// <auto-generated />
using System;
using IManage.Repositories.V1.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace IManage.Repositories.Migrations
{
    [DbContext(typeof(IManageContext))]
    [Migration("20221211161625_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("IManage.Repositories.V1.EFModels.FunctionRight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("description");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("function_right", (string)null);
                });

            modelBuilder.Entity("IManage.Repositories.V1.EFModels.FunctionRightType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("function_right_type", (string)null);
                });

            modelBuilder.Entity("IManage.Repositories.V1.EFModels.FunctionRightTypeMapping", b =>
                {
                    b.Property<int>("FunctionrightId")
                        .HasColumnType("int")
                        .HasColumnName("functionrightID");

                    b.Property<int>("TypeId")
                        .HasColumnType("int")
                        .HasColumnName("typeID");

                    b.HasKey("FunctionrightId", "TypeId")
                        .HasName("FRTMPRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.HasIndex(new[] { "TypeId" }, "fk_typeid_idx");

                    b.ToTable("function_right_type_mapping", (string)null);
                });

            modelBuilder.Entity("IManage.Repositories.V1.EFModels.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("description");

                    b.Property<bool>("IsExfactory")
                        .HasColumnType("bit")
                        .HasColumnName("is_exfactory");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.Property<int>("TypeId")
                        .HasColumnType("int")
                        .HasColumnName("type_id");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "TypeId" }, "fk_role_type_idx");

                    b.ToTable("role", (string)null);
                });

            modelBuilder.Entity("IManage.Repositories.V1.EFModels.RoleFunctionRightMapping", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<int>("FunctionRightId")
                        .HasColumnType("int")
                        .HasColumnName("function_right_id");

                    b.HasKey("RoleId", "FunctionRightId")
                        .HasName("RFRMPRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.HasIndex(new[] { "FunctionRightId" }, "IX_RoleFunctionRightMapping_function_right_id");

                    b.ToTable("role_function_right_mapping", (string)null);
                });

            modelBuilder.Entity("IManage.Repositories.V1.EFModels.RoleType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("role_type", (string)null);
                });

            modelBuilder.Entity("IManage.Repositories.V1.EFModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AvatarUrl")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)")
                        .HasColumnName("avatar_url");

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("email_id");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)")
                        .HasColumnName("full_name");

                    b.Property<bool?>("IsExfactory")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("is_exfactory")
                        .HasDefaultValueSql("'0'");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("name");

                    b.HasKey("Id");

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("IManage.Repositories.V1.EFModels.UserRoleMapping", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<int>("RoleId")
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.HasKey("UserId", "RoleId")
                        .HasName("PRIMARY")
                        .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                    b.HasIndex(new[] { "RoleId" }, "IX_UserRoleMapping_role_id");

                    b.ToTable("user_role_mapping", (string)null);
                });

            modelBuilder.Entity("IManage.Repositories.V1.EFModels.FunctionRightTypeMapping", b =>
                {
                    b.HasOne("IManage.Repositories.V1.EFModels.FunctionRight", "Functionright")
                        .WithMany("FunctionRightTypeMappings")
                        .HasForeignKey("FunctionrightId")
                        .IsRequired()
                        .HasConstraintName("fk_id");

                    b.HasOne("IManage.Repositories.V1.EFModels.FunctionRightType", "Type")
                        .WithMany("FunctionRightTypeMappings")
                        .HasForeignKey("TypeId")
                        .IsRequired()
                        .HasConstraintName("fk_typeid");

                    b.Navigation("Functionright");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("IManage.Repositories.V1.EFModels.Role", b =>
                {
                    b.HasOne("IManage.Repositories.V1.EFModels.RoleType", "Type")
                        .WithMany("Roles")
                        .HasForeignKey("TypeId")
                        .IsRequired()
                        .HasConstraintName("fk_role_type");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("IManage.Repositories.V1.EFModels.RoleFunctionRightMapping", b =>
                {
                    b.HasOne("IManage.Repositories.V1.EFModels.FunctionRight", "FunctionRight")
                        .WithMany("RoleFunctionRightMappings")
                        .HasForeignKey("FunctionRightId")
                        .IsRequired()
                        .HasConstraintName("FK_RoleFunctionRightMapping_FunctionRight");

                    b.HasOne("IManage.Repositories.V1.EFModels.Role", "Role")
                        .WithMany("RoleFunctionRightMappings")
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("FK_RoleFunctionRightMapping_Role");

                    b.Navigation("FunctionRight");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("IManage.Repositories.V1.EFModels.UserRoleMapping", b =>
                {
                    b.HasOne("IManage.Repositories.V1.EFModels.Role", "Role")
                        .WithMany("UserRoleMappings")
                        .HasForeignKey("RoleId")
                        .IsRequired()
                        .HasConstraintName("FK_UserRoleMapping_Role");

                    b.HasOne("IManage.Repositories.V1.EFModels.User", "User")
                        .WithMany("UserRoleMappings")
                        .HasForeignKey("UserId")
                        .IsRequired()
                        .HasConstraintName("FK_UserRoleMapping_User");

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("IManage.Repositories.V1.EFModels.FunctionRight", b =>
                {
                    b.Navigation("FunctionRightTypeMappings");

                    b.Navigation("RoleFunctionRightMappings");
                });

            modelBuilder.Entity("IManage.Repositories.V1.EFModels.FunctionRightType", b =>
                {
                    b.Navigation("FunctionRightTypeMappings");
                });

            modelBuilder.Entity("IManage.Repositories.V1.EFModels.Role", b =>
                {
                    b.Navigation("RoleFunctionRightMappings");

                    b.Navigation("UserRoleMappings");
                });

            modelBuilder.Entity("IManage.Repositories.V1.EFModels.RoleType", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("IManage.Repositories.V1.EFModels.User", b =>
                {
                    b.Navigation("UserRoleMappings");
                });
#pragma warning restore 612, 618
        }
    }
}