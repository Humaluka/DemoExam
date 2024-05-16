using DinaD.Models;
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
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public static int countOfErrors = 0;
        public Authorization()
        {
            InitializeComponent();
            
        }

        private string GenerateCaptcha()
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, 4).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private async void ToAuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(IdNumberTextBlock.Text, out int userId))
            {
                string userPass = PasswordTextBlock.Password;

                using (praktikaVesnaEntities db = new praktikaVesnaEntities())
                {
                    var person = db.People.FirstOrDefault(p => p.Id == userId && p.Password == userPass);
                    if (person != null)
                    {
                        if (person.RoleId == 3)
                        {
                            UserInfo.UserId = person.Id;
                            UserInfo.UserName = person.Name;
                            UserInfo.RoleId = person.RoleId;
                            Organizer organizer = new Organizer();
                            organizer.Show();
                            this.Close();

                        }
                    }
                    else
                    {
                        countOfErrors++;
                        if (countOfErrors >= 3)
                        {
                            MessageBox.Show("Программа остановлена на 10 сек");
                            await Task.Delay(10000); 

                            countOfErrors = 0; 

                        }
                        else
                        {
                            string captcha = GenerateCaptcha();
                            Captcha captchaWindow = new Captcha(captcha);
                            captchaWindow.Show();
                            this.Close();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("ID должен быть числом", "Ошибка ввода", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



    }
}
