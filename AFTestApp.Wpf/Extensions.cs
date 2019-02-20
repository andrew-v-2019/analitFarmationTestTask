using System.Linq;
using System.Windows.Controls;

namespace AFTestApp.Wpf
{
    public static class Extensions
    {
        public static void FormatTextBoxForOnlyDigits(this TextBox textBox)
        {
            var textBoxText = textBox.Text.Trim();
            var newValue = string.Join("", textBoxText.Where(c => char.IsDigit(c) && c != '0'));
            textBox.Text = newValue;
            textBox.CaretIndex = newValue.Length;
        }
    }
}
