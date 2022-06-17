using System;
using System.IO;
using System.Timers;
using System.Collections.Generic;

namespace HomeWork9
{
    class Pet
    {
        List<string> list;

        public Pet(string dir = "pig.data")
        {
            list = new List<string>();
            using (StreamReader sr = new StreamReader(dir))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    list.Add(line);
                }
            }
        }
        public void Show()
        {
            foreach (string str in list)
            {
                foreach (char symb in str)
                    if (symb == '0')
                        Console.Write("  ");
                    else
                        Console.Write("##");
                Console.WriteLine();
            }
        }
    }
    public enum Wish
    {
        None,
        Eat,
        Walk,
        Sleep,
        Ill,
        Play
    }
    class Tamagochi
    {
        Pet pet;
        Wish wish; //желание питомца
        byte countRefusal; //количество отказов
        bool isIll; //болеет ли питомец
        Timer timer; 

        void ChangeWish()
        {
            var rand = new Random();
            int temp;
            do
            {
                temp = rand.Next(1, 6);
            } while (temp == (int)wish);
            wish = (Wish)temp;
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            timer.Stop();
            Console.Clear();
            if (isIll)
            {
                Console.WriteLine("Ваш питомец умер. Игра окончена!");
                pet = null;
                return;
            }
            pet.Show();
            if (countRefusal < 3)
                ChangeWish();
            else
                wish = Wish.Ill;
            switch (wish)
            {
                case Wish.None:
                    break;
                case Wish.Eat:
                    Console.Write("Я хочу кушать!"); break;
                case Wish.Walk:
                    Console.Write("Я хочу гулять!"); break;
                case Wish.Sleep:
                    Console.Write("Я хочу спать!"); break;
                case Wish.Ill:
                    Console.Write("Я заболел, меня надо полечить!"); isIll = true; break;
                case Wish.Play:
                    Console.Write("Я хочу играть!"); break;
                default:
                    break;
            }
            Console.Write(" (Нажмите пробел, чтобы удовлетворить питомца): ");
            if(wish != Wish.None)
                countRefusal++;
            ConsoleKey? key = new ConsoleKey();
            if ((key = Console.ReadKey().Key) == ConsoleKey.Spacebar)
            {
                if (countRefusal > 0)
                {
                    countRefusal--;
                    if (isIll)
                    {
                        if(countRefusal == 3)
                            countRefusal--;
                        isIll = false;
                    }
                }
            }
        }
        public Tamagochi()
        {
            pet = new Pet();
            wish = Wish.None;
            countRefusal = 0;
            isIll = false;
            timer = new Timer();
            timer.Elapsed += new ElapsedEventHandler(timer_Tick);
            timer.Interval = 5000;
        }
        public void Play()
        {
            Console.Clear();
            pet.Show();
            while (pet != null)
            {
                timer.Start();
            }
        }
    }

    internal class Program
    {
        public static void Main()
        {
            Tamagochi tamagochi = new Tamagochi();
            tamagochi.Play();
        }
    }
}
