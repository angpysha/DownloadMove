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
using SettingsLib;
using System.IO;
using System.Collections.Specialized;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Settings
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int LB_ADDSTRING = 0x180;
        private const int LB_INSERTSTRING = 0x181;
        private const int LB_DELETESTRING = 0x182;
        private const int LB_RESETCONTENT = 0x184;
        public MainWindow()
        {
            InitializeComponent();
            ((INotifyCollectionChanged)listBox.Items).CollectionChanged += new NotifyCollectionChangedEventHandler(chang);
        }

        private void chang(object sender, NotifyCollectionChangedEventArgs e)
        {
            comboBox.Items.Clear();
            foreach (var val in listBox.Items.Cast<string>().ToList())
            {
                comboBox.Items.Add(val);
            }
        }

        List<SettingsLib.Settings.Sets> sets = new List<SettingsLib.Settings.Sets>();
        SettingsLib.Settings.Setts setts = new SettingsLib.Settings.Setts();
        private void kd(object sender, KeyEventArgs e)
        {
            // MessageBox.Show(((int)e.Key).ToString());
            if ((int)e.Key == 32)
            {
                listBox.Items.Remove(listBox.SelectedItem);
                // sets.Remove(sets.Where(s => s.category == comboBox.SelectedItem.ToString()).First());
                
                savef();
            }
        }
        List<string> s = BinaryTools.FromBinary();
        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            if (File.Exists(Constants.categotries))
            {
                foreach (var val in s)
                {
                    listBox.Items.Add(val);
                  //  comboBox.Items.Add(val);
                }
            }
            loaddata();
            comboBox.SelectedIndex = 0;
            textBox1_Copy.SetValue(ScrollViewer.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Disabled);
            textBox1_Copy.TextWrapping = TextWrapping.NoWrap;
            textBox1.TextWrapping = TextWrapping.NoWrap;
            textBox.TextWrapping = TextWrapping.NoWrap;
                }

        private void loaddata()
        {
            SettingsLib.Settings.category cato = new SettingsLib.Settings.category();
            if (File.Exists(Constants.savefile))
            {
                SettingsLib.Settings.Setts settss = cato.deserialize();
                sets = settss.sets;
              /*  foreach (var el in sets)
                {
                    
                }*/
            }
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            listBox.Items.Add(textBox.Text);
            savef();
        }
        private void savef()
        {
            List<string> categ = listBox.Items.Cast<string>().ToList();
            BinaryTools.ToBinary(categ);
        }

        private void size_changed(object sender, SizeChangedEventArgs e)
        {
            comboBox.Items.Clear();
            foreach (var val in listBox.Items.Cast<string>().ToList())
            {
                comboBox.Items.Add(val);
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            if (!listBox1.Items.Contains(textBox1.Text))
            listBox1.Items.Add(textBox1.Text);
            if (!sets.Any(s=>s.category.ToString().Equals(comboBox.SelectedItem.ToString())))
                {
                SettingsLib.Settings.Sets setss= new SettingsLib.Settings.Sets();
                setss.category = comboBox.Text;
                setss.extentions = String.Join(",", listBox1.Items.Cast<string>().ToList().ToArray());
                setss.path = textBox1_Copy.Text;
                sets.Add(setss);
                //MessageBox.Show(sets.First().extentions.ToString());
            } else
            {
                sets.Where(s => s.category == comboBox.SelectedItem.ToString()).ToList().ForEach(s => s.extentions = String.Join(",", listBox1.Items.Cast<string>().ToList().ToArray()));
                sets.Where(s => s.category == comboBox.SelectedItem.ToString()).ToList().ForEach(s => s.path = textBox1_Copy.Text);
               // MessageBox.Show(sets.First().extentions.ToString());
            }          
        }

        private void button1_Copy1_Click(object sender, RoutedEventArgs e)
        {
           if (sets.Any(s=>s.category.ToString().Equals(comboBox.SelectedItem.ToString())))
                    sets.Where(s => s.category == comboBox.SelectedItem.ToString()).ToList().ForEach(s => s.path = textBox1_Copy.Text);
            SettingsLib.Settings.category cato = new SettingsLib.Settings.category();
            setts.sets = sets;
            //setts.inputfolder = @"D:\do";
            cato.serialize(setts);
                  //  (sets.Where(s=>s.category == comboBox.Text))
                
        }

        private void changed(object sender, SelectionChangedEventArgs e)
        {
          //  MessageBox.Show(comboBox.SelectedItem.ToString());
            listBox1.Items.Clear();
            // sets.Where(s => s.category == comboBox.Text).ToList().ForEach(s=)
            try {
                if (sets.Any(s => s.category.ToString().Equals(comboBox.SelectedItem.ToString())))
                {
                    // MessageBox.Show("OK");
                    string ext = sets.Where(s => s.category == comboBox.SelectedItem.ToString()).First().extentions;
                    List<string> ls = ext.Split(',').ToList();
                    foreach (var el in ls)
                    {
                        listBox1.Items.Add(el);
                    }
                    string _path = sets.Where(s => s.category == comboBox.SelectedItem.ToString()).First().path;
                    textBox1_Copy.Text = _path;
                    /* textBox1.Clear();
                     textBox1_Copy.Clear();*/
                }
            } catch
            {
                return;
            }
            

        }

        private void button1_Copy_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog st = new CommonOpenFileDialog();
            st.IsFolderPicker = true;
            CommonFileDialogResult res = st.ShowDialog();
            textBox1_Copy.Text = st.FileName;
            //var fold = new System.Windows.Forms.FolderBrowserDialog();
           // fold.ShowDialog();
           // textBox1_Copy.Text = fold.SelectedPath;
        }

        private void txt_chang(object sender, TextChangedEventArgs e)
        {
            sets.Where(s => s.category == comboBox.SelectedItem.ToString()).ToList().ForEach(s => s.path = textBox1_Copy.Text);
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog op = new CommonOpenFileDialog();
            op.IsFolderPicker = true;
            op.ShowDialog();
            textBox2.Text = op.FileName;
            setts.inputfolder = textBox2.Text;
        }
        #region Профіксити через фунцію чи заняти файл do_while
        private void button3_Click(object sender, RoutedEventArgs e)
        {
            try {
                SettingsLib.Settings.category cato = new SettingsLib.Settings.category();
                setts.sets = sets;
                cato.serialize(setts);
            }
            catch (Exception ex)
            {
               // MessageBox.Show(ex.Message.ToString());
            }
        }
        #endregion
        //  protected override void WndProc(ref Message m )
    }

}
