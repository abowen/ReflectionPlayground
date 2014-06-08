using System;
using System.Diagnostics;
using System.Windows.Controls;

namespace ReflectionPlayground.Utilities
{
    public class MyTimer : IDisposable
    {
        private readonly TextBlock _textBlock;
        private Stopwatch _stopwatch;

        public MyTimer(TextBlock textBlock)
        {
            _textBlock = textBlock;
            _stopwatch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            if (_stopwatch == null) return;

            _stopwatch.Stop();
            _textBlock.Text = string.Format("{0}ms", _stopwatch.ElapsedMilliseconds);
            _stopwatch = null;
        }
    }
}
