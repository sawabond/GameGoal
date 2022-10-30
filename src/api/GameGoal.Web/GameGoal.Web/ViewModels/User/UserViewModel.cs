using System.ComponentModel.DataAnnotations;

namespace GameGoal.Web.ViewModels.User
{
    public sealed class UserViewModel
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Gender { get; set; }

        public string Token { get; set; }

        public int Health { get; set; }

        public int Serotonin { get; set; }

        public int Dopamine { get; set; }

        public int Endorphins { get; set; }

        public int Oxytocin { get; set; }

        public int Cortisol { get; set; }
    }
}
