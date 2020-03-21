using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculator
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        private string[] numbers = new string[2];
        private string operation;

        public MainPage()
        {
            InitializeComponent();
        }   

        public void ButtonClick(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if ("0123456789.".Contains(button.Text))
            {
                AddDigit(button.Text);
            }
            else if ("/X-+".Contains(button.Text))
            {
                AddOperator(button.Text);
            }
            else if ("=" == button.Text)
            {
                Calculate();
            }
            else if ("C" == button.Text)
            {
                Clear();
            }
            else if ("%" == button.Text)
            {
                Percent();
            }
            else
            {
                Delete();
            }
        }

        private void AddDigit(string value)
        {
            int index = operation == null ? 0 : 1;

            if (value == "." && numbers[index].Contains("."))
            {
                return;
            }

            numbers[index] += value;

            UpdateHead();
        }

        private void AddOperator(string value)
        {
            if (numbers[1] != null)
            {
                Calculate(value);
                return;
            }

            operation = value;

            UpdateHead();
        }

        private void UpdateHead()
        {
            this.head.Text = $"{numbers[0]} {operation} {numbers[1]}";
        }

        private void Delete()
        {
            if (numbers[0] != null && operation == null && numbers[1] == null)
            {
                if (numbers[0].Length == 1)
                {
                    numbers[0] = null;
                } 
                else
                {
                    numbers[0] = numbers[0].Remove(numbers[0].Length - 1);
                }
            }
            else if (numbers[0] != null && operation != null && numbers[1] == null)
            {
                operation = null;
            }
            else if (numbers[0] != null && operation != null && numbers[1] != null)
            {
                if (numbers[1].Length == 1)
                {
                    numbers[1] = null;
                }
                else
                {
                    numbers[1] = numbers[1].Remove(numbers[1].Length - 1);
                }
            }

            UpdateHead();
        }

        private void Clear()
        {
            numbers[0] = numbers[1] = null;
            operation = null;
            UpdateHead();
        }

        private void Percent()
        {
            int index = operation == null ? 0 : 1;
            double? result, p = 100;
            double? value = numbers[index] == null ? null : (double?)double.Parse(numbers[index]);

            result = value / p;
            numbers[index] = result.ToString();
            UpdateHead();
        }   

        private void Calculate(string newOperator = null)
        {
            double? result = null;
            double? value1 = numbers[0] == null ? null : (double?)double.Parse(numbers[0]);
            double? value2 = numbers[1] == null ? null : (double?)double.Parse(numbers[1]);

            switch (operation)
            {
                case "/":
                    result = value1 / value2;
                    break;
                case "X":
                    result = value1 * value2;
                    break;
                case "-":
                    result = value1 - value2;
                    break;
                case "+":
                    result = value1 + value2;
                    break;
            }

            if (result != null)
            {
                numbers[0] = result.ToString();
                operation = newOperator;
                numbers[1] = null;
                UpdateHead();
            }
        }
    }
}
