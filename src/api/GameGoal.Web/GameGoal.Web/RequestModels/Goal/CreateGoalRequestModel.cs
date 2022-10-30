using System.ComponentModel.DataAnnotations;

namespace GameGoal.Web.RequestModels.Goal
{
    public sealed class CreateGoalRequestModel
    {
        [Range(0, 100)]
        public int Progression { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(1000)]
        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;

        public DateTime DeadLine { get; set; }
    }
}
