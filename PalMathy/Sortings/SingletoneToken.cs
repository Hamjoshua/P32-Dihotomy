using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace PalMathy.Sortings
{
    class Singletone<T> where T: new()
    {
        static private T _instance;
        private static object syncRoot = new Object();

        static public T Instance
        {
            get
            {
                lock (syncRoot)
                {
                    if (_instance == null)
                    {
                        _instance = new T();
                    }
                }
                return _instance;
            }
        }

        public Singletone()
        {

        }
    }

    class CancelToken : Singletone<CancelToken>
    {
        public void Cancel()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
            cancellationTokenSource = new CancellationTokenSource();
        }
        public CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();        
    }
}
