public class BattleScene : Scene
{
    private PlayerCharacter _player;

    private Monster _monster;

    private BattleState _state;

    private string _message;

    private BattleList _battleMenu;

    private bool _isInventoryOpen;

    enum BattleState
    {
        Start,
        PlayerTurn,
        MonsterTurn,
        Victory,
        Defeat
    }
    public BattleScene()
        {
            Init();
        }
    public void Init()
    {
        _battleMenu = new BattleList();
        _battleMenu.Add("전투", Attack);
        _battleMenu.Add("인벤토리", OpenInventory);
        _battleMenu.Add("도망", Escape);

    }
    public override void Enter()
    {
        _state = BattleState.Start;
        Debug.Log("전투 시작");
        Init();
    }

    public override void Exit()
    {
        
    }

    public override void Render()
    {
        Console.Clear();

        _battleMenu.Render(2, 12);

        Console.SetCursorPosition(50, 1);
        Console.WriteLine($"몬스터 체력 : {_monster.HP}");

        Console.SetCursorPosition(2, 8);
        Console.WriteLine($"플레이어 체력 : {_player.HP}");

        Console.SetCursorPosition(20, 4);
        Console.Write(_message);

        if (_isInventoryOpen)
        {
            _player.Render();

            return;
        }
    }

    public override void Update()
    {
        if (InputManager.GetKey(ConsoleKey.UpArrow))
        {
            _battleMenu.SelectUp();
        }

        if (InputManager.GetKey(ConsoleKey.DownArrow))
        {
            _battleMenu.SelectDown();
        }

        if (InputManager.GetKey(ConsoleKey.Enter))
        {
            _battleMenu.Select();
        }
        switch (_state)
        {
            case BattleState.PlayerTurn:
                
                if(InputManager.GetKey(ConsoleKey.D1))
                {
                    if(_monster.HP <= 0)
                    {
                        _state = BattleState.Victory;
                        return;
                    }
                }
                if(InputManager.GetKey(ConsoleKey.D2))
                {
                    // 인벤토리
                }
                if(InputManager.GetKey(ConsoleKey.D3))
                {
                    // 도망
                }
                break;

             case BattleState.MonsterTurn:

                _player.TakeDamage(_monster.ATK);

                if(_player.HP <= 0)
                {
                    _state = BattleState.Defeat;
                    return;
                }
                break;
            case BattleState.Victory:
                // 몬스터 킬 카운트
                // 던전 복귀
                break;

            case BattleState.Defeat:
                // 게임 오버
                break;

        }
    }

    public BattleScene(PlayerCharacter player, Monster monster)
    {
        _player = player;
        _monster = monster;
    }
    public void Attack()
    {
        int damage = _player.ATK;
        _monster.HP -= damage;
        _message = $"플레이어가 몬스터에게 {damage} 데미지를 입혔습니다.\n";
        if (_monster.HP <= 0)
        {
            _state = BattleState.Victory;
            _message += "몬스터를 물리쳤습니다!";
        }
        else
        {
            _state = BattleState.MonsterTurn;
        }
    }
    public void OpenInventory()
    {
        _isInventoryOpen = true;

        if (_isInventoryOpen)
        {
            _message = "인벤토리가 열렸습니다.";
            _player.HandleControl();
        }
        else
        {
            _message = "전투로 복귀합니다.";
            _player.HandleControl();
        }
    }
    public void Escape()
    {
        _message = "도망쳤습니다!";

        SceneManager.Change("Dungeon");
    }
}

