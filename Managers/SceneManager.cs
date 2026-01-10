

public static class SceneManager
{
    public static Action OnChangeScene;// 씬 전환 직후 실행할 이벤트
    public static Scene Current { get; private set; } // 현재 활성 씬
    private static Scene _prev; //이전 씬

    private static Dictionary<string, Scene> _scenes = new Dictionary<string, Scene>(); //씬 저장소

    public static void AddScene(string key, Scene state)// key 이름으로 씬 등록
    {
        if (_scenes.ContainsKey(key)) return;// 중복 방지
        
        _scenes.Add(key, state);// 씬 저장소 추가
    }

    public static void ChangePrevScene() //이전 씬으로 돌아가기
    {
        Change(_prev);
    }

    // 상태 바꾸는 기능
    public static void Change(string key) //key에 해당하는 씬으로 전환
    {
        if (!_scenes.ContainsKey(key)) return;
        
        Change(_scenes[key]);
    }

    public static void Change(Scene scene)
    {
        Scene next = scene;
        
        if (Current == next) return;

        Current?.Exit();
        next.Enter();
        
        _prev = Current;
        Current = next;
        OnChangeScene?.Invoke();
    }

    public static void Update()
    {
        Current?.Update();
    }

    public static void Render()
    {
        Current?.Render();
    }
}