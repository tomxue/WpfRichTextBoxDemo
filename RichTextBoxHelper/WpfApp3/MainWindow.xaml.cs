using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp3
{
    public class ViewModel
    {
        public Model model { get; set; }

        public ViewModel()
        {
            model = new Model()
            {
                ID = "001",
                Name = "Daisy",
                Description = "<FlowDocument xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\"><Paragraph Foreground=\"Red\"><Bold>Hello, this is RichTextBox</Bold></Paragraph></FlowDocument>"
            };
        }
    }

    public class Model
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            
        }

        private static TextPointer GetTextPointAt(TextPointer from, int pos)
        {
            TextPointer ret = from;
            int i = 0;

            while ((i < pos) && (ret != null))
            {
                if ((ret.GetPointerContext(LogicalDirection.Backward) == TextPointerContext.Text) || (ret.GetPointerContext(LogicalDirection.Backward) == TextPointerContext.None))
                    i++;

                if (ret.GetPositionAtOffset(1, LogicalDirection.Forward) == null)
                    return ret;

                ret = ret.GetPositionAtOffset(1, LogicalDirection.Forward);
            }

            return ret;
        }

        internal string Select(RichTextBox rtb, int offset, int length, SolidColorBrush brush)
        {
            rtb.SelectAll();
            // Get text selection:
            TextSelection textRange = rtb.Selection;

            // Get text starting point:
            TextPointer start = rtb.Document.ContentStart;

            // Get begin and end requested:
            TextPointer startPos = GetTextPointAt(start, offset);
            TextPointer endPos = GetTextPointAt(start, offset + length);

            // New selection of text:
            textRange.Select(startPos, endPos);

            // Apply property to the selection:
            textRange.ApplyPropertyValue(TextElement.BackgroundProperty, brush);

            // Return selection text:
            return rtb.Selection.Text;
        }

        private void rtf_SelectionChanged(object sender, RoutedEventArgs e)
        {
            RichTextBox rtb = sender as RichTextBox;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Select(rtf, 15, 2, Brushes.AliceBlue);
            Select(rtf, 0, 5, Brushes.Yellow);
            Select(rtf, 7, 4, Brushes.Pink);
        }
    }
}
