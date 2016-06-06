using System;
using System.Collections;
using System.Diagnostics;
using System.Windows.Threading;

namespace Reactive.Bindings
{
    internal static class CoroutineWorker
    {
        static readonly MicroCoroutine coroutine = new MicroCoroutine(ex => Trace.WriteLine(ex));

        static CoroutineWorker()
        {
            var timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(33.33);
            timer.Tick += (sender, e) => coroutine.Run();
            timer.Start();
        }

        public static void AddCoroutine(IEnumerator coroutineEnumerator)
        {
            coroutine.AddCoroutine(coroutineEnumerator);
        }
    }
}