using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using Tracker.Models.TrackerModels;
using System.Collections.Generic;

namespace Tracker.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full Name")]
        [Required]
        [StringLength(256)]
        public string FullName { get; set; }
        public virtual ICollection<Workout> Workouts { get; set; }
        public virtual ICollection<WorkoutMaster> WorkoutMasters { get; set; }
        public virtual ICollection<Profile> Profile { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationRole : IdentityRole
    {
        public ApplicationRole() : base() { }
        public ApplicationRole(string roleName) : base(roleName) { }
    }


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityUser>()
                .ToTable("Users");
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("Users");
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Tracker.Models.TrackerModels.Profile> Profiles { get; set; }

        public System.Data.Entity.DbSet<Tracker.Models.TrackerModels.Exercise> Exercises { get; set; }

        public System.Data.Entity.DbSet<Tracker.Models.TrackerModels.Workout> Workouts { get; set; }

        public System.Data.Entity.DbSet<Tracker.Models.TrackerModels.WorkoutMaster> WorkoutMasters { get; set; }

    }
}