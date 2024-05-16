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
    /// Interaction logic for Organizer.xaml
    /// </summary>
    public partial class Organizer : Window
    {
        praktikaVesnaEntities db;

        public Organizer()
        {
            InitializeComponent();
            InitializeGreetings();
            db = new praktikaVesnaEntities();

        }

        private void InitializeGreetings()
        {
            db = new praktikaVesnaEntities();

            var person = db.People.Where(n => n.Id == UserInfo.UserId).FirstOrDefault();

            DataContext = person;

            string greeting = "";

            int hour = DateTime.Now.Hour;

            if (hour >= 9 && hour <= 11)
                greeting = "Доброе утро, ";
            else if (hour > 11 && hour <= 18)
                greeting = "Добрый день, ";
            else
                greeting = "Добрый вечер,";

            string name = db.People.Where(n => n.Id == UserInfo.UserId).Select(x => x.Name).FirstOrDefault();
            greeting += name;

            GreetingsLabel.Text = greeting;
        }


        private void EventsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ParticipantsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void JuryButton_Click(object sender, RoutedEventArgs e)
        {
            JuryOrModeratorRegister juryOrModeratorRegister = new JuryOrModeratorRegister();
            juryOrModeratorRegister.Show();
            this.Close();
        }

        private void ProfileButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
