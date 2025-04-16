using System.Windows;
using System.Windows.Controls;

namespace WpfAppForTest1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        string name = txtName.Text;
        var country = ((ComboBoxItem)cmbCountry.SelectedItem).Tag;
        string vipStatus = cbIsVIP.IsChecked == true ? "VIP" : "Not VIP";
        txtResult.Text = $@"Name: {name}
            Country: {country}
            {vipStatus}";
    }
}