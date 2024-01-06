using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace rpg
{
    //캐릭터 클래스와 아이템 클래스 구현하는 기능
    public class Character
    {
        public string Name { get; } // 한 번 정의되면 바꿀 수 없도록 

        public string Job { get; }

        public int Level { get; }

        public int Atk { get; }

        public int Def { get; }

        public int HP { get; }

        public int Gold { get; }

        public Character(string name, string job, int level, int atk, int def, int hp, int gold)
        {
            Name = name;
            Job = job;
            Level = level;
            Atk = atk;
            Def = def;
            HP = hp;
            Gold = gold;
        }
        //생성자 - 특별한 함수로 생각. 자동으로 완성되게 끔
        //클레스의 이름과 같은 함수로 생각. 기본 세팅을 하는 역할
        //클래스는 구성도, 캐릭터를 생성할 때 인스턴스를 만든다고 함. 

    }

    public class Item
    {
        public string Name { get; }
        public string Description { get; }
        public int Type { get; }
        public int Atk { get; }
        public int Def { get; }
        public int HP { get; }

        public bool IsEquipped { get; set; }

        public static int ItemCnt = 0;

        public Item(string name, string description, int type, int atk, int def, int hP, bool isEquipped = false)
        {
            Name = name;
            Description = description;
            Type = type;
            Atk = atk;
            Def = def;
            HP = hP;
            IsEquipped = isEquipped;
        }

        public void PrintItemStatDescription(bool withNumber = false, int idx = 0)
        {
            Console.Write("- ");
            if (withNumber)
            {
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.Write("{0}", idx);
                Console.ResetColor();

            }
            if (IsEquipped)
            {
                Console.Write("[");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("E");
                Console.ResetColor();
                Console.Write("]");

            }
            Console.Write(Name);
            Console.Write(" | ");

            //삼항연산자 : (Atk >= 0 ? "+" : ") [조건 ? 조건이 참이라면 : 조건이 거짓이라면]
            if (Atk != 0) Console.Write($"Atk {(Atk >= 0 ? "+" : "")}{Atk} ");
            if (Def != 0) Console.Write($"Def {(Def >= 0 ? "+" : "")}{Def} ");
            if (HP != 0) Console.Write($"HP {(HP >= 0 ? "+" : "")}{HP} ");

            Console.Write(" | ");

            Console.WriteLine(Description);
        }



    }

    internal class Program
    {
        //던전에서 쓸 캐릭터 추가
        static Character _player;
        static Item[] _items;
        static void Main(string[] args)
        {
            // 구성
            // 0. 데이터 초기화
            // 1. 스타팅 로고를 보여줌 (게임 처음 켤때만 보여줌)
            // 2. 선택 화면을 보여줌 (기본 구현사항 - 상태 / 인벤토리)
            // 3. 상태화면을 구현함 (필요 구현 요소 : 캐릭터, 아이템)
            // 4. 인벤토리 화면 구현함
            // 4-1. 장비 장착 화면 구현
            // 5. 선택 화면 확장

            GameDataSetting(); // atl + enter 메서드 생성하기
            PrintStartLogo();
            StartMenu();
        }

        static void StartMenu()
        {
            Console.Clear();
            Console.WriteLine("===========================================================");
            Console.WriteLine("스파르타 마을에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 던전으로 들어가기전 활동을 할 수 있습니다.");
            Console.WriteLine("===========================================================");
            Console.WriteLine("");
            Console.WriteLine("1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("");

            // int keyInput = int.Parse(Console.ReadLine()); -> 유저들일 착할때만 가능함. 없는 메뉴 입력하면 불가능함.

            //유저가 아무거나 입력했을 때 점검하는 함수
            switch (CheckValidInput(1, 2))
            {
                case 1:
                    StatusMenu();
                    break;
                case 2:
                    InventoryMenu();
                    break;
            }
        }

        private static void InventoryMenu()
        {
            Console.Clear();

            ShowHighlightedText("■ 인벤토리 ■");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");

            for (int i = 0; i < Item.ItemCnt; i++)
            {

                _items[i].PrintItemStatDescription();
            }
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");
            Console.WriteLine("1. 장착관리");
            Console.WriteLine("");
            switch (CheckValidInput(0, 1))
            {
                case 0:
                    StartMenu();
                    break;
                case 1:
                    EquipMenu();
                    break;
            }

        }

        private static void EquipMenu()
        {
            Console.Clear();

            ShowHighlightedText("■ 인벤토리 - 장착 관리 ■");
            Console.WriteLine("보유 중인 아이템을 관리할 수 있습니다.");
            Console.WriteLine("");
            Console.WriteLine("[아이템 목록]");
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                _items[i].PrintItemStatDescription(true, i + 1);
            }
            Console.WriteLine("");
            Console.WriteLine("0. 나가기");

            int keyInput = CheckValidInput(0, Item.ItemCnt);

            switch (keyInput)
            {
                case 0:
                    InventoryMenu();
                    break;
                default:  // 모든 케이스가 아니어서 마지막에 남은 케이스를 사용할 때 사용
                    ToggleEquipStatus(keyInput - 1);  // 유저가 입력하는 건 1,2,3. 실제 배역은 0,1,2
                    //ToggleEquipStatus : 아이템의 장착 상태를 바꾸는 것
                    EquipMenu();
                    break;
            }
        }

        private static void ToggleEquipStatus(int idx)
        {
            _items[idx].IsEquipped = !_items[idx].IsEquipped;
        }

        private static void StatusMenu()
        {
            Console.Clear();

            //상태보기
            ShowHighlightedText("■ 상태 보기 ■");
            Console.WriteLine("캐릭터의 정보가 표기됩니다.");

            PrintTextWithHighlights("LV. ", _player.Level.ToString("00")); //0이 생략되지 않고 나옴 ex 01 07
            Console.WriteLine("");
            Console.WriteLine("{0} ({1})", _player.Name, _player.Job);
            //{0}chad, {1}번쨰 전사 

            int bonusAtk = getSumBonusAtk();
            PrintTextWithHighlights("공격력 : ", (_player.Atk + bonusAtk).ToString(), bonusAtk > 0 ? string.Format(" (+{0})", bonusAtk) : "");
            int bonusDef = getSumBonusDef();
            PrintTextWithHighlights("방어력 : ", (_player.Def + bonusDef).ToString(), bonusDef > 0 ? string.Format(" (+{0})", bonusDef) : "");
            int bonusHP = getSumBonusHP();
            PrintTextWithHighlights("체력 : ", (_player.HP + bonusHP).ToString(), bonusHP > 0 ? string.Format(" (+{0})", bonusHP) : "");
            PrintTextWithHighlights("골드 : ", _player.Gold.ToString());

            Console.WriteLine("");
            Console.WriteLine("0. 뒤로가기");
            Console.WriteLine("");
            switch (CheckValidInput(0, 0))
            {
                case 0:
                    StartMenu();
                    break;
            }
        }

        private static int getSumBonusAtk()
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                if (_items[i].IsEquipped) sum += _items[i].Atk;
            }
            return sum;
        }
        private static int getSumBonusDef()
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                if (_items[i].IsEquipped) sum += _items[i].Def;
            }
            return sum;
        }
        private static int getSumBonusHP()
        {
            int sum = 0;
            for (int i = 0; i < Item.ItemCnt; i++)
            {
                if (_items[i].IsEquipped) sum += _items[i].HP;
            }
            return sum;
        }




        private static void ShowHighlightedText(string text)
        {
            //글자 색깔 바꿀 수 있음
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(text);
            Console.ResetColor();
        }

        private static void PrintTextWithHighlights(string s1, string s2, string s3 = "")
        {
            Console.Write(s1);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(s2);
            Console.ResetColor();
            Console.WriteLine(s3);
        }

        private static int CheckValidInput(int min, int max)
        {
            // 아래 두 가지 상황은 비정상 -> 재입력 요청
            // 1) 숫자가 아닌 입력을 받은 경우
            // 2) 숫자가 최소값 ~ 최대값의 범위를 넘는 경우
            int keyinput;  // 미리 정하는 이유는 tryParse에 필요하기 때문임
            bool result;  // while문에 필요하기 떄문
            do // 일단 한 번 실행을 보장
            {
                Console.WriteLine("원하는 행동을 입력해주세요.");
                result = int.TryParse(Console.ReadLine(), out keyinput);
                // TryParse는 될 수도 있고, 안 될 수도 있다고 생각하는 것(숫자 일수도, 아닐 수도 있다고 생각 )
                // ReadLine을 한 다음 맞다면, 가져올 것(out keyinput)
            }
            while (result == false || CheckIfValid(keyinput, min, max) == false);
            // false이거나 CheckIfValid로 min, max 내의 범위에 들어가는지 판단 했을 때 false라면 계속 반복

            // 여기에 왔다는 것은 제대로 입력을 받았다는 것
            return keyinput;

        }

        private static bool CheckIfValid(int keyinput, int min, int max)
        //CheckIfValid에 기능 추가
        {
            if (min <= keyinput && keyinput <= max) return true;
            // 만약 keyinput이 min, max 범위 내에 들어온다 -> true
            return false;
        }

        private static void PrintStartLogo()
        {
            Console.WriteLine("========================================================================================");
            Console.WriteLine("              ███████   ██████     █████    ██████   ████████    █████ ");
            Console.WriteLine("              ██        ██   ██   ██   ██   ██   ██     ██      ██   ██");
            Console.WriteLine("              ███████   ██████    ███████   ██████      ██      ███████");
            Console.WriteLine("                   ██   ██        ██   ██   ██   ██     ██      ██   ██");
            Console.WriteLine("              ███████   ██        ██   ██   ██   ██     ██      ██   ██");


            Console.WriteLine("       ██████    ██    ██   ███    ██    ██████    ███████    ██████    ███    ██ ");
            Console.WriteLine("       ██   ██   ██    ██   ████   ██   ██         ██        ██    ██   ████   ██ ");
            Console.WriteLine("       ██   ██   ██    ██   ██ ██  ██   ██   ███   █████     ██    ██   ██ ██  ██ ");
            Console.WriteLine("       ██   ██   ██    ██   ██  ██ ██   ██    ██   ██        ██    ██   ██  ██ ██ ");
            Console.WriteLine("       ██████     ██████    ██   ████    ██████    ███████    ██████    ██   ████ ");
            Console.WriteLine("========================================================================================");
            Console.WriteLine("                               PRESS ANYKEY TO START                                    ");
            Console.WriteLine("========================================================================================");
            Console.ReadKey();
        }

        private static void GameDataSetting()
        {
            _player = new Character("chad", "전사", 1, 10, 5, 100, 1500);
            _items = new Item[10];
            Additem(new Item("무쇠갑옷", "무쇠로 만들어져 튼튼한 갑옷입니다.", 0, 0, 5, 0)); // // Type 0, Atk 0, Def 5, HP 0
            Additem(new Item("낡은 검", "쉽게 볼 수 있는 낡은 검입니다.", 1, 2, 0, 0)); // Type 1, Atk 2, Def 0,  HP 0

        }

        static void Additem(Item item)
        {
            if (Item.ItemCnt == 10) return;
            _items[Item.ItemCnt] = item; //0개 -> 0번 인덱스 / 1개 -> 1번 인덱스
            Item.ItemCnt++;
        }
    }
}


