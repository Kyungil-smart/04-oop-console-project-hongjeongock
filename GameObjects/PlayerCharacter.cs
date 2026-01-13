using System.Runtime.InteropServices.Marshalling;

public class PlayerCharacter : GameObject
{
    public Tile[,] Field { get; set; }
    public Inventory Inventory { get; set; }
    private Inventory _inventory = new Inventory();
    public bool IsInventoryActive => _inventory.IsActive;
    public bool IsActiveControl { get; private set; }

    public int MaxHP { get; private set; } = 100;

    public int HP { get; private set; } = 100;
    public int ATK { get; private set; } = 10;

    public int MonsterKillCount { get; private set; }

    public int BossKillCount { get; private set; }

    public void AddMonsterKillCount()
    {
       if (IsBoss)
       {
           BossKillCount++;
       }
       else
       {
           MonsterKillCount++;
       }
    }

    public bool IsBoss { get; set; }

    public int Gold { get; private set; }
    public void AddGold(int _gold)
    {
        Gold += _gold;
    }

    public PlayerCharacter() => Init();

    public void Init()
    {
        Symbol = 'P';
        IsActiveControl = true;

        MaxHP = 100;
        HP = MaxHP;
        ATK = 10;
    }
    public void TakeDamage(int damage)
    {
        HP -= damage;
        if (HP < 0) HP = 0;
        Debug.LogWarning($"플레이어가 {damage} 데미지를 입었습니다. 남은 체력 : {HP}/{MaxHP}");
    }


    public void Update()
    {
        if (InputManager.GetKey(ConsoleKey.I))
        {
            HandleControl();
        }
        
        if (InputManager.GetKey(ConsoleKey.UpArrow))
        {
            Move(Vector.Up);
            _inventory.SelectUp();
        }

        if (InputManager.GetKey(ConsoleKey.DownArrow))
        {
            Move(Vector.Down);
            _inventory.SelectDown();
        }

        if (InputManager.GetKey(ConsoleKey.LeftArrow))
        {
            Move(Vector.Left);
        }

        if (InputManager.GetKey(ConsoleKey.RightArrow))
        {
            Move(Vector.Right);
        }

        if (InputManager.GetKey(ConsoleKey.Enter))
        {
            _inventory.Select();
        }
    }

    public void HandleControl()
    {
        _inventory.IsActive = !_inventory.IsActive;
        IsActiveControl = !_inventory.IsActive;
    }

    private void Move(Vector direction)
    {
        if (Field == null || !IsActiveControl) return;
        
        Vector current = Position;
        Vector nextPos = Position + direction;
        
        // 1. 맵 바깥은 아닌지?
        // 2. 벽인지?

        GameObject nextTileObject = Field[nextPos.Y, nextPos.X].OnTileObject;

        if (nextTileObject != null)
        {
            if (nextTileObject is IInteractable)
            {
                (nextTileObject as IInteractable).Interact(this);
            }
        }

        if (nextTileObject is Dungeon)
        {
            SceneManager.Change("Dungeon");
        }

        if (nextTileObject is Monster monster)
        {
            SceneManager.Change(new BattleScene(this, monster));
            return;
        }

        Field[Position.Y, Position.X].OnTileObject = null;
        Field[nextPos.Y, nextPos.X].OnTileObject = this;
        Position = nextPos;

        Debug.LogWarning($"플레이어 이동 : ({current.X},{current.Y}) -> ({nextPos.X},{nextPos.Y})");
    }

    public void Render()
    {
        _inventory.Render(Gold);
    }

    public void AddItem(Item item)
    {
        _inventory.Add(item);
    }
}