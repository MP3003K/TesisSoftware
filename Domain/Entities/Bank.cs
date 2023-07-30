using Domain.Entities.Base;

namespace Domain.Entities;

public class Bank : Entity
{
    public string Name { get; private set; } 
    public string Code { get; private set; }
    
    public virtual IList<Pix>? Pixes { get; private set; }

    public Bank(string name, string code)
    {
        Name = name;
        Code = code;
    }

    public void UpdateName(string name)
    {
        Name = name;
    }

    public void UpdateCode(string code)
    {
        Code = code;
    }

    public void Update(string name, string code)
    {
        UpdateName(name);
        UpdateCode(code);
    }
}