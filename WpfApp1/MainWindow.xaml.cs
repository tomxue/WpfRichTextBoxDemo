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
            rtb.Document = new FlowDocument(paragraph);

            var from = "  user1";
            var text = "chat message goes here";
            paragraph.Inlines.Add(new Bold(new Run(from + ": "))
            {
                Background = Brushes.Yellow
            });
            paragraph.Inlines.Add(text);
            //paragraph.Inlines.Add(new LineBreak());

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
    }
}
