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

namespace MyEditorialConverter {
    public class ImageButton : Button {

        static ImageButton() {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ImageButton), new FrameworkPropertyMetadata(typeof(ImageButton)));
        }

        public static readonly DependencyProperty DefaultImageProperty =
                                                        DependencyProperty.Register("DefaultImage",     // プロパティ名
                                                                                    typeof(string),     // プロパティの型
                                                                                    typeof(ImageButton),        // プロパティを所有する型
                                                                                    new FrameworkPropertyMetadata("")); // メタデータの設定
        public static readonly DependencyProperty HoverImageProperty =
                                                        DependencyProperty.Register("HoverImage",
                                                                                    typeof(string),
                                                                                    typeof(ImageButton),
                                                                                    new FrameworkPropertyMetadata(""));
        public static readonly DependencyProperty PressedImageProperty =
                                                        DependencyProperty.Register("PressedImage",
                                                                                    typeof(string),
                                                                                    typeof(ImageButton),
                                                                                    new FrameworkPropertyMetadata(""));

        public String DefaultImage {
            get { return (string)GetValue(DefaultImageProperty); }
            set { SetValue(DefaultImageProperty, value); }
        }

        public string HoverImage {
            get { return (string)FindResource( GetValue(HoverImageProperty)); }
            set { SetValue(HoverImageProperty, value); }
        }

        public string PressedImage {
            get { return (string)GetValue(PressedImageProperty); }
            set { SetValue(PressedImageProperty, value); }
        }

    }
}
