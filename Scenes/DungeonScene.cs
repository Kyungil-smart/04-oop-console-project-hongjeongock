public class DungeonScene : Scene
{
    //던전 맵 크기
    private Tile[,] _field = new Tile[10, 20];

    // TownScene에서 쓰던 플레이어 인스턴스
    private PlayerCharacter _player;
    
    // 던전 진입 시 플레이어 시작 위치
    private Vector _startPos = new Vector(1, 1);

    public DungeonScene(PlayerCharacter player) => Init(player);

    public Inventory Inventory { get; } = new Inventory();

    // 타일 배열 생성/초기화
    public void Init(PlayerCharacter player)
    {
        _player = player;

        // _field의 모든 좌표에 Tite을 생성해서 넣기
        for (int y = 0; y < _field.GetLength(0); y++)
        {
            for (int x = 0; x < _field.GetLength(1); x++)
            {
                Vector pos = new Vector(x, y);
                _field[y, x] = new Tile(pos);
            }
        }
    }

    public override void Enter()
    {
        // 플레이어 Field연결
        _player.Field = _field;
        // 플레이어 위치 설정
        _player.Position = _startPos;
        // 해당 타일에 플레이어 올리기
        _field[_player.Position.Y, _player.Position.X].OnTileObject = _player;
        // 골드
        _field[3, 5].OnTileObject = new Gold() { _gold = 50, Name = "Gold"};
    }

    public override void Update()
    {
        _player.Update();
        if (InputManager.GetKey(ConsoleKey.B))
        {
            SceneManager.Change("Town");
        }
        if (InputManager.GetKey(ConsoleKey.Q))
        {
            SceneManager.Change("Title");
        }
    }

    public override void Render()
    {
        // 화면 출력
        // 던전 맵 출력
        // 플레이어 ui 출력
        PrintField();
        _player.Render();
        // 조작키 안내 문구 출력
        if (_player.IsInventoryActive) return;
        Console.WriteLine("↑ : 위로 / ↓ : 아래로 / ← : 왼쪽 / → : 오른쪽 / B : 마을로 이동 / Q : 메인메뉴로 이동");
    }

    public override void Exit()
    {
        // 씬 나갈때 정리, 현재 타일에서 플레이어 제거
        _field[_player.Position.Y, _player.Position.X].OnTileObject = null;
        _player.Field = null;
    }

    private void PrintField()
    {
        // 타일 배열 출력
        for (int y = 0; y < _field.GetLength(0); y++)
        {
            for (int x = 0; x < _field.GetLength(1); x++)
            {
                _field[y, x].Print();
            }
            Console.WriteLine();
        }
    }
}