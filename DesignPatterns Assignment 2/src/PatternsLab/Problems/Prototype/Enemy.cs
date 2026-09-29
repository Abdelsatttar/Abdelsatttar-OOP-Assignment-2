namespace PatternsLab.Problems.Prototype;

public class Weapon
{
    public string Name { get; set; } = string.Empty;
    public int Damage { get; set; }

    public Weapon Clone()
    {
        return new Weapon
        {
            Name = Name,
            Damage = Damage
        };
    }
}

public abstract class Enemy
{
    private string _modelData;

    public string Name { get; set; } = string.Empty;
    public int Health { get; set; }
    public Weapon Weapon { get; set; } = new();
    public List<string> Abilities { get; set; } = new();
    public string ModelId => _modelData;

    protected Enemy()
    {
        Console.WriteLine("   ...loading 3D model (slow)...");
        Thread.Sleep(500);
        _modelData = "MODEL_" + Guid.NewGuid().ToString("N")[..6];
    }

    protected Enemy(Enemy source)
    {
        _modelData = source._modelData;
        Name = source.Name;
        Health = source.Health;
        Weapon = source.Weapon.Clone();
        Abilities = new List<string>(source.Abilities);
    }

    public abstract Enemy Clone();
}

public class Orc : Enemy
{
    public Orc()
    {
        Name = "Orc";
        Health = 100;
        Weapon = new Weapon { Name = "Axe", Damage = 25 };
        Abilities.Add("Rage");
    }

    private Orc(Orc source) : base(source)
    {
    }

    public override Enemy Clone() => new Orc(this);
}

public class Elf : Enemy
{
    public Elf()
    {
        Name = "Elf";
        Health = 70;
        Weapon = new Weapon { Name = "Bow", Damage = 18 };
        Abilities.Add("Stealth");
    }

    private Elf(Elf source) : base(source)
    {
    }

    public override Enemy Clone() => new Elf(this);
}

public sealed class EnemyPrototypeRegistry
{
    private readonly Dictionary<string, Enemy> _prototypes = new(StringComparer.OrdinalIgnoreCase);

    public void Register(string name, Enemy prototype)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Prototype name is required.");
        ArgumentNullException.ThrowIfNull(prototype);
        _prototypes[name] = prototype;
    }

    public Enemy Create(string name)
    {
        if (!_prototypes.TryGetValue(name, out var prototype))
            throw new KeyNotFoundException($"No prototype registered with name '{name}'.");

        return prototype.Clone();
    }
}
