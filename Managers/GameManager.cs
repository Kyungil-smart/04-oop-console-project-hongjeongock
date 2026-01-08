using System;

public class GameManager
{
    public static bool IsGameOver { get; set; }
    public const string GameName = "아무튼 RPG";
    private PlayerCharacter _player;

    public void Run()
    {
        // 게임 시작 시 1회 초기화
        Init();
        
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
        
        SceneManager.AddScene("Title", new TitleScene());
        SceneManager.AddScene("Story", new StoryScene());
        SceneManager.AddScene("Town", new TownScene(_player));
        SceneManager.AddScene("Log", new LogScene());
        
        SceneManager.Change("Title");
        
        Debug.Log("게임 데이터 초기화 완료");
    }
}