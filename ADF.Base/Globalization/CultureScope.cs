using System;
using System.Globalization;
using System.Threading;

namespace Adf.Base.Globalization
{
    public sealed class CultureScope : IDisposable
    {
        private readonly CultureInfo _currentCulture;
        private readonly CultureInfo _currentUICulture;

        public CultureScope(CultureInfo cultureInfo = null)
        {
            var currentThread = Thread.CurrentThread;
            _currentCulture = currentThread.CurrentCulture;
            _currentUICulture = currentThread.CurrentUICulture;

            if (cultureInfo == null)
                currentThread.CurrentCulture = currentThread.CurrentUICulture; // Set CurrentCulture to current UICulture
            else
                currentThread.CurrentCulture = currentThread.CurrentUICulture = cultureInfo; // Set CurrentCulture / CurrentUICulture to given cultureinfo
        }

        public void Dispose()
        {
            Thread.CurrentThread.CurrentCulture = _currentCulture;
            Thread.CurrentThread.CurrentUICulture = _currentUICulture;
        }
    }
}
