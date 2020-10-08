using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace StartupPopup
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, string> configDict = new Dictionary<string, string>();
        List<CheckBox> chkboxList = new List<CheckBox>();

        public MainWindow()
        {
            InitializeComponent();

            List<string> todoList = new List<string>();
            var readTask = File.ReadAllLinesAsync("config.ini");
            var dict = readTask.Result;
            for (int i = 0; i < dict.Length; i++)
            {
                var line = dict[i];
                if (line != null)
                    line = line.Trim();

                var pair = line.Split('=');
                if (pair != null && pair.Length != 2)
                    continue;

                var k = pair[0];
                var v = pair[1];
                if (k.Equals("todo"))
                {
                    todoList.Add(v);
                    continue;
                }

                configDict.Add(k, v);
            }

            //listBox.FontWeight = FontWeights.UltraBlack;

            for (int i = 0; i < todoList.Count; i++)
            {
                CheckBox chkbox = new CheckBox
                {
                    Content = todoList[i],                    
                    FontSize = 20,
                    FontWeight = FontWeights.UltraBold,
                    VerticalContentAlignment = VerticalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    HorizontalAlignment = HorizontalAlignment.Center                    
                };
                chkbox.Checked += Chkbox_Checked;
                chkboxList.Add(chkbox);
            }

            listBox.Items.Clear();
            listBox.ItemsSource = chkboxList;




        }

        private void Chkbox_Checked(object sender, RoutedEventArgs e)
        {
            foreach(var chk in chkboxList)
            {
                if (!(bool)chk.IsChecked)
                {
                    return;
                }
            }

            window.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //this.Hide();
            //Thread.Sleep(60 * 1000);
            //this.Show();


            Debug.Print("lines :" + configDict.Count);
        }
    }
}
