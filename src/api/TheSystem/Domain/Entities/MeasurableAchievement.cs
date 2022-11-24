namespace Domain.Entities;

public class MeasurableAchievement : Achievement
{
    public decimal Quantity { get; set; }

    public string UnitOfMeasurement { get; set; }
}
