using System;
using System.Collections.Generic;
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
using System.Text.RegularExpressions;

namespace MyEditorialConverter {
    /// <summary>
    /// メインウィンドウ
    /// </summary>
    public partial class MainWindow : Window {

        #region Constructor
        public MainWindow() {
            InitializeComponent();
        }
        #endregion

        #region Form Event
        private void Window_Loaded(object sender, RoutedEventArgs e) {
            System.Reflection.Assembly assenbley = System.Reflection.Assembly.GetExecutingAssembly();
            this.Title = "社説コンバーター(" + assenbley.GetName().Version + ")";
        }
        #endregion

        #region Control Event
        private void cSrcTitle_TextChanged(object sender, TextChangedEventArgs e) {
            this.cDestTitle.Text = DateTime.Now.ToString("yyyy/MM/dd ") + this.cSrcTitle.Text.Replace("　", " ").Trim();
        }

        private void cSrcText_TextChanged(object sender, TextChangedEventArgs e) {
            var lines = this.cSrcText.Text.Split('\n');
            for(int i=0; i < lines.Length; i++) {
                lines[i] = lines[i].Replace("　", " ").Trim();
                lines[i] = ConvertToWiderNumber(lines[i]);
            }

            FlowDocument doc = this.cDestText.Document;
            TextRange range = new TextRange(doc.ContentStart, doc.ContentEnd);
            this.cDestText.Tag = string.Join("\r\n", lines);
            range.Text = this.cDestText.Tag.ToString();
            var regex = new Regex("（.*?）");
            var matches = regex.Matches(this.cDestText.Tag.ToString().Replace("\r\n", ""));
            var hilightColor = new SolidColorBrush(Color.FromArgb(255, 249,205, 173));
            foreach (Match match in matches) {
                var p1 = GetPoint(this.cDestText.Document.ContentStart, match.Index);
                var p2 = GetPoint(this.cDestText.Document.ContentStart, match.Index + match.Length);

                this.cDestText.Selection.Select(p1, p2);
                TextRange tr = new TextRange(p1, p2);
                tr.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Blue);
                tr.ApplyPropertyValue(TextElement.BackgroundProperty, hilightColor);
            }
        }

        private TextPointer GetPoint(TextPointer start, int pos) {
            var result = start;
            int i = 0;
            while (i < pos) {
                if (result.GetPointerContext(LogicalDirection.Forward) == TextPointerContext.Text) {
                    i++;
                }
                if (result.GetPositionAtOffset(1, LogicalDirection.Forward) == null) {
                    return result;
                }
                result = result.GetPositionAtOffset(1, LogicalDirection.Forward);
            }
            return result;
        }


        private void CopyDestTile_Click(object sender, RoutedEventArgs e) {
            Clipboard.SetText(this.cDestTitle.Text);
        }

        private void CopyDestText_Click(object sender, RoutedEventArgs e) {
            //Clipboard.SetText(this.cDestText.Text);
            Clipboard.SetText(this.cDestText.Tag.ToString());
        }
        #endregion

        #region Private Method
        private string ConvertText(string[] textLines) {
            var result = new StringBuilder();

            foreach (var line in textLines) {
                var text = line.Replace("　", " ").Trim();
                text = this.ConvertToWiderNumber(text);

                result.Append(text + '\n');
            }


            return result.ToString();
        }

        private string ConvertToWiderNumber(string text) {
            return Regex.Replace(text, "[0-9]", (Match match) => ((char)(match.Value[0] - '0' + '０')).ToString());
        }
        #endregion
    }
}
