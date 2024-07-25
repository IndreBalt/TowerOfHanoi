using System.Drawing;
using System.Net.WebSockets;
using System.Runtime.InteropServices;
using System.Threading.Channels;

namespace TowerOfHanoi
{
    public class Program
    {
        static int towerSize = 0;
        static int moves = 0;
        static List<int> tower1 = new List<int>();
        static List<int> tower2 = new List<int>();
        static List<int> tower3 = new List<int>();
  

        static int lowestDisc1 = 0;
        static int lowestDisc2 = 0;
        static int firstTower = 0;
        static int secondTower = 0;
        
        
        static void Main(string[] args)
        {
            Console.WriteLine("TOWERS OF HANOI");
            Console.WriteLine();
            do
            {
                Console.WriteLine("Ivesti boksto dydi iki 9");
                towerSize = int.Parse(Console.ReadLine());
                if (towerSize <= 0 || towerSize > 9)
                {
                    Console.WriteLine("Netinkamas dydis, veskite dar karta");
                }
            } while (towerSize <= 0 || towerSize > 9);
            FillTowersLists(towerSize);
            Console.Clear();
            do
            {
                GameWindow();
                firstTower = FirstTowerChoose();
                GameWindow();
                secondTower = SecondTowerChoose();
                ToTakeDisc(firstTower);
                ToPutDisk(secondTower);


            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }

        public static int ToTakeDisc(int tower)//disko paemimas is boksto
        {
            List<int> towerDisckList = new List<int>();
            if (tower == 1)
            {
                towerDisckList = tower1;
            }
            else if (tower == 2)
            {
                towerDisckList = tower2;
            }
            else if (tower == 3)
            {
                towerDisckList = tower3;
            }
            int index = towerDisckList.IndexOf(lowestDisc1);
            if (firstTower == tower)
            {
                towerDisckList[index] = 0;
            }
            return index;
            
        } 
        public static (int, int) ToPutDisk(int tower)//disko padejimas ir ejimu skaiciavimas
        {
            List<int> towerDisckList = new List<int>();
            if(tower == 1)
            {
                towerDisckList = tower1;
            }
            else if (tower == 2)
            {
                towerDisckList = tower2;
            }
            else if (tower == 3)
            {
                towerDisckList = tower3;
            }

            int index = towerDisckList.IndexOf(lowestDisc2);
            if (secondTower == tower) //idedam diska
            { 
                
                if (towerDisckList[towerSize-1] == 0)
                {
                    towerDisckList[towerSize - 1] = lowestDisc1;
                }
                else
                {
                    towerDisckList[index-1] = lowestDisc1;
                }  
            }
            lowestDisc1 = 0;//isvalom pirmo boksto pasirinkima
            moves++;//pridedam ejima
            return (index, moves);
        }
        public static int FirstTowerChoose()//Pasirenkamas pirmas bokstas ir grazinama jo skaicius is meniu
        {
            int towerPickNumberFirst = 0;
            do
            {
                towerPickNumberFirst = TowerPick("pirmo");
                lowestDisc1 = ChoseTowerAndReurnsLowestValue(towerPickNumberFirst);
                if (lowestDisc1 == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("stulpelis tuscias, bandykit dar karta");
                    Console.ResetColor();
                }
            }while(lowestDisc1 == 0);
            return towerPickNumberFirst;
            
        }
        public static int SecondTowerChoose() //pasirenkamas antras bokstas ir grazinamas jo skaicius is meniu
        {
            int towerPickNumberSecond = 0;
            do
            {
                towerPickNumberSecond = TowerPick("antro");
                lowestDisc2 = ChoseTowerAndReurnsLowestValue(towerPickNumberSecond);
                if(firstTower == towerPickNumberSecond)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Is sio boksto imamas diskas");
                    Console.ResetColor();
                }
                if (lowestDisc1 > lowestDisc2 && lowestDisc2 != 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Negalima didesnio disko deti ant mazesnio");
                    Console.ResetColor();
                }
            } while (firstTower == towerPickNumberSecond || (lowestDisc1 > lowestDisc2 && lowestDisc2 != 0));
            

            return towerPickNumberSecond;
        } 
        public static void GameWindow()//spausina visa zaidimo langa
        {
            Console.Clear();
            Console.WriteLine("TOWERS OF HANOI");
            Console.WriteLine($"Ejimai: {moves}");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Norint baigti zaidima spauskite ESC");
            Console.ResetColor();
            Console.WriteLine();
            PrintTowers(tower1, tower2, tower3, lowestDisc1);

        }
        public static string DiskcCreation(int diskNumber) //piesiam diska
        {
            string allDisk = "";          
            int diskSize = 10;
            for (int j = diskSize - diskNumber; j >= 1; j--)
            {
                allDisk += " ";
            }
            for (int k = diskNumber; k >= 1; k--)
            {
                allDisk += "X";
            }
            allDisk += diskNumber;
            for (int l = diskNumber; l >= 1; l--)
            {
                allDisk += "X";
            }
            for (int m = diskSize - diskNumber; m >= 1; m--)
            {
                allDisk += " ";
            }
            return allDisk;           
        }     
        public static void PrintTowers(List<int> tower1, List<int> tower2, List<int> tower3, int lowestDisc) //spausdina bokstus
        {
            var color1 = ConsoleColor.Gray;
            var color2 = ConsoleColor.Gray;
            var color3 = ConsoleColor.Gray;
            var towerColor1 = ConsoleColor.Gray;
            var towerColor2 = ConsoleColor.Gray;
            var towerColor3 = ConsoleColor.Gray;
            var colorBlue = ConsoleColor.Blue;
            Console.WriteLine();
            for (int i = 0; i < tower1.Count; i++)
            {
                
                if (tower1[i] == lowestDisc && lowestDisc > 0)
                {
                    color1 = colorBlue;
                    towerColor1 = colorBlue;
                }
                if (tower2[i] == lowestDisc && lowestDisc > 0)
                {
                    color2 = colorBlue;
                    towerColor2 = colorBlue;
                }
                if (tower3[i] == lowestDisc && lowestDisc > 0)
                {
                    color3 = colorBlue;
                    towerColor3 = colorBlue;
                }

                Console.ForegroundColor = color1;
                Console.Write(DiskcCreation(tower1[i]));
                color1 = ConsoleColor.Gray;
                Console.ResetColor();
                Console.ForegroundColor = color2;
                Console.Write(DiskcCreation(tower2[i]));
                color2 = ConsoleColor.Gray;
                Console.ResetColor();
                Console.ForegroundColor = color3;
                Console.Write(DiskcCreation(tower3[i]));
                color3 = ConsoleColor.Gray;                
                Console.ResetColor();                
                Console.WriteLine();
            }
            
            Console.WriteLine();
            Console.ResetColor();
            string bokstas1 = "     Bokstas nr.1     ";
            string bokstas2 = "     Bokstas nr.2     ";
            string bokstas3 = "     Bokstas nr.3     ";
            
            Console.ForegroundColor = towerColor1;
            Console.Write(bokstas1);
            Console.ResetColor();
            Console.ForegroundColor = towerColor2;
            Console.Write(bokstas2);
            Console.ResetColor();
            Console.ForegroundColor = towerColor3;
            Console.Write(bokstas3);
            towerColor1 = ConsoleColor.Gray;
            towerColor2 = ConsoleColor.Gray;
            towerColor3 = ConsoleColor.Gray;
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine();

        }
        public static (List<int>, List<int>, List<int>) FillTowersLists(int towerSize)// uzpildom bokstu Listus
        {
            Console.WriteLine();
            for (int i = 1;i <= towerSize;i++)
            {
                tower1.Add(i);
                tower2.Add(0);
                tower3.Add(0);
            }  
            return (tower1, tower2, tower3);
        }
        public static int TowerPick(string bokstas)//grazina vartotojo pasirinkima
        {
            int towerNumber = 0;
            do
            {
                Console.WriteLine($"Pasirinkite {bokstas} boksto numeri");
                int.TryParse(Console.ReadLine(), out towerNumber);
                if (towerNumber < 1 || towerNumber > 3)
                {
                    Console.WriteLine("Tokio boksto nera");
                }
            } while (towerNumber < 1 || towerNumber > 3);


            return towerNumber;
        }
        public static int FindFirstLowestValue(List<int> tower)//Randa maziausia reiksme bokste
        {
            int minValue = 0;
            int tuscia = 0;
            for (int i = 0; i < tower.Count; i++)
            {
                if (tower[i] == minValue)
                {
                    minValue = tuscia;
                }
                else
                {
                    minValue = tower[i];
                    break;
                }
            }
                      
            return minValue;
        }
        public static int ChoseTowerAndReurnsLowestValue(int towerPickNumber)// boksto priskyrimas pasirinkimui ir grazina maziausia diska
        {
            int lowestDisc = 0;
            switch (towerPickNumber)
            {
                case 1:
                    Console.WriteLine("Pasirinktas pirmas");
                    lowestDisc = FindFirstLowestValue(tower1);
                    break;
                case 2:
                    Console.WriteLine("Pasirinktas antras");
                    lowestDisc = FindFirstLowestValue(tower2);
                    break;
                case 3: 
                    Console.WriteLine("Pasirinktas trecias");
                    lowestDisc = FindFirstLowestValue(tower3);
                    break;
                default: 
                    Console.WriteLine("Tokio nera");
                    break;

            }
            return lowestDisc;
        }

    }
}
