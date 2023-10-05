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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public enum SelectedOperator
    {
        Addition,
        Substraction,
        Multiplication,
        Divison
    }

    public partial class MainWindow : Window
    {
        double lastNumber, resultValue;
        SelectedOperator selectedOperator;
        MathOperation mathOperation;

        public MainWindow()
        {
            InitializeComponent();

            clearButton.Click += ClearButton_Click;
            negativeButton.Click += NegativeButton_Click;
            percentageButton.Click += PercentageButton_Click;
            equalButton.Click += EqualButton_Click;
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            mathOperation = new MathOperation();

            if(double.TryParse(result.Content.ToString(), out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Divison:
                        resultValue = mathOperation.Divide(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        resultValue = mathOperation.Multiplication(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Addition:
                        resultValue = mathOperation.Add(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Substraction:
                        resultValue = mathOperation.Subtract(lastNumber, newNumber);
                        break;
                }

                result.Content = resultValue.ToString();
            }
        }

        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            // 50 + 5% (2.5) = 52.5 example with bug before corretion
            // 80 + 10% (8) = (88)
            double tempNumber;
            if (double.TryParse(result.Content.ToString(), out tempNumber))
            {
                tempNumber = tempNumber / 100;

                if (lastNumber != 0)
                    tempNumber *= lastNumber;

                result.Content = tempNumber.ToString();
            }
        }

        private void NegativeButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(result.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber * -1;
                result.Content = lastNumber.ToString();
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(result.Content.ToString(), out lastNumber))
            {
                result.Content = "0";
            }

            if(sender == multipleButton)
                selectedOperator = SelectedOperator.Multiplication;
            if (sender == divisionButton)
                selectedOperator = SelectedOperator.Divison;
            if (sender == plusButton)
                selectedOperator = SelectedOperator.Addition;
            if (sender == minusButton)
                selectedOperator = SelectedOperator.Substraction;
        }

        private void NumberButton_click(object sender, RoutedEventArgs e)
        {
            string resultValue = result.Content as string;

            int selectedValue = int.Parse((sender as Button).Content.ToString());

            if (sender == zeroButton) selectedValue = 0;

            if (resultValue == "0")
            {
                result.Content = $"{selectedValue}";
            }
            else
            {
                result.Content = $"{resultValue}{selectedValue}";
            }
        }

        private void pointButton_Click(object sender, RoutedEventArgs e)
        {
            if(!result.Content.ToString().Contains(","))
                result.Content = $"{result.Content},";
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            result.Content = "0";
            resultValue = 0;
            lastNumber = 0;
        }
    }

    public class MathOperation
    {
        public double Add(double value1, double value2)
        {
            return value1 + value2;
        }

        public double Subtract(double value1, double value2) 
        { 
            return value1 - value2; 
        }

        public double Multiplication(double value1, double value2)
        {
            return value1 * value2;
        }

        public double Divide(double value1, double value2)
        {
            if(value2 == 0)
            {
                MessageBox.Show("Division by 0 is not supported", "Error in operation", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
            return value1 / value2;
        }
    }

}
