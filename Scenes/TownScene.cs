public class TownScene : Scene // 마을 화면 씬
{
    // 마을 맵 구성하는 타일 배열
    private Tile[,] _field = new Tile[20, 20]; 
    // 현재 씬에서 사용하는 플레이어 객체
    private PlayerCharacter _player; 
    
    // 타운씬 생성시 플레이어를 전달받아 초기화
    public TownScene(PlayerCharacter player) => Init(player);

    // 마을씬 초기화
    public void Init(PlayerCharacter player)
    {
        _player = player;
        
        for (int y = 0; y < _field.GetLength(0); y++) // 세로 타일 순회
        {
            for (int x = 0; x < _field.GetLength(1); x++)// 가로 타일 순회
            {
                Vector pos = new Vector(x, y);
                _field[y, x] = new Tile(pos);
            }
        }
    }

    public override void Enter()
    {
        // 플레이어를 필드에서 분리
        _player.Field = _field;
        _player.Position = new Vector(1, 1);
        _field[_player.Position.Y, _player.Position.X].OnTileObject = _player;

        _field[3, 5].OnTileObject = new Potion() {Name = "Potion1"};
        _field[2, 15].OnTileObject = new Potion() {Name = "Potion2"};
        _field[7, 3].OnTileObject = new Potion() {Name = "Potion3"};
        _field[9, 19].OnTileObject = new Potion() {Name = "Potion4"};
        _field[5, 10].OnTileObject = new Dungeon();
        _field[10, 10].OnTileObject = new Shop();

        Debug.Log("타운 씬 진입");
    }

    public override void Update()
    {
        _player.Update();
    }

    public override void Render()
    {
        PrintField();
        _player.Render();
    }

    public override void Exit()
    {
        _field[_player.Position.Y, _player.Position.X].OnTileObject = null;
        _player.Field = null;
    }

    // 마을 맵을 화면에 출력
    private void PrintField()
    {
        for (int y = 0; y < _field.GetLength(0); y++) // 맵 세로 출력
        {
            for (int x = 0; x < _field.GetLength(1); x++)// 가로 출력
            {
                _field[y, x].Print();// 해당 좌표 타일 출력
            }
            Console.WriteLine(); 
        }
    }
}