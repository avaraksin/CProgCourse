using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepInversion
{
    public enum PROGMODE
    {
        DEVELOP,
        PROD
    }
    public abstract class MyExample
    {
        public MyExample() {}
    }

    public class Example_DEV : MyExample
    {
        public Example_DEV() : base() {}
        //public override 
    }

    public class Example_PROD : MyExample
    {
        public Example_PROD() : base() {}
    }

    public static class ExampleController
    {
        public static MyExample GetExample()
        {
            if (prog.progmode == PROGMODE.DEVELOP) return new Example_DEV();
            return new Example_PROD();
        }
    }

    public class prog
    {
        public static  PROGMODE progmode =  PROGMODE.DEVELOP;
        public void main()
        {
            MyExample example = ExampleController.GetExample();
        }
    }

}
