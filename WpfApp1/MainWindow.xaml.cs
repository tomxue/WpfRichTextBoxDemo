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
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Paragraph paragraph = new Paragraph();
            FlowDocument doc = rtb.Document = new FlowDocument(paragraph);

            var from = "  user1";
            var text = "chat message goes here";
            paragraph.Inlines.Add(new Bold(new Run(from + ": "))
            {
                Background = Brushes.Yellow
            });
            paragraph.Inlines.Add(text);
            //paragraph.Inlines.Add(new LineBreak());

            paragraph.Inlines.Add(new Run(from + ": ")
            {
                Background = Brushes.Yellow
            });
            paragraph.Inlines.Add(text);

            TextPointer contentStart = doc.ContentStart.GetNextInsertionPosition(LogicalDirection.Forward);
            var nextStart = contentStart.GetLineStartPosition(0);

            TextPointer pointer = doc.ContentStart;
            TextPointer len = nextStart.GetPositionAtOffset(8);
            TextRange wordRange = new TextRange(nextStart, len);
            wordRange.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Red);

            TextPointer start = pointer.GetPositionAtOffset(15);    // set below indexStart to this argument
            len = start.GetPositionAtOffset(4); // set below (indexEnd - indexStart) to this argument as the length
            TextRange wordRange2 = new TextRange(start, len);
            wordRange2.ApplyPropertyValue(TextElement.BackgroundProperty, Brushes.Blue);

            string richText = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd).Text;
            Debug.WriteLine($"Current rich text is:{richText}");
        }

        private void RichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            RichTextBox rtb = sender as RichTextBox;
            var docStart = rtb.Document.ContentStart;
            var indexStart = docStart.GetOffsetToPosition(rtb.Selection.Start);
            var indexEnd = docStart.GetOffsetToPosition(rtb.Selection.End);

            TextRange start = new TextRange(docStart, rtb.Selection.Start);
            TextRange end = new TextRange(docStart, rtb.Selection.End);
            int indexStart_abs = start.Text.Length;
            int indexEnd_abs = end.Text.Length;

            if (rtb.Selection.Text != "")
            {
                Debug.WriteLine($"Current selection is:{rtb.Selection.Text}, Start: {indexStart}, End: {indexEnd}, " +
                    $"indexStart_abs: {indexStart_abs}, indexEnd_abs: {indexEnd_abs}");
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            rtb.Document.Blocks.Clear();
            rtb.Document.Blocks.Add(new Paragraph(new Run("Set new text string: 123")));
        }

        private void GetContentButton_Click(object sender, RoutedEventArgs e)
        {
            string richText = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd).Text;
            Debug.WriteLine($"Get rich text content is:{richText}");
        }

        private void ChangeWidthButton_Click(object sender, RoutedEventArgs e)
        {
            rtb.Width -= 20;
        }
    }
}
