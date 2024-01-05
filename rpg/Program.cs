using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public class Startscene
{

}

public class Player
{ 
    
}

namespace rpg
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("이름을 입력하세요.");
            string name = Console.ReadLine();
            Console.Clear();


            while (true)
            {
                Console.WriteLine("스파르타 마을에 오신 것을 환영합니다." + name + "님.");
                Console.WriteLine("이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.");
                Console.WriteLine(" ");
                Console.WriteLine("1. 상태 보기");
                Console.WriteLine("2. 인벤토리 보기");
                Console.WriteLine("3. 상점");

                Console.WriteLine(" ");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                int input = int.Parse(Console.ReadLine());


                if (input == 1)
                {
                    Console.Clear();
                    Console.WriteLine("상태창을 엽니다.");
                    break;
                }

                if (input == 2)
                {
                    Console.Clear();
                    Console.WriteLine("인벤토리를 확인합니다.");
                    break;
                }

                if (input == 3)
                {
                    Console.Clear();
                    Console.WriteLine("상점을 방문합니다.");
                    break;
                }

                else
                {
                    Console.Clear();
                    Console.WriteLine("잘못된 입력입니다.");
                    Console.WriteLine(" ");
                    continue;
                }
            }
        }

        public class Player
        {


        }


    }


        
}



