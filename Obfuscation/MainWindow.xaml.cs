using System;
using System.Windows;
using Обфускация.Class;

namespace Obfuscation
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Selection_сode(object sender, RoutedEventArgs e) // Получение кода в string
        {
            
            InputKod.Text = null;
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            openFileDlg.Filter = "C# Source Files (*.cs)|*.cs";
            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {
                InputKod.Text = System.IO.File.ReadAllText(openFileDlg.FileName); //Помещение ввыбранного кода пользователем
            }
            else
            {
                MessageBox.Show("Выберите файл с текстом программы!", "Файл с текстом программы не выбран", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }

        }


        private void Performing_obfuscation(object sender, RoutedEventArgs e) // Выполнение обфускации
        {
            if (InputKod.Text == "")
            {
                MessageBox.Show("Выберите другой файл с текстом программы", "Файл с текстом программы был выбран неправильно", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                Obfuscations obfuscation = new Obfuscations();
                obfuscation.Kod = InputKod.Text;
                obfuscation.Deleting_comments();
                obfuscation.Deleting_extra_characters();
                obfuscation.Changing_the_name_of_variables();
                obfuscation.Adding_false_comments();
                if (obfuscation.Kod == "")
                {
                    MessageBox.Show("Выберите другой файл с текстом программы", "Файл с текстом программы не имеет строк функционального кода", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
                else
                {
                    OutputKod.Text = obfuscation.Kod; //Вывод обфусцированного кода на экран
                }

            }
        }

        private void Save_kod(object sender, RoutedEventArgs e) // Сохранение текста программы в файл
        {
            if (OutputKod.Text == "")
            {
                MessageBox.Show("Выберите файл с текстом программы, или нажмите кнопку <<Выполнить обфускацию>>", "Текст программы не был выбран или не обцусцирован", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                Microsoft.Win32.SaveFileDialog saveFileDlg = new Microsoft.Win32.SaveFileDialog();
                saveFileDlg.Filter = "C# Source Files (*.cs)|*.cs"; // Тип файла
                Nullable<bool> result = saveFileDlg.ShowDialog();
                if (result == true)
                {
                    System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(saveFileDlg.FileName);
                    streamWriter.WriteLine(OutputKod.Text);
                    streamWriter.Close();
                }
            }
        }

        private void Example_obfuscation(object sender, RoutedEventArgs e)
        {
            bool isWindowOpen = false;
            foreach (Window w in Application.Current.Windows)
            {
                if (w is ExampleWindow)
                {
                    isWindowOpen = true;
                    w.Activate();
                }
            }

            if (!isWindowOpen)
            {
                ExampleWindow newwindow = new ExampleWindow();
                newwindow.Show();
            }
        }
    }
}
