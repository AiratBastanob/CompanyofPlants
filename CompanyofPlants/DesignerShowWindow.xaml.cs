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
using System.Windows.Navigation;
using System.Data;

namespace CompanyofPlants
{
    /// <summary>
    /// Логика взаимодействия для DesignerShowWindow.xaml
    /// </summary>
    public partial class DesignerShowWindow : Window
    {
        public DesignerShowWindow()
        {
            InitializeComponent();
            DataGridDesginer.ItemsSource = planting_parksEntities.GetContext().Designers.ToList();    ///Присвоение атрибуту элемента для вывода списка декораторов таблицы Designers
        }

        public void UpdateDesignersList()
        {
            var DesignerName = planting_parksEntities.GetContext().Designers.ToList();

            DesignerName = DesignerName.Where(p => p.LFM.ToLower().Contains(SearchTextBox_name.Text.ToLower())).ToList();
            DataGridDesginer.ItemsSource = DesignerName.OrderBy(p => p.LFM).ToList();     ///Возврат имени декоратора, совпавших с введённым значением в текстбокс 
        }

        private void DeleteDesignerButton_Click(object sender, RoutedEventArgs e)
        {
            var DesignerForDeleting = DataGridDesginer.SelectedItems.Cast<Designers>().FirstOrDefault();

            if (MessageBox.Show($"Вы точно хотите удалить декоратора {DesignerForDeleting.LFM}?", "Внимание!",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    planting_parksEntities.GetContext().Designers.Remove(DesignerForDeleting);
                    planting_parksEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данный декоратор удален!", "Уведомление");
                    DataGridDesginer.ItemsSource = planting_parksEntities.GetContext().Designers.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }     
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Close();
        }
        private void ChangeDesignerButton_Click(object sender, RoutedEventArgs e)
        {           
            planting_parksEntities.GetContext().SaveChanges();
        }
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                planting_parksEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());        ///Обновление сущностей в GetContext и выполнение Reload (перезагрузки значения таблицы) для каждого элемента
                DataGridDesginer.ItemsSource = planting_parksEntities.GetContext().Designers.ToList();
            }
        }        
        private void SearchTextBox(object sender, TextChangedEventArgs e)
        {
            UpdateDesignersList();      ///Вызов метода обновления списка декораторов по событию изменения текста внутри SearchTextBox
        }
    }
}
