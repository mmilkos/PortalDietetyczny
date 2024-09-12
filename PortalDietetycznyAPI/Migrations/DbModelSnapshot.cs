﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PortalDietetycznyAPI.Infrastructure.Context;

#nullable disable

namespace PortalDietetycznyAPI.Migrations
{
    [DbContext(typeof(Db))]
    partial class DbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0-preview.6.24327.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PortalDietetycznyAPI.Domain.Entities.BlogPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("BlogPostId")
                        .HasColumnType("int");

                    b.Property<string>("PublicId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BlogPostId")
                        .IsUnique()
                        .HasFilter("[BlogPostId] IS NOT NULL");

                    b.ToTable("BlogPhotos");
                });

            modelBuilder.Entity("PortalDietetycznyAPI.Domain.Entities.BlogPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PhotoId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Uid")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Uid");

                    b.ToTable("BlogPosts");
                });

            modelBuilder.Entity("PortalDietetycznyAPI.Domain.Entities.Diet", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FileUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Kcal")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PhotoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Diets");
                });

            modelBuilder.Entity("PortalDietetycznyAPI.Domain.Entities.DietPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("DietId")
                        .HasColumnType("int");

                    b.Property<string>("PublicId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DietId")
                        .IsUnique()
                        .HasFilter("[DietId] IS NOT NULL");

                    b.ToTable("DietPhotos");
                });

            modelBuilder.Entity("PortalDietetycznyAPI.Domain.Entities.DietTag", b =>
                {
                    b.Property<int>("DietId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("DietId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("DietTags");
                });

            modelBuilder.Entity("PortalDietetycznyAPI.Domain.Entities.Ingredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("PortalDietetycznyAPI.Domain.Entities.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Instruction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PhotoId")
                        .HasColumnType("int");

                    b.Property<int>("Uid")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Uid");

                    b.ToTable("Recipe");
                });

            modelBuilder.Entity("PortalDietetycznyAPI.Domain.Entities.RecipeIngredient", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("HomeUnit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("HomeUnitValue")
                        .HasColumnType("real");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<string>("Unit")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("UnitValue")
                        .HasColumnType("real");

                    b.HasKey("Id");

                    b.HasIndex("IngredientId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeIngredients");
                });

            modelBuilder.Entity("PortalDietetycznyAPI.Domain.Entities.RecipePhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("PublicId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("RecipeId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId")
                        .IsUnique()
                        .HasFilter("[RecipeId] IS NOT NULL");

                    b.ToTable("Photos");
                });

            modelBuilder.Entity("PortalDietetycznyAPI.Domain.Entities.RecipeTag", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("TagId")
                        .HasColumnType("int");

                    b.HasKey("RecipeId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("RecipeTags");
                });

            modelBuilder.Entity("PortalDietetycznyAPI.Domain.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("PortalDietetycznyAPI.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("PortalDietetycznyAPI.Domain.Entities.BlogPhoto", b =>
                {
                    b.HasOne("PortalDietetycznyAPI.Domain.Entities.BlogPost", "BlogPost")
                        .WithOne("Photo")
                        .HasForeignKey("PortalDietetycznyAPI.Domain.Entities.BlogPhoto", "BlogPostId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("BlogPost");
                });

            modelBuilder.Entity("PortalDietetycznyAPI.Domain.Entities.DietPhoto", b =>
                {
                    b.HasOne("PortalDietetycznyAPI.Domain.Entities.Diet", "Diet")
                        .WithOne("Photo")
                        .HasForeignKey("PortalDietetycznyAPI.Domain.Entities.DietPhoto", "DietId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Diet");
                });

            modelBuilder.Entity("PortalDietetycznyAPI.Domain.Entities.DietTag", b =>
                {
                    b.HasOne("PortalDietetycznyAPI.Domain.Entities.Diet", "Diet")
                        .WithMany("DietTags")
                        .HasForeignKey("DietId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PortalDietetycznyAPI.Domain.Entities.Tag", "Tag")
                        .WithMany("DietTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Diet");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("PortalDietetycznyAPI.Domain.Entities.Recipe", b =>
                {
                    b.OwnsOne("PortalDietetycznyAPI.Domain.Entities.NutritionInfo", "Nutrition", b1 =>
                        {
                            b1.Property<int>("RecipeId")
                                .HasColumnType("int");

                            b1.Property<int>("Carb")
                                .HasColumnType("int");

                            b1.Property<int>("Fat")
                                .HasColumnType("int");

                            b1.Property<int>("Fiber")
                                .HasColumnType("int");

                            b1.Property<int>("Kcal")
                                .HasColumnType("int");

                            b1.Property<int>("Protein")
                                .HasColumnType("int");

                            b1.HasKey("RecipeId");

                            b1.ToTable("Recipe");

                            b1.WithOwner()
                                .HasForeignKey("RecipeId");
                        });

                    b.Navigation("Nutrition")
                        .IsRequired();
                });

            modelBuilder.Entity("PortalDietetycznyAPI.Domain.Entities.RecipeIngredient", b =>
                {
                    b.HasOne("PortalDietetycznyAPI.Domain.Entities.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PortalDietetycznyAPI.Domain.Entities.Recipe", null)
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("PortalDietetycznyAPI.Domain.Entities.RecipePhoto", b =>
                {
                    b.HasOne("PortalDietetycznyAPI.Domain.Entities.Recipe", "Recipe")
                        .WithOne("Photo")
                        .HasForeignKey("PortalDietetycznyAPI.Domain.Entities.RecipePhoto", "RecipeId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("PortalDietetycznyAPI.Domain.Entities.RecipeTag", b =>
                {
                    b.HasOne("PortalDietetycznyAPI.Domain.Entities.Recipe", "Recipe")
                        .WithMany("RecipeTags")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("PortalDietetycznyAPI.Domain.Entities.Tag", "Tag")
                        .WithMany("RecipeTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("PortalDietetycznyAPI.Domain.Entities.BlogPost", b =>
                {
                    b.Navigation("Photo");
                });

            modelBuilder.Entity("PortalDietetycznyAPI.Domain.Entities.Diet", b =>
                {
                    b.Navigation("DietTags");

                    b.Navigation("Photo")
                        .IsRequired();
                });

            modelBuilder.Entity("PortalDietetycznyAPI.Domain.Entities.Recipe", b =>
                {
                    b.Navigation("Ingredients");

                    b.Navigation("Photo");

                    b.Navigation("RecipeTags");
                });

            modelBuilder.Entity("PortalDietetycznyAPI.Domain.Entities.Tag", b =>
                {
                    b.Navigation("DietTags");

                    b.Navigation("RecipeTags");
                });
#pragma warning restore 612, 618
        }
    }
}
