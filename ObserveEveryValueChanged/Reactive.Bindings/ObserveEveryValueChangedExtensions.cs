using System;
using System.Collections;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Reactive.Bindings
{
    public static class ObserveEveryValueChangedExtensions
    {
        /// <summary>
        /// Publish target property when value is changed. If source is destructed, publish OnCompleted.
        /// </summary>
        public static IObservable<TProperty> ObserveEveryValueChanged<TSource, TProperty>(this TSource source, Func<TSource, TProperty> propertySelector)
            where TSource : class
        {
            if (source == null) return Observable.Empty<TProperty>();

            var reference = new WeakReference(source);
            source = null;

            return Observable.Create<TProperty>(observer =>
            {
                var d = new BooleanDisposable();
                var coroutine = PublishPocoValueChanged(reference, propertySelector, observer, d);
                CoroutineWorker.AddCoroutine(coroutine);
                return d;
            });
        }

        static IEnumerator PublishPocoValueChanged<TSource, TProperty>(WeakReference sourceReference, Func<TSource, TProperty> propertySelector, IObserver<TProperty> observer, BooleanDisposable cancellationToken)
        {
            var comparer = System.Collections.Generic.EqualityComparer<TProperty>.Default;

            var isFirst = true;
            var currentValue = default(TProperty);
            var prevValue = default(TProperty);

            while (!cancellationToken.IsDisposed)
            {
                var target = sourceReference.Target;
                if (target != null)
                {
                    try
                    {
                        currentValue = propertySelector((TSource)target);
                    }
                    catch (Exception ex)
                    {
                        observer.OnError(ex);
                        yield break;
                    }
                    finally
                    {
                        target = null; // remove reference(must need!)
                    }
                }
                else
                {
                    observer.OnCompleted();
                    yield break;
                }


                if (isFirst || !comparer.Equals(currentValue, prevValue))
                {
                    isFirst = false;
                    observer.OnNext(currentValue);
                    prevValue = currentValue;
                }

                yield return null;
            }
        }
    }
}