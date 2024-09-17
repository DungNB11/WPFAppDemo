using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFAppDemo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void txta_GotFocus(object sender, RoutedEventArgs e)
        {
            txta.Background = Brushes.LightPink;
        }

        private void txta_LostFocus(object sender, RoutedEventArgs e)
        {
            txta.Background = Brushes.White;
        }

        private void txtb_LostFocus(object sender, RoutedEventArgs e)
        {
            txtb.Background= Brushes.White;
        }

        private void txtb_GotFocus(object sender, RoutedEventArgs e)
        {
            txtb.Background = Brushes.LightPink;
        }

        private void btnExe_Click(object sender, RoutedEventArgs e)
        {
            handleCaculator();
        }

        private void cboOption_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            handleCaculator();
        }

        private void txta_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter) {
                handleCaculator();
            }
        }

        private void txtb_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter) {
                handleCaculator();
            }
        }

        private void handleCaculator()
        {
            int option = cboOption.SelectedIndex;
            try
            {
                int a = Int32.Parse(txta.Text);
                int b = Int32.Parse(txtb.Text);
                float result = 0;
                switch (option)
                {
                    case 0:
                        result = a + b;
                        break;
                    case 1:
                        result = a - b;
                        break;
                    case 2:
                        result = a * b;
                        break;
                    case 3:
                        if(b == 0)
                        {
                            MessageBox.Show("B must be different from 0!", "Notification");
                            txtResult.Clear();
                            break;
                        }
                        result = (float)a / b;
                        break;
                    default:
                        MessageBox.Show("Full value required!", "Notification");
                        txtResult.Clear();
                        break;
                }

                txtResult.Text = result.ToString();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error");
            }
        }
    }
}