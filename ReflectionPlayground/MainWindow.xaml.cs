using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using GenericLineOfBusiness.Common.Interfaces;

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
                var listAddMethod = listType.GetMethod("Add", new[] { typeof(int) });
                foreach (var value in Enumerable.Range(0, MaxCount))
                {
                    listAddMethod.Invoke(list, new object[] { value });
                }
                OutputTextBox.Text = string.Format("Added {0} items using reflection", MaxCount);
            }
        }

        private readonly List<IPersonRepository> _repositories = new List<IPersonRepository>();
        private IPersonRepository _selectedRepository;

        private void LoadRepositories_OnClick(object sender, RoutedEventArgs e)
        {
            using (new MyTimer(PerformanceTextBlock))
            {
                var outputBuilder = new StringBuilder();
                var path = Directory.GetCurrentDirectory();
                outputBuilder.AppendLine("Checking: " + path);
                var assemblies = Directory.GetFiles(path, "*.dll").Select(Assembly.LoadFile);
                foreach (var assembly in assemblies)
                {
                    var repositoryType = assembly.ExportedTypes
                        .Where(t => t.IsClass)
                        .FirstOrDefault(t => typeof(IPersonRepository).IsAssignableFrom(t));
                    if (repositoryType != null)
                    {
                        var repository = assembly.CreateInstance(repositoryType.ToString()) as IPersonRepository;
                        _repositories.Add(repository);
                        outputBuilder.AppendLine("Added Type: " + repositoryType.FullName);
                    }
                }
                OutputTextBox.Text = outputBuilder.ToString();
            }
        }

        private void ChangeRepository_OnClick(object sender, RoutedEventArgs e)
        {
            using (new MyTimer(PerformanceTextBlock))
            {
                if (_selectedRepository == null)
                {
                    _selectedRepository = _repositories.FirstOrDefault();
                }
                else
                {
                    var currentIndex = _repositories.IndexOf(_selectedRepository);
                    if (_repositories.Count > currentIndex + 1)
                    {
                        _selectedRepository = _repositories[currentIndex + 1];
                    }
                    else
                    {
                        _selectedRepository = _repositories[0];
                    }
                }

                if (_selectedRepository == null)
                {
                    LoadedTextBlock.Text = "None";
                }
                else
                {
                    LoadedTextBlock.Text = _selectedRepository.GetType().ToString();
                }
            }
        }

        private void RepositoryGetAll_OnClick(object sender, RoutedEventArgs e)
        {
            using (new MyTimer(PerformanceTextBlock))
            {
                if (_selectedRepository == null)
                {
                    OutputTextBox.Text = "No Repository to load from";
                    return;
                }

                var people = _selectedRepository.GetAll();
                var peopleOutput = string.Join("\r\n", people);
                OutputTextBox.Text = peopleOutput;
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
