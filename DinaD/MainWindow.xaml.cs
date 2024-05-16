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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.Entity; 


namespace DinaD
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        praktikaVesnaEntities db;

        public MainWindow()
        {
            InitializeComponent();

            db = new praktikaVesnaEntities();
            EventsListView.ItemsSource = db.Events.Include(e => e.Direction).ToList();
            DirectionComboBox.ItemsSource = db.Directions.ToList();
            DirectionComboBox.DisplayMemberPath = "Name"; 
            DirectionComboBox.SelectedValuePath = "Id";
        }

        private void ToAuthorizationButton_Click(object sender, RoutedEventArgs e)
        {
            Authorization toAuthorization = new Authorization();
            toAuthorization.Show();
            this.Close();
        }

        private void OrderByDateCheckBox_Checked(object sender, RoutedEventArgs e)
        {

            EventsListView.ItemsSource = db.Events.Include(eventItem => eventItem.Direction).OrderBy(eventItem => eventItem.Date).ToList();

        }

        private void OrderByDateCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            EventsListView.ItemsSource = db.Events.Include(eventItem => eventItem.Direction).ToList();
        }

        private void DirectionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DirectionComboBox.SelectedValue != null)
            {
                int selectedDirectionId = Convert.ToInt32(DirectionComboBox.SelectedValue);
                EventsListView.ItemsSource = db.Events.Include(ev => ev.Direction)
                                                     .Where(ev => ev.DirectionId == selectedDirectionId)
                                                     .ToList();
            }
            else
            {
                EventsListView.ItemsSource = db.Events.Include(ev => ev.Direction).ToList();
            }
        }
    }
   
}
