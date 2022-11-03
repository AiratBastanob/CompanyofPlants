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
using Word = Microsoft.Office.Interop.Word;

namespace CompanyofPlants
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void DesignerShowButton_Click(object sender, RoutedEventArgs e)
        {
            DesignerShowWindow designerShow = new DesignerShowWindow();
            designerShow.Show();
            Hide();
        }

        private void ChangeDesignerInfoButton_Click(object sender, RoutedEventArgs e)
        {
            DesignerShowWindow designerShow = new DesignerShowWindow();
            designerShow.Show();
            Hide();
        }
        private void EmployesShowButton_Click(object sender, RoutedEventArgs e)
        {
            EmployesShowWindow employShow = new EmployesShowWindow();
            employShow.Show();
            Hide();
        }
        private void CreateNewPlantButton_Click(object sender, RoutedEventArgs e)
        {
            CreateNewPlantWindow createnewplant = new CreateNewPlantWindow();
            createnewplant.Show();
            Hide();
        }
        private void DeleteDesignerButton_Click(object sender, RoutedEventArgs e)
        {
            DesignerShowWindow designerShow = new DesignerShowWindow();
            designerShow.Show();
            Hide();
        }
        private void ShowInfoPlantButton_Click(object sender, RoutedEventArgs e)
        {
            List<Plants> plants;
            using (planting_parksEntities planting = new planting_parksEntities())
            {
                plants = planting.Plants.ToList().OrderBy(g => g.Id).ToList();
                var entryTypes = plants
                        .GroupBy(s => s.Sort_plant);         ///Разделение таблиц по сортам

                var app = new Word.Application();
                Word.Document document = app.Documents.Add();
                int i = 0;
                if (plants.First().Id != 1)
                {
                    i = plants.First().Id - 1;
                }
                            
                foreach (var group in entryTypes)
                {
                    Word.Paragraph paragraph = document.Paragraphs.Add();
                    Word.Range range = paragraph.Range;
                    range.Text = $"Идентификатор растение {i+1}"; ///Название заголовка страницы                    
                    paragraph.set_Style("Заголовок 1");
                    range.InsertParagraphAfter();
                    Word.Paragraph tableParagraph = document.Paragraphs.Add();
                    Word.Range tableRange = tableParagraph.Range;
                    Word.Table newTable = document.Tables.Add(tableRange, group.Count() + 1, 4);
                    newTable.Borders.InsideLineStyle = newTable.Borders.OutsideLineStyle = Word.WdLineStyle.wdLineStyleSingle;
                    newTable.Range.Cells.VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                    Word.Range cellRange;          ///Заполнение оглавлений таблицы
                    cellRange = newTable.Cell(1, 1).Range;
                    cellRange.Text = "Id";
                    cellRange = newTable.Cell(1, 2).Range;
                    cellRange.Text = "Дата посадки";
                    cellRange = newTable.Cell(1, 3).Range;
                    cellRange.Text = "Возраст растение";
                    cellRange = newTable.Cell(1, 4).Range;
                    cellRange.Text = "Сорт растение";                   
                    newTable.Rows[1].Range.Bold = 1;
                    newTable.Rows[1].Range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;

                    int k = 1;
                    foreach (var data in group)     ///Заполнение данных внутри таблиц
                    {
                        cellRange = newTable.Cell(k + 1, 1).Range;
                        cellRange.Text = Convert.ToString(data.Id);
                        cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        cellRange = newTable.Cell(k + 1, 2).Range;
                        cellRange.Text = Convert.ToString(data.Date_planting);
                        cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        cellRange = newTable.Cell(k + 1, 3).Range;
                        cellRange.Text = Convert.ToString(data.Plant_age);
                        cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                        cellRange = newTable.Cell(k + 1, 4).Range;
                        cellRange.Text = Convert.ToString(data.Sort_plant);
                        cellRange.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;                     
                        k++;
                    }

                    Word.Paragraph newParagraph = document.Paragraphs.Add();
                    Word.Range countEmployeeRange = newParagraph.Range;
                    countEmployeeRange.Font.Color = Word.WdColor.wdColorBlue;
                    countEmployeeRange.InsertParagraphAfter();

                    document.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);
                    //app.Visible = true;                    
                    document.SaveAs2(@"C:\Basta\InfoPlant.docx");
                    document.SaveAs2(@"C:\Basta\InfoPlant.pdf", Word.WdExportFormat.wdExportFormatPDF);                    
                    i++;
                }
            }
            MessageBox.Show("Документ заполнен. Путь к нему: C:\\Basta\\InfoPlant.pdf", "Уведомление");
        }
    }
}
