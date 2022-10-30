using System.ComponentModel.DataAnnotations;

namespace GameGoal.Web.RequestModels.User
{
    public class UpdateUserStateRequest
    {
        [Range(0, 100)]
        [Required]
        public int Health { get; set; }

        [Range(0, 100)]
        [Required]
        public int Serotonin { get; set; }

        [Range(0, 100)]
        [Required]
        public int Dopamine { get; set; }

        [Range(0, 100)]
        [Required]
        public int Endorphins { get; set; }

        [Range(0, 100)]
        [Required]
        public int Oxytocin { get; set; }

        [Range(0, 100)]
        [Required]
        public int Cortisol { get; set; }
    }
}
