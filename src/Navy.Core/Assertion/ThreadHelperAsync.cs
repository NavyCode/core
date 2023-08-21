using System;
using System.Threading;
using System.Threading.Tasks;

namespace Navy.Core.Assertion
{
    public class ThreadHelperAsync
    {
        static readonly TimeSpan DefaultInterval = TimeSpan.FromMilliseconds(25);

        static async Task<bool> SleepAsync(TimeSpan time, CancellationToken ct)
        {  
            await Task.Delay(time, ct);
            if (Thread.CurrentThread.ThreadState == ThreadState.StopRequested)
                return false;
            if (ct.IsCancellationRequested)
                return false;
            return true;
        }

        public static async Task<bool> WaitTrueAsync(Func<Task<bool>> func, TimeSpan timeout, TimeSpan? interval = null)
        {
            var ct = new CancellationTokenSource().Token;
            var date = DateTime.UtcNow;
            while (!await func())
            {
                if (!await SleepAsync(interval ?? DefaultInterval, ct))
                    return false;
                if (timeout == TimeSpan.Zero)
                    return false;
                if (DateTime.UtcNow - date > timeout)
                    return false;
            } 
            return true;
        } 

        public static async Task<bool> WaitFalseAsync(Func<Task<bool>> func, TimeSpan timeout, TimeSpan? interval = null)
        {
            return await WaitTrueAsync(async () => await func() == false, timeout, interval);
        } 
    }
}
