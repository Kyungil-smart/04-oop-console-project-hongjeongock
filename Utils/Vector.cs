

public struct Vector // 2차원 좌표 표현
{
    public int X { get; set; }
    public int Y { get; set; }

    public Vector(int x, int y) //X,Y좌표 받아 vector생성
    {
        X = x; // 전달 받은 X값 저장
        Y = y; 
    }
    
    public static Vector Up => new Vector(0, -1);//위로 이동
    public static Vector Down => new Vector(0, 1); // 아래로 이동
    public static Vector Left => new Vector(-1, 0); // 왼쪽 이동
    public static Vector Right => new Vector(1, 0);// 오른쪽 이동
    
    public static Vector operator +(Vector a, Vector b)
        => new Vector(a.X + b.X, a.Y + b.Y); //두 벡터 더해서 새로운 좌표 생성
}