public class BattleScene : Scene
{
    private PlayerCharacter _player;

    private Monster _monster;

    private BattleState _state;

    enum BattleState
    {
        Start,
        PlayerTurn,
        MonsterTurn,
        Victory,
        Defeat
    }
    public override void Enter()
    {
        _state = BattleState.Start;
        Debug.Log("전투 시작");
    }

    public override void Exit()
    {
        
    }

    public override void Render()
    {
        
    }

    public override void Update()
    {
        switch(_state)
        {
            case BattleState.PlayerTurn:
                if(InputManager.GetKey(ConsoleKey.D1))
                {
                    Debug.Log("공격 선택");
                }
                break;
        }
    }

    public BattleScene(PlayerCharacter player, Monster monster)
    {
        _player = player;
        _monster = monster;
    }
}

