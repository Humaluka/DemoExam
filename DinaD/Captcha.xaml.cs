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
using System.Windows.Shapes;

namespace DinaD
{
    /// <summary>
    /// Interaction logic for Captcha.xaml
    /// </summary>
    public partial class Captcha : Window
    {
        private string captcha;

        public Captcha(string generatedCaptcha)
        {
            InitializeComponent();
            captcha = generatedCaptcha;
            CaptchaDisplay.Text = captcha;
            Loaded += Captcha_Loaded;
        }

        private void CheckCaptchaButton_Click(object sender, RoutedEventArgs e)
        {
            string userInput = CaptchaTextBox.Text;
            if (userInput == captcha)
            {
                Authorization toAuth = new Authorization();
                toAuth.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неправильная капча! Пожалуйста, попробуйте еще раз.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Captcha_Loaded(object sender, RoutedEventArgs e)
        {
            AddNoise();
        }

        private void AddNoise()
        {
            Random rand = new Random();
            DrawingVisual visual = new DrawingVisual();
            using (DrawingContext context = visual.RenderOpen())
            {
                for (int i = 0; i < 5; i++)
                {
                    double x = rand.Next((int)CaptchaDisplay.ActualWidth);
                    double y = rand.Next((int)CaptchaDisplay.ActualHeight);
                    double size = rand.Next(1, 2);
                    Brush brush = Brushes.Black;
                    Pen pen = new Pen(brush, 1);
                    context.DrawEllipse(brush, pen, new Point(x, y), size, size);
                }
            }
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)CaptchaDisplay.ActualWidth, (int)CaptchaDisplay.ActualHeight, 96, 96, PixelFormats.Default);
            bmp.Render(visual);
            CaptchaDisplay.Background = new ImageBrush(bmp);
        }
    }
}
