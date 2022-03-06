using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepInversion
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Dependency inversion -- 5-й принцип SOLID, суть в том что модули программы зависят от абстракций, нежели от конкретных реализаций
            // Dependency injection - внедрение конкретной зависимости, см. пример ниже - внедрение в конструктор
            // IoC - inversion of control, или инверсия управления, вместо программиста какие-то действия делает программа
            // IoC контейнер - фреймворк для внедрения зависимостей, имеет несколько методов: 
            // Register() - зарегстрнировать зависимость, псевдокод Register<IFlyable, Pigeon>();
            var popug = new Pigeon();

            var zoo = new Zoo(popug);
            var showResult = zoo.Show();

            Console.WriteLine(showResult);
            Console.ReadLine();
        }
    }

    public interface IFlyable
    {
        string Fly();
    }

    public class Parrot: IFlyable
    {
        public string Fly()
        {
            return "попуг летает";
        }
    }

    public class Zoo
    {
        private IFlyable _somefly;

        public Zoo(IFlyable somefly)
        {
            _somefly = somefly;
        }

        public string Show()
        {
            return _somefly.Fly();
        }
    }

    public class Pigeon : IFlyable
    {
        string IFlyable.Fly()
        {
            return "Голубь полетел";
        }
    }
}
