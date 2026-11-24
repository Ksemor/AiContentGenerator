using System.Windows.Forms;

namespace AiContentGenerator.WinForms.UI;

// Главное окно приложения (Windows Forms)
// Создается полностью программной версткой без .Designer.cs во избежание сбоев визуального дизайнера
public class MainForm : Form
{
    public MainForm()
    {
        Text = "Генератор контента с ИИ";
        Width = 1000;
        Height = 700;
        StartPosition = FormStartPosition.CenterScreen;
    }
}
