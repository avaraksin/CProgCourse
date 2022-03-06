using System;
using System.Collections.Generic;
using System.Linq;

namespace MyApp // Note: actual namespace depends on the project name.
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Rectangle r = new Rectangle();
            r.SetA(5);
            r.SetB(6);
            Console.WriteLine(r.Area());

            Console.WriteLine(VerifyArea(r));
            Square s = new Square();
            s.SetA(5);
            s.SetB(6);
            Console.WriteLine(s.Area());

            Console.WriteLine(VerifyArea(s));

            Rectangle r1 = new Square();
            r1.SetA(5);
            r1.SetB(6);

            Console.WriteLine(VerifyArea(r1));

            Console.WriteLine(r.Area());
        }

        public static bool VerifyArea(Rectangle r)
        {
            r.SetA(5);
            r.SetB(6);

            return r.Area() == 30;
        }
    }



    public class Rectangle
    {
        public int a { get; set; }

        public virtual void SetA(int n)
        {
            a = n;
        }

        public int b { get; set; }

        public virtual void SetB(int n)
        {
            b = n;
        }

        public virtual int Area()
        {
            return a * b;
        }
    }

    public class Square : Rectangle
    {
        private int a;

        public override void SetA(int n) {
            a = n;
        }

        public override void SetB(int n)
        {
            a = n;
        }

        public override int Area()
        {
            return a * a;
        }
    }

}