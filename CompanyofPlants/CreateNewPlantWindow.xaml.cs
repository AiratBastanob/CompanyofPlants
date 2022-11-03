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

namespace CompanyofPlants
{
    /// <summary>
    /// Логика взаимодействия для CreateNewPlantWindow.xaml
    /// </summary>
    public partial class CreateNewPlantWindow : Window
    {
        private Plants plants = new Plants();
        public CreateNewPlantWindow()
        {
            InitializeComponent();
            this.DataContext = plants;
        }
       
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
        private void SavePlantsButton_Click(object sender, RoutedEventArgs e)
        {            
            if (plants.Date_planting == null)
                MessageBox.Show("Введите дату посадки растение", "Уведомление");
            if (plants.Plant_age.ToString() == null)
                MessageBox.Show("Введите возраст растение", "Уведомление");
            if (plants.Sort_plant == null)
                MessageBox.Show("Введите сорт растение", "Уведомление");

            if (plants.Date_planting != null && plants.Plant_age.ToString() != null || plants.Sort_plant != null)
                planting_parksEntities.GetContext().Plants.Add(plants);
            try
            {
                planting_parksEntities.GetContext().SaveChanges();
                MessageBox.Show("Растение добавлено", "Уведомление");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
           
        }
    }
}
