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

namespace RomanNumbers2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class RomanIntConverter : Window, RomanIntView
    {
        RomanIntPresenter presenter = new RomanIntPresenter();

        public RomanIntConverter()
        {
            InitializeComponent();
            presenter.addView(this);
        }

        public void ClearResult()
        {
            txtResultBox.Text = "";
        }

        public void DisplayResult(string result)
        {
            txtResultBox.Text = result;
        }

        public void HandleErrorMessage(Exception ex)
        {
            if(ex is BLL.ConversionException)
                MessageBox.Show("Please enter the number from allowed range.", "Invalid input argument", MessageBoxButton.OK, MessageBoxImage.Information);
            else
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtInputNumber.Text == string.Empty) return;
            presenter.Convert(txtInputNumber.Text);
        }

        private void txtInputNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            presenter.ClearResult();
        }
    }
}
