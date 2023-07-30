using Domain.Entities.Base;

namespace Domain.Entities;

public class Pix : Entity
{
    public int BankId { get; private set; }
    public string Key { get; private set; }

    public virtual Bank? Bank { get; private set; }
    public virtual IList<Transaction>? Transactions { get; private set; }

    public Pix(int bankId, string key)
    {
        BankId = bankId;
        Key = key;
    }

    public void UpdateBankId(int bankId)
    {
        BankId = bankId;
    }

    public void UpdateKey(string key)
    {
        Key = key;
    }

    public void Update(int bankId, string key)
    {
        UpdateBankId(bankId);
        UpdateKey(key);
    }
}