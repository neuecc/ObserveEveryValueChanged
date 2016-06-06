using System;
using System.Collections;
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Threading;

namespace Reactive.Bindings
{
    internal static class CoroutineWorker
    {
        static readonly MicroCoroutine coroutine = new MicroCoroutine(ex => Trace.WriteLine(ex));

        static CoroutineWorker()
        {
            CompositionTarget.Rendering += RunMicroCoroutine;
        }

        private static void RunMicroCoroutine(object sender, EventArgs e)
        {
            coroutine.Run();
        }

        public static void AddCoroutine(IEnumerator coroutineEnumerator)
        {
            coroutine.AddCoroutine(coroutineEnumerator);
        }
    }
}