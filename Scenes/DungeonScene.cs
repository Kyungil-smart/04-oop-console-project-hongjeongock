

public class DungeonScene : Scene
{
    //던전 맵 크기(TownScene과 동일)
    private Tile[,] _field = new Tile[10, 20];

    // TownScene에서 쓰던 플레이어 인스턴스
    private PlayerCharacter _player;
    
    // 던전 진입 시 플레이어 시작 위치

    public DungeonScene(PlayerCharacter player) => Init(player);

    // 타일 배열 생성/초기화
    public void Init(PlayerCharacter player)
    {
        _player = player;
        
        // _field의 모든 좌표에 Tite을 생성해서 넣기
    }

    public override void Enter()
    {
        // 씬에 들어올 때 1회 실행
        // 플레이어 Field연결
        // 플레이어 위치 설정
        // 해당 타일에 플레이어 올리기
        Debug.Log("던전 씬 진입");
    }

    public override void Update()
    {
        _player.Update();
        // 마을로 복귀 기능 추가
    }

    public override void Render()
    {
        // 화면 출력
        // 던전 맵 출력
        // 플레이어 ui 출력
        PrintField();
        _player.Render();
        // 조작키 안내 문구? 출력(가능하면)
    }

    public override void Exit()
    {
        // 씬 나갈때 정리, 현재 타일에서 플레이어 제거
        _field[_player.Position.Y, _player.Position.X].OnTileObject = null;
        _player.Field = null;
    }

    private void PrintField()
    {
        // 타일 배열 순회하며 출력
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