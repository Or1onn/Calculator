using Calculate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Runtime.InteropServices;
using System.Windows.Interop;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.CodeAnalysis;

namespace Calculate
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new WindowBlureffect(this, AccentState.ACCENT_ENABLE_BLURBEHIND) { BlurOpacity = 100 };
            EventManager.RegisterClassHandler(typeof(Button), Button.ClickEvent, new RoutedEventHandler(Button_Click));
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            this.DragMove();
        }
        public static double Evaluate(string expression)
        {
            System.Data.DataTable table = new System.Data.DataTable();
            table.Columns.Add("expression", string.Empty.GetType(), expression);
            System.Data.DataRow row = table.NewRow();
            table.Rows.Add(row);
            return double.Parse((string)row["expression"]);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var senderBtn = sender as Button;
            if (senderBtn != null)
            {
                textBox.Text += senderBtn!.Content.ToString();
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            textBox.Text = Evaluate(textBox.Text.Trim('=').Replace('÷', '/').Replace('×', '*')).ToString();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1);
        }
    }
}