using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test1
{
    
    public interface IMyEnumerator<T>
    {
        T Current { get; }
        bool MoveNext();
        void Reset();
    }

    public class RandomListEnumerator : IMyEnumerator<int>
    {
        List<int> currentList;  // текущий список из которого извлекаем элементы
        int icount;             // текущий указатель
        List<int> storeList;    // сохраненный список для Reset()

        // конструктор, принимает список интов
        public RandomListEnumerator(List<int> list)
        {
            if (list == null || list.Count == 0)
            {
                icount = -1;
                return;
            }

            currentList = new List<int>(list);
            storeList = new List<int>(list);

            icount = new Random().Next(list.Count);
        }

        // получить текущее значение итератора
        public int Current
        {
            get
            {
                if (icount == -1) return 0;

                int current = currentList[icount];

                if (currentList.Count > 1)
                {
                    currentList.RemoveAt(icount);
                }

                return current;
            }
        }

        // сдвинуть итератор так, чтобы он выдал следующий случайный элемент списка
        // вернёт true, если мы успешно перешли к следующему элементу коллекции
        // или false, если мы уже в конце коллекции находимся
        public bool MoveNext()
        {
            if (icount == -1) return false;

            if (currentList.Count == 1)
            {
                icount = 0;
                return false;
            }

            icount = new Random().Next(currentList.Count);

            return true;
        }

        // сбросить итератор, в следующий раз MoveNext() начнёт сначала
        public void Reset()
        {
            if (icount == -1) return;

            currentList = new List<int>(storeList);
            icount = new Random().Next(currentList.Count);
        }
    }
    public interface IRandomEnumerable<T> 
    {
         IMyEnumerator<T> GetEnumerator();
    }

    public class RandomList : IRandomEnumerable<int>
    {
        RandomListEnumerator enumerator;
        public RandomList(List<int> list)
        {
            enumerator = new RandomListEnumerator(list);
        }
        public  IMyEnumerator<int> GetEnumerator()
        {
            return enumerator;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // ***************************************** ЗАДАНИЕ 1 ************************************************************************ 
       
            var list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            RandomListEnumerator rle = new RandomListEnumerator(list);

            // тут мы должны вывести все элементы 
            for (;;)
            {
                Console.WriteLine(rle.Current);
                if (!rle.MoveNext())
                {
                    Console.WriteLine(rle.Current);
                    break;
                }
            }

            // тут мы по идее уже прошли все элементы коллекции, так что должен возвращаться просто последний
            rle.MoveNext();
            Console.WriteLine(rle.Current);

            rle.MoveNext();
            Console.WriteLine(rle.Current);

            // сбрасываем счётчик
            rle.Reset();

    // тут мы должны снова вывести все элементы, должны выводиться в новом случайном порядке
            for (;;)
            {
                Console.WriteLine(rle.Current);
                if (!rle.MoveNext())
                {
                    Console.WriteLine(rle.Current);
                    break;
                }
            }
            
            // ************************************ ЗАДАНИЕ 2 *************************************************************************
            /*
            RandomList randomList = new RandomList(new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 });
            IMyEnumerator<int> randomEnumerator = randomList.GetEnumerator();

            while (true)
            {
                Console.WriteLine(randomEnumerator.Current);
                if( !randomEnumerator.MoveNext() ) break;
            }
            Console.WriteLine(randomEnumerator.Current); // Выводим последний элемент
            */

            // ********************************************* ЗАДАНИЕ 3 ****************************************************************
            List<int> lst = new List<int>() { 132, 241, 35, 14, 578, 106, 1071, 81 };
            MyEnumerable myEnumerable = new MyEnumerable( lst );

            foreach(int me in myEnumerable)
            {
                Console.WriteLine(me);
            }

            Console.WriteLine();
            // Проверяем первоначальный список
            foreach(var l in lst)
            {
                Console.WriteLine(l);
            }

            //Сбрасываем IEnumerate
            myEnumerable.GetEnumerator().Reset();
            Console.WriteLine();

            //Выводим список еще раз по нашему алгоритму
            for (int i = 0; i < myEnumerable.Count; i++)
            {
                Console.WriteLine(myEnumerable.GetEnumerator().Current);
            }

            Console.ReadKey();
        }
    }
}
