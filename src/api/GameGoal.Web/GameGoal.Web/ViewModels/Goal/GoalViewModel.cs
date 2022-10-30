using System.ComponentModel.DataAnnotations;

namespace GameGoal.Web.ViewModels.Goal
{
    public class GoalViewModel
    {
        public int Id { get; set; }

        public int Progression { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsCompleted { get; set; }

        public DateTime DateOfCreation { get; set; }

        public DateTime DeadLine { get; set; }
    }
}
