using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Word = Microsoft.Office.Interop.Word;

namespace bilet13
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public int CostPred, CostSelect;
        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text == "Красная шапочка")
            {
                CostPred = 560;
            }
            else if (comboBox1.Text == "Летучий корабль")
            {
                CostPred = 650;
            }
            else if (comboBox1.Text == "Лебединое озеро")
            {
                CostPred = 550;
            }
            else if (comboBox1.Text == "Донкихот")
            {
                CostPred = 470;
            }
            else if (comboBox1.Text == "Алые паруса")
            {
                CostPred = 520;
            }
            else if (comboBox1.Text == "Щелкунчик")
            {
                CostPred = 510;
            }
            else
            {
                MessageBox.Show("Не выбрано представление");
            }

            // Выбор представление и вычисление его стоимости


            if(radioButton1.Checked == true)
            {
                CostSelect = CostPred + (CostPred / 2);
            }
            else if(radioButton2.Checked == true)
            {
                CostSelect = CostPred + (CostPred * (7 / 100));
            }
            else if(radioButton3.Checked == true)
            {
                CostSelect = CostPred + (CostPred * (20 / 100));
            }
            else
            {
                MessageBox.Show("Не выбран режим билета");
            }

            // Вычисление повышение стоимости билета в зависимости от выбора режима билета


            if(textBox1.Text.Length != 0)
                // проверка на пустое значение
            {
                if(Convert.ToInt32(textBox1.Text) > 1 & Convert.ToInt32(textBox1.Text) <= 100)
                {
                    if (Convert.ToInt32(textBox1.Text) >= 10)
                    {
                        CostSelect = CostSelect - (CostPred * (5 / 100));
                    }
                    else if (Convert.ToInt32(textBox1.Text) >= 15)
                    {
                        CostSelect = CostSelect - (CostPred * (7 / 100));
                    }
                    else if (Convert.ToInt32(textBox1.Text) >= 20)
                    {
                        CostSelect = CostSelect - (CostPred * (10 / 100));
                    }
                    else if (Convert.ToInt32(textBox1.Text) >= 30)
                    {
                        CostSelect = CostSelect - (CostPred * (25 / 100));
                    }
                    label4.Text = CostSelect.ToString();
                    MessageBox.Show($"Стоимость билета составляет {CostSelect}");
                }
                else
                {
                    MessageBox.Show("Выбрано много билетов");
                }
               
            }
            else
            {
                MessageBox.Show("Пустое значение");
            }

            // Вычисление итоговой суммы билета и вычисление скидки за количество билетов
        }
        string imglocation = "";
        private void button3_Click(object sender, EventArgs e)
        {
            if(CostSelect <= 0)
            {
                MessageBox.Show("Итоговая сумма не расчитана");
            }
            else
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "All filles(*.*)|*.*";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imglocation = dialog.FileName.ToString();
                    pictureBox1.ImageLocation = imglocation;
                }
                // Выбор картинки для афиши из папки Resources
            }
        }
        private readonly string TemplateFileName = @"C:\Users\vovv0\Desktop\bilet13\чекк.docx";
        // переменная указывающая на путь к файлу для печати данных
        private void button1_Click(object sender, EventArgs e)
        {           
            string tod = DateTime.Now.ToString();
            var tov = comboBox1.Text;
            var cost = CostSelect.ToString();
            if(CostSelect == 0)
            {
                MessageBox.Show("Сумма не указана");
                // Проверка на наличие итоговой суммы
            }
            else
            {
                try
                {
                    var wordApp = new Word.Application();
                    wordApp.Visible = false;
                    var wordDocument = wordApp.Documents.Open(TemplateFileName);
                    ReplaceWordStub("{Уникальный_номер}", textBox1.Text, wordDocument);
                    ReplaceWordStub("{дата}", tod, wordDocument);
                    ReplaceWordStub("{Товар}", tov, wordDocument);
                    ReplaceWordStub("{итог}", cost, wordDocument);
                    wordDocument.SaveAs(@"C:\чекк.docx");
                    wordApp.Visible = true;
                    // Печать параметров в файл в папке с билетом
                }
                catch
                {
                    MessageBox.Show("Ошибка");
                    // Данные передаются в файл .docx и сохраняются, но система серавно выдает ошибку
                }
            }    
        }
        private void ReplaceWordStub(string stubToReplace, string text, Word.Document wordDocument)
        {           
            var range = wordDocument.Content;
            range.Find.ClearFormatting();
            range.Find.Execute(FindText: stubToReplace, ReplaceWith: text);
            // Ввод данных для печати
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
            // Пользователь может вводить только цифры
        }
    }
}
