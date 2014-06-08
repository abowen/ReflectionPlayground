using System.Collections.Generic;
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
    /// Interaction logic for RepositoryView.xaml
    /// </summary>
    public partial class RepositoryView : UserControl
    {
        public RepositoryView()
        {
            InitializeComponent();
        }

        private readonly List<IPersonRepository> _repositories = new List<IPersonRepository>();
        private IPersonRepository _selectedRepository;

        private void LoadRepositories_OnClick(object sender, RoutedEventArgs e)
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

        private void ChangeRepository_OnClick(object sender, RoutedEventArgs e)
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

        private void RepositoryGetAll_OnClick(object sender, RoutedEventArgs e)
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
