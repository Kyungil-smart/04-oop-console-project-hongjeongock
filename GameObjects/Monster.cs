

public class Monster : GameObject
{
    public string Name { get; set; }
    public int HP { get; set; } = 100;
    public int ATK { get; set; } = 10;
    public bool IsBoss { get; set; }
    public Monster() => Init();

    private void Init()
    {
        Symbol = 'M';
    }

}