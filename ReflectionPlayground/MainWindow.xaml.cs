using System;
using System.Collections.Generic;
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

        private const int MaxCount = 1000000;

        private void LoadAssemblies_OnClick(object sender, RoutedEventArgs e)
        {
            using (new MyTimer(PerformanceTextBlock))
            {
                var path = Directory.GetCurrentDirectory();
                var assemblies = Directory.GetFiles(path, "*.dll").Select(Assembly.LoadFile);
                var assembliesOutput = string.Join("\r\n", assemblies);
                OutputTextBox.Text = assembliesOutput;
            }
        }

        private void ListAdd_OnClick(object sender, RoutedEventArgs e)
        {
            using (new MyTimer(PerformanceTextBlock))
            {
                var list = new List<int>();
                foreach (var value in Enumerable.Range(0, MaxCount))
                {
                    list.Add(value);
                }
                OutputTextBox.Text = string.Format("Added {0} items using instance", MaxCount);
            }
        }

        private void ListAddReflection_OnClick(object sender, RoutedEventArgs e)
        {
            using (new MyTimer(PerformanceTextBlock))
            {
                var list = new List<int>();
                var listType = list.GetType();
                var listAddMethod = listType.GetMethod("Add", new[] {typeof(int)});
                foreach (var value in Enumerable.Range(0, MaxCount))
                {
                    listAddMethod.Invoke(list, new object[] { value });
                }
                OutputTextBox.Text = string.Format("Added {0} items using reflection", MaxCount);
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
