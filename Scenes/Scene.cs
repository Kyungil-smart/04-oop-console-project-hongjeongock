

public abstract class Scene
{
    public abstract void Enter(); // 씬에 들어올때 실행
    public abstract void Update();// 매 프레임 입력/로직 처리
    public abstract void Render();// 매 프레임 화면 출력
    public abstract void Exit();// 씬에서 나갈 때 실행
}