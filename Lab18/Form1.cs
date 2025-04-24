using System;
using System.Linq;
using System.Windows.Forms;
using System.Globalization;

namespace Lab18
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            txtConsole.Clear();

            // 1. Зчитування масиву
            string[] parts = txtArray.Text.Split(new[] { ' ', ',', ';' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
            {
                txtConsole.Text = "Введіть дійсні числа через пробіл.";
                return;
            }

            double[] array;
            try
            {
                array = parts.Select(s => double.Parse(s, CultureInfo.InvariantCulture)).ToArray();
            }
            catch
            {
                txtConsole.Text = "Невірний формат числа. Використовуйте крапку як роздільник дробової частини (напр. 3.14).";
                return;
            }

            // 2. Номер максимального елемента
            int maxIndex = Array.IndexOf(array, array.Max());
            txtConsole.AppendText($"Номер (індекс) максимального елемента: {maxIndex}\r\n");

            // 3. Добуток між першим і другим нулем
            int firstZero = Array.IndexOf(array, 0);
            int secondZero = Array.FindIndex(array, firstZero + 1, x => x == 0);

            if (firstZero >= 0 && secondZero > firstZero + 1)
            {
                double product = 1;
                for (int i = firstZero + 1; i < secondZero; i++)
                    product *= array[i];

                txtConsole.AppendText($"Добуток між першим і другим нулем: {product}\r\n");
            }
            else if (firstZero >= 0 && secondZero == firstZero + 1)
            {
                txtConsole.AppendText("Між нулями немає елементів. Добуток = 1\r\n");
            }
            else
            {
                txtConsole.AppendText("Не знайдено двох нулів для обчислення добутку.\r\n");
            }

            // 4. Перетворення масиву
            var oddPos = array.Where((x, i) => i % 2 == 0).ToArray();  // індекси 0,2,4... (непарні позиції)
            var evenPos = array.Where((x, i) => i % 2 != 0).ToArray(); // індекси 1,3,5... (парні позиції)

            double[] transformed = oddPos.Concat(evenPos).ToArray();

            txtConsole.AppendText("Перетворений масив: " + string.Join(" ", transformed.Select(x => x.ToString(CultureInfo.InvariantCulture))) + "\r\n");
        }
    }
}

    

