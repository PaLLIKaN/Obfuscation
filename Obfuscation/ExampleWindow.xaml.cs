using System.Windows;

namespace Obfuscation
{
    /// <summary>
    /// Логика взаимодействия для ExampleWindow.xaml
    /// </summary>
    public partial class ExampleWindow : Window
    {
        
        public ExampleWindow()
        {
            InitializeComponent();
            Inputting.Text = System.IO.File.ReadAllText("Example/Input.cs");
            Outputting.Text = System.IO.File.ReadAllText("Example/Output.cs");
        }
    }
}
