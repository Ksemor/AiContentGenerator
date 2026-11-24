using System.Windows.Forms;
using AiContentGenerator.WinForms.UI;

namespace AiContentGenerator.WinForms;

// Точка входа в приложение
static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        Application.Run(new MainForm());
    }
}
