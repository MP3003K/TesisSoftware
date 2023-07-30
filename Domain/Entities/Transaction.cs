using Domain.Entities.Base;

namespace Domain.Entities;

public class Transaction : Entity
{
    public int PixId { get; private set; }
    public double Value { get; private set; }
    public DateTime Date { get; private set; }

    public virtual Pix? Pix { get; private set; }

    public Transaction(int pixId, double value)
    {
        PixId = pixId;
        Value = value;
    }
}