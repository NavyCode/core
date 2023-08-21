#region

using System;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Navy.Core.Assertion
{
    public class ThreadHelper
    {
            private static TimeSpan _defaultInterval = TimeSpan.FromMilliseconds(25);

            public static bool Sleep(TimeSpan time, CancellationToken ct, TimeSpan interval)
            {
                var date = DateTime.UtcNow;
                do
                {
                    Task.Delay(interval, ct).Wait(ct);
                    if (Thread.CurrentThread.ThreadState == ThreadState.StopRequested)
                        return false;
                    if (ct.IsCancellationRequested)
                        return false;
                } while (DateTime.UtcNow - date < time);

                return true;
            }

            public static bool Sleep(Func<bool> whileState, CancellationToken ct, TimeSpan timeout, TimeSpan interval)
            {
                var date = DateTime.UtcNow;
                while (whileState())
                {
                    if (!Sleep(interval, ct, interval))
                        return false;
                    if (DateTime.UtcNow - date > timeout)
                        return false;
                }

                return true;
            }
         

            public static bool WaitTrue(Func<bool> whileState, TimeSpan timeout, TimeSpan? interval = null)
            {
                var ct = new CancellationTokenSource().Token;
                var date = DateTime.UtcNow;
                while (!whileState())
                {
                    if (!Sleep(interval ?? _defaultInterval, ct, interval ?? _defaultInterval))
                        return false;
                    if (timeout == TimeSpan.Zero)
                        return false;
                    if (DateTime.UtcNow - date > timeout)
                        return false;
                } 
                return true;
            } 

            public static bool WaitFalse(Func<bool> whileState, TimeSpan timeout, TimeSpan? interval = null)
            {
                return WaitTrue(() => !whileState(), timeout, interval);
            } 

            public static bool WaitNotNull(Func<object> whileState, TimeSpan timeout, TimeSpan? interval = null)
            {
                return WaitTrue(() => whileState() != null, timeout, interval);
            }
        } 
}