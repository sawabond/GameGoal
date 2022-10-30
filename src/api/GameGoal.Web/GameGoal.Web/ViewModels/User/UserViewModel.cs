using System.ComponentModel.DataAnnotations;

namespace GameGoal.Web.ViewModels.User
{
    public sealed class UserViewModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Gender { get; set; }

        public string Token { get; set; }

        [Range(0, 100)]
        public int Health { get; set; }

        [Range(0, 100)]
        public int Serotonin { get; set; }

        [Range(0, 100)]
        public int Dopamine { get; set; }

        [Range(0, 100)]
        public int Endorphins { get; set; }

        [Range(0, 100)]
        public int Oxytocin { get; set; }

        [Range(0, 100)]
        public int Cortisol { get; set; }
    }
}
