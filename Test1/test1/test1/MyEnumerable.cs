using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace test1
{
    public class MyEnumerator : IEnumerator<int>
    {
        // list - состояние, не должно храниться в енумераторе, должно быть полем Enumerable, возможно хранится ссылка, но не копия
        private List<int> list;
        private readonly int icount;
        private List<int> listpassed;
        public MyEnumerator(List<int> list)
        {         
            if (list == null || list.Count ==0)
            {
                icount = -1;
                return;
            }
            this.list = list;
            icount = 0;
            listpassed = new List<int>();
        }

        // Current не должен изменять состояние
        // Current должен возвращать текущее состояние, без мутации списка
        public int Current
        {
            get
            {
                if (icount == -1) return 0;

                if (listpassed.Count == list.Count) return 0;

                int rValue = new Random().Next(list.Count);
                while (listpassed.Contains(rValue))
                {
                    rValue = new Random().Next(list.Count);
                }
                listpassed.Add(rValue);
                return list[rValue];
            }
        }
        object IEnumerator.Current => throw new NotImplementedException();
        public void Dispose() {}

        public bool MoveNext()
        {
            if (icount == -1) return false;
            if (listpassed.Count == list.Count) return false;
            return true;
        }

        public void Reset()
        {
            if(icount == -1) return;

            listpassed = new List<int>();
        }
    }

    public class MyEnumerable : IEnumerable<int>
    {
        private List<int> _list;
        private readonly MyEnumerator myEnumerator;
        public readonly int Count;

        public MyEnumerable(List<int> list)
        {
            _list = list;
            myEnumerator = new MyEnumerator(list);
            Count = list.Count;
        }
        public IEnumerator<int> GetEnumerator()
        {
            return myEnumerator;
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        void Add(int n)
        {
            _list.Add(n);
        }
    }
}
