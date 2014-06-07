using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace ReflectionPlayground
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            using (new MyTimer(PerformanceTextBlock))
            {
                var path = Directory.GetCurrentDirectory();
                var assemblies = Directory.GetFiles(path, "*.dll").Select(Assembly.LoadFile);
                var assembliesOutput = string.Join("\r\n", assemblies);
                OutputTextBox.Text = assembliesOutput;
            }
        }
    }

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
