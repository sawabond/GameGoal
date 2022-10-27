using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GameGoal.Data.Entities
{
    public class AppUser : IdentityUser<int>
    {
        [Required]
        public string Gender { get; set; } = "Male";

        public ICollection<AppUserRole> UserRoles { get; set; }

        public ICollection<Goal> Goals { get; set; }

        public ICollection<Skin> Skins { get; set; }

        [Range(0, 100)]
        public int Health { get; set; } = 50;

        [Range(0, 100)]
        public int Serotonin { get; set; } = 50;

        [Range(0, 100)]
        public int Dopamine { get; set; } = 50;

        [Range(0, 100)]
        public int Endorphins { get; set; } = 50;

        [Range(0, 100)]
        public int Oxytocin { get; set; } = 50;

        [Range(0, 100)]
        public int Cortisol { get; set; } = 50;
    }
}