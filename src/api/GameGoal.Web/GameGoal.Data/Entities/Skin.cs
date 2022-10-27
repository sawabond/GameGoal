using System.ComponentModel.DataAnnotations;

namespace GameGoal.Data.Entities
{
    public class Skin
    {
        public int Id { get; set; }

        public int? AppUserId { get; set; }

        public string Name { get; set; }

        [Range(0, 100)]
        public int Level { get; set; }

        [Url]
        public int PhotoUrl { get; set; }
    }
}
