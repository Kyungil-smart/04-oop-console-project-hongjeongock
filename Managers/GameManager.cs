using System;

public class GameManager
{
    public static bool IsGameOver { get; set; } // 게임 종료 여부
    public const string GameName = "아무튼 RPG";
    private PlayerCharacter _player; // 플레이어 캐릭터 객체

    public void Run() //게임 메인 루프 실행
    {
        // 게임 시작 시 1회 초기화
        Init();
        //종료될 때까지 반복
        while (!IsGameOver)
        {
            // 렌더링
            Console.Clear();
            SceneManager.Render();
            // 키입력 받고
            InputManager.GetUserInput();

            if (InputManager.GetKey(ConsoleKey.L))
            {
                SceneManager.Change("Log");
            }

            // 데이터 처리
            SceneManager.Update();
        }
    }

    private void Init()
    {
        IsGameOver = false;
        // 씬 전환이 발생할 때다마 입력 상태를 초기화
        SceneManager.OnChangeScene += InputManager.ResetKey;
        _player = new PlayerCharacter();
        // 타이틀 씬 등록
        SceneManager.AddScene("Title", new TitleScene());
        // 스토리 씬 등록
        SceneManager.AddScene("Story", new StoryScene());
        // 타운 씬 생성시 플레이어 객체 전달
        SceneManager.AddScene("Town", new TownScene(_player));
        // 로그 씬 등록
        SceneManager.AddScene("Log", new LogScene());
        // 던전 씬 등록
        SceneManager.AddScene("Dungeon", new DungeonScene(_player));
        // 상점 씬 등록
        SceneManager.AddScene("Shop", new ShopScene());

        SceneManager.Change("Title");
        
        Debug.Log("게임 데이터 초기화 완료");
    }
}