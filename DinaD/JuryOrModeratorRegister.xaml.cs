using DinaD.Models;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DinaD
{
    /// <summary>
    /// Interaction logic for JuryOrModeratorRegister.xaml
    /// </summary>
    public partial class JuryOrModeratorRegister : Window
    {
        private praktikaVesnaEntities _context;
        public JuryOrModeratorRegister()
        {
            InitializeComponent();
            _context = new praktikaVesnaEntities();
            LoadComboBoxes();
            SetRandomIdNumber();
        }

        public void UserImagesComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UserImagesComboBox.SelectedItem != null)
            {
                string selectedImageFile = UserImagesComboBox.SelectedItem.ToString();
                string imagesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UsersImages", selectedImageFile);
                if (File.Exists(imagesPath))
                {
                    try
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.UriSource = new Uri(imagesPath, UriKind.Absolute);
                        bitmap.CacheOption = BitmapCacheOption.OnLoad;
                        bitmap.EndInit();
                        UserImage.Source = bitmap;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show($"Файл не найден: {imagesPath}");
                }
            }
        }

        public void LoadComboBoxes()
        {
            SexComboBox.ItemsSource = _context.Sexes.ToList();
            SexComboBox.DisplayMemberPath = "Name";
            SexComboBox.SelectedValuePath = "Id";

            CountryComboBox.ItemsSource = _context.Countries.ToList();
            CountryComboBox.DisplayMemberPath = "Name";
            CountryComboBox.SelectedValuePath = "Id";

            FieldComboBox.ItemsSource = _context.Directions.ToList();
            FieldComboBox.DisplayMemberPath = "Name";
            FieldComboBox.SelectedValuePath = "Id";

            EventComboBox.ItemsSource = _context.Events.ToList();
            EventComboBox.DisplayMemberPath = "Name";
            EventComboBox.SelectedValuePath = "Id";

            RoleComboBox.Items.Add(new ComboBoxItem { Content = "Жюри", Tag = 1 });
            RoleComboBox.Items.Add(new ComboBoxItem { Content = "Модератор", Tag = 2 });

            UserImagesComboBox.Items.Add("foto1.jpg");
            UserImagesComboBox.Items.Add("foto2.jpg");
            UserImagesComboBox.Items.Add("foto3.jpg");
            UserImagesComboBox.Items.Add("foto4.jpg");
            UserImagesComboBox.Items.Add("foto5.jpg");
        }

        public void OkButton_Click(object sender, RoutedEventArgs e)
        {
            string password = PasswordBox.Password;
            string reEnteredPassword = ReEnterPasswordBox.Password;
            string fullName = FullNameTextBox.Text;
            string email = EmailTextBox.Text;
            string phoneNumber = PhoneNumberTextBox.Text;
            
            if (string.IsNullOrEmpty(fullName) ||
                SexComboBox.SelectedValue == null ||
                RoleComboBox.SelectedValue == null ||
                string.IsNullOrEmpty(email) ||
                string.IsNullOrEmpty(phoneNumber) ||
                FieldComboBox.SelectedValue == null ||
                CountryComboBox.SelectedValue == null ||
                string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(reEnteredPassword))
            {
                MessageBox.Show("Все поля должны быть заполнены.");
                return;
            }

            if (!IsPasswordValid(password) || password != reEnteredPassword)
            {
                MessageBox.Show("Пароль не соответствует требованиям: \n• не менее 6 символов;\n• заглавные и строчные буквы;\n• не менее одного спецсимвола;\n• не менее одной цифры.\n\nИли пароли не совпадают.");
                return;
            }

            if (!IsFullNameValid(fullName))
            {
                MessageBox.Show("ФИО должно быть в формате: Фамилия Имя Отчество или Фамилия Имя.");
                return;
            }

            if (!IsEmailValid(email))
            {
                MessageBox.Show("Email должен быть в формате реальной электронной почты.");
                return;
            }

            if (!IsPhoneNumberValid(phoneNumber))
            {
                MessageBox.Show("Телефон должен быть в формате реального российского телефона (например, 79172379339 или 89172379339).");
                return;
            }

            if (LinkToEventCheckBox.IsChecked == true && EventComboBox.SelectedValue == null)
            {
                MessageBox.Show("Пожалуйста, выберите мероприятие.");
                return;
            }

            int idNumber;
            if (!int.TryParse(IdNumberTextBox.Text, out idNumber) || _context.People.Any(p => p.Id == idNumber))
            {
                MessageBox.Show("Пользователь с таким Id уже зарегистрирован.");
                return;
            }

            string selectedImageFile = UserImagesComboBox.SelectedItem?.ToString();

            var person = new Person
            {
                Id = idNumber,
                Name = fullName,
                EMail = email,
                PhoneNumber = phoneNumber,
                Password = password,
                SexId = (int)SexComboBox.SelectedValue,
                RoleId = (int)((ComboBoxItem)RoleComboBox.SelectedItem).Tag,
                DirectionId = (int)FieldComboBox.SelectedValue,
                CountryId = (int)CountryComboBox.SelectedValue,
                Image = selectedImageFile
            };

            _context.People.Add(person);
            _context.SaveChanges();

            if (LinkToEventCheckBox.IsChecked == true)
            {
                var selectedEvent = _context.Events.Find((int)EventComboBox.SelectedValue);
                selectedEvent.People.Add(person);
                _context.SaveChanges();
            }


            MessageBox.Show("Регистрация успешна!");
            Organizer organizer = new Organizer();
            organizer.Show();
            this.Close();
        }

        public static bool IsPasswordValid(string password)
        {
            if (password.Length < 6) return false;
            if (!password.Any(char.IsUpper)) return false;
            if (!password.Any(char.IsLower)) return false;
            if (!password.Any(char.IsDigit)) return false;
            if (!password.Any(ch => !char.IsLetterOrDigit(ch))) return false;

            return true;
        }

        public static bool IsFullNameValid(string fullName)
        {
            var regex = new Regex(@"^([A-Za-zА-Яа-я]+\s[A-Za-zА-Яа-я]+(\s[A-Za-zА-Яа-я]+)?)$");
            return regex.IsMatch(fullName);
        }

        public static bool IsEmailValid(string email)
        {
            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            return regex.IsMatch(email);
        }

        public static bool IsPhoneNumberValid(string phoneNumber)
        {
            var regex = new Regex(@"^7\d{10}$|^8\d{10}$");
            return regex.IsMatch(phoneNumber);
        }

        public void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Organizer organizer = new Organizer();
            organizer.Show();
            this.Close();
        }

        public void SetRandomIdNumber()
        {
            Random rnd = new Random();
            int randomNumber;
            do
            {
                randomNumber = rnd.Next(1, 100000);
            } while (_context.People.Any(p => p.Id == randomNumber));

            IdNumberTextBox.Text = randomNumber.ToString("D5");
        }

        public void UnHidePasswordCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            PasswordTextBox.Text = PasswordBox.Password;
            ReEnterPasswordTextBox.Text = ReEnterPasswordBox.Password;
            PasswordBox.Visibility = Visibility.Collapsed;
            ReEnterPasswordBox.Visibility = Visibility.Collapsed;
            PasswordTextBox.Visibility = Visibility.Visible;
            ReEnterPasswordTextBox.Visibility = Visibility.Visible;
        }

        public void UnHidePasswordCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            PasswordBox.Password = PasswordTextBox.Text;
            ReEnterPasswordBox.Password = ReEnterPasswordTextBox.Text;
            PasswordBox.Visibility = Visibility.Visible;
            ReEnterPasswordBox.Visibility = Visibility.Visible;
            PasswordTextBox.Visibility = Visibility.Collapsed;
            ReEnterPasswordTextBox.Visibility = Visibility.Collapsed;
        }
    }
}
