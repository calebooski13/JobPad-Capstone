using System;
using System.Collections.Generic;
using System.Text;
using JobPad.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace JobPad.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Job> Jobs { get; set; }
        public DbSet<Material> Materials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Create a new user for Identity Framework
            ApplicationUser user = new ApplicationUser
            {
                FirstName = "admin",
                LastName = "admin",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                Id = "00000000-ffff-ffff-ffff-ffffffffffff"
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            // Create 3 customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer()
                {
                    Id = 1,
                    UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                    FirstName = "John",
                    LastName = "Snow",
                    PhoneNumber = "304-700-7000",
                    EmailAddress = "winteriscoming@winteriscoming.com"


                },

                new Customer()
                {
                    Id = 2,
                    UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                    FirstName = "Bob",
                    LastName = "Ross",
                    PhoneNumber = "304-410-4101",
                    EmailAddress = "happylittlebush@happylittlebush.com"


                });

            // Create som jobs
            modelBuilder.Entity<Job>().HasData(
                new Job()
                {
                    Id = 1,
                    CustomerId = 1,
                    EstimatedJobCost = 1200,
                    FinalCost = 1275,
                    Address = "2723 Guyan Ave, Huntington WV 25702",
                    IsPaid = true
                },
                new Job()
                {
                    Id = 2,
                    CustomerId = 1,
                    EstimatedJobCost = 800,
                    FinalCost = 725,
                    Address = "24 Kobe Way, Mamba LA 82408",
                    IsPaid = true
                },
                new Job()
                {
                    Id = 3,
                    CustomerId = 2,
                    EstimatedJobCost = 1300,
                    FinalCost = 1400,
                    Address = "8 Mamba Mentality, Los Angeles CA, 90012",
                    IsPaid = true
                },
                new Job()
                {
                    Id = 4,
                    CustomerId = 2,
                    EstimatedJobCost = 4005,
                    FinalCost = 3900,
                    Address = "123 Tret Rd, Midtown OH, 32323",
                    IsPaid = true
                }
                );
            // Create materials for each job
            modelBuilder.Entity<Material>().HasData(

                new Material()
                {
                    Id = 1,
                    JobId = 1,
                    PaintBrand = "Walls: Marquee, Ceiling: Eminence, Trim: Valspar",
                    PaintColor = "Walls: Repose Gray #SW-7015, Ceiling: Flat White, Trim: Perfect White",
                    MaterialType = "Tape, Caulking (3), Plastic, Spackling",
                    TotalMaterialCost = 325
                },

                 new Material()
                 {
                     Id = 2,
                     JobId = 2,
                     PaintBrand = "Walls: Behr, Ceiling: none, Trim: none",
                     PaintColor = "Walls: Red My Mind #MQ1-10",
                     MaterialType = "Spackling",
                     TotalMaterialCost = 125
                 },
                 new Material()
                 {
                     Id = 3,
                     JobId = 3,
                     PaintBrand = "Walls: Behr Premium, Ceiling: Valspar, Trim: Valspar",
                     PaintColor = "Walls: Nomad Brown #AE-18, Ceiling: Flat White, Trime: Perfect White",
                     MaterialType = "Spackling, Caulking, Plastic",
                     TotalMaterialCost = 275
                 },
                 new Material()
                 {
                     Id = 4,
                     JobId = 4,
                     PaintBrand = "Walls: SW Emerald, Trim: Valspar",
                     PaintColor = "Walls: Sky High #SW-6504, Trime: Perfect White",
                     MaterialType = "Caulking, Plastic",
                     TotalMaterialCost = 500
                 });
        } } }
        
