using System;

class Program
{
    static void Main(string[] args)
    {
        GameManager gameManager = new GameManager(); // 게임 흐름 관리
        gameManager.Run(); // 게임 실행 루프
    }
}