#region

using System;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Navy.Core.Threading
{
    /// <summary>
    /// </summary>
    public class ThreadHelper
    {
        private static TimeSpan _distance = TimeSpan.FromMilliseconds(25);

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
                if (timeout > TimeSpan.Zero && DateTime.UtcNow - date > timeout)
                    return false;
            }

            return true;
        }

        public static bool WaitTrue(Func<bool> whileState, TimeSpan timeout)
        {
            return WaitTrue(whileState, timeout, _distance);
        }

        public static bool WaitTrue(Func<bool> whileState, TimeSpan timeout, TimeSpan interval)
        {
            var ct = new CancellationTokenSource().Token;
            var date = DateTime.UtcNow;
            while (!whileState())
            {
                if (!Sleep(interval, ct, interval))
                    return false;
                if (timeout == TimeSpan.Zero)
                    return false;
                if (DateTime.UtcNow - date > timeout)
                    return false;
            }

            return true;
        }

        public static bool WaitFalse(Func<bool> whileState, TimeSpan timeout)
        {
            return WaitFalse(whileState, timeout, _distance);
        }

        public static bool WaitFalse(Func<bool> whileState, TimeSpan timeout, TimeSpan interval)
        {
            return WaitTrue(() => !whileState(), timeout, interval);
        }

        public static bool WaitNotNull(Func<object> whileState, TimeSpan timeout)
        {
            return WaitNotNull(whileState, timeout, _distance);
        }

        public static bool WaitNotNull(Func<object> whileState, TimeSpan timeout, TimeSpan interval)
        {
            return WaitTrue(() => whileState() != null, timeout, interval);
        }


        public static bool Sleep(Func<bool> whileState, CancellationToken ct, TimeSpan timeout)
        {
            return Sleep(whileState, ct, timeout, _distance);
        }


        public static bool Sleep(Func<bool> whileState, CancellationToken ct)
        {
            return Sleep(whileState, ct, TimeSpan.Zero);
        }

        public static bool Sleep(Func<bool> whileState, TimeSpan timeout)
        {
            return Sleep(whileState, CancellationToken.None, timeout);
        }

        public static bool Sleep(Func<bool> whileState, TimeSpan timeout, TimeSpan interval)
        {
            return Sleep(whileState, CancellationToken.None, timeout, interval);
        }

        public static bool Sleep(Func<bool> whileState)
        {
            return Sleep(whileState, CancellationToken.None, TimeSpan.Zero);
        }
    }
}