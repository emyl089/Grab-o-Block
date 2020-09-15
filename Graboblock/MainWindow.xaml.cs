using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using System.Reflection;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using System.Text;
using GongSolutions.Wpf.DragDrop;
using FormSerialisation;
using System.Collections.ObjectModel;

namespace Licenta
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<string> variabile = new ObservableCollection<string>();
        
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            console_sect.Clear();
            ConsoleTextWriter writer = new ConsoleTextWriter(console_sect);
            Console.SetOut(writer);
        }

        //IF & WHILE
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            // Preiau obiectul care a generat evenimentul si il convertesc fortat la tipul Buton
            var elem = sender as Button;

            Label ldp = new Label();
            ldp.Content = "(";

            Label lip = new Label();
            lip.Content = ")";

            Label lpv = new Label();
            lpv.Content = ";";

            // Daca obiectul selectat este buton, atunci creez un obiect TextBlock caruia ii setez proprietatea Content cu aceeasi valoare
            // existenta pe buton
            if (elem != null)
            {

                TextBlock r = new TextBlock();
                r.Text = elem.Content.ToString();
                r.Margin = new Thickness(10, 0, 0, 0);

                //IF
                if ((r.Text == "if") || (r.Text == "while"))
                {
                    int index = 0;

                    WrapPanel wp = new WrapPanel() { Name = "if" + index, Height = 30, MinWidth = 75, MaxWidth = 365, Background = new SolidColorBrush(Color.FromRgb(255, 255, 150)), Margin = new Thickness(2, 1, 1, 0) };

                    ComboBox cb1 = new ComboBox();
                    foreach (var item in variabile)
                    {
                        cb1.Items.Add(item);
                    }
                    cb1.Name = "Primul";
                    cb1.Width = 100;
                    cb1.Margin = new Thickness(10, 0, 0, 0);

                    ComboBox cb = new ComboBox();
                    cb.Items.Add("<");
                    cb.Items.Add(">");
                    cb.Items.Add("=");
                    cb.Items.Add("!=");
                    cb.Margin = new Thickness(10, 0, 0, 0);

                    ComboBox cb2 = new ComboBox();
                    foreach (var item in variabile)
                    {
                        cb2.Items.Add(item);
                    }
                    cb2.Name = "Secundul";
                    cb2.Width = 100;
                    cb2.Margin = new Thickness(10, 0, 0, 0);

                    // Adaug in zona de lucru textBlockul
                    wp.Children.Add(r);
                    wp.Children.Add(ldp);
                    wp.Children.Add(cb1);
                    wp.Children.Add(cb);
                    wp.Children.Add(cb2);
                    wp.Children.Add(lip);
                    sp_lucru.Items.Add(wp);

                    index++;
                }

                //SCHIMBA VARIABILA
                if (r.Text == "Schimba variabila")
                {
                    int index = 0;

                    ComboBox cb = new ComboBox();
                    foreach (var item in variabile)
                    {
                        cb.Items.Add(item);
                    }
                    cb.Margin = new Thickness(10, 0, 0, 0);

                    WrapPanel wp = new WrapPanel() { Name = "change_var" + index, Height = 30, MinWidth = 75, MaxWidth = 185, Background = new SolidColorBrush(Color.FromRgb(155, 255, 150)), Margin = new Thickness(2, 1, 1, 0) };
                    Label l = new Label() { Content = "=" };
                    TextBox ba = new TextBox() { Width = 100 };

                    wp.Children.Add(cb);
                    wp.Children.Add(l);
                    wp.Children.Add(ba);
                    wp.Children.Add(lpv);
                    sp_lucru.Items.Add(wp);

                    index++;
                }
            }
        }

        //ELSE
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var elem = sender as Button;
            if (elem != null)
            {

                TextBlock r = new TextBlock();
                r.Text = elem.Content.ToString();
                r.Margin = new Thickness(10, 0, 0, 0);

                int index = 0;

                WrapPanel wp = new WrapPanel() { Name = "Else" + index, Height = 30, MinWidth = 45, MaxWidth = 120, Background = new SolidColorBrush(Color.FromRgb(255, 255, 150)), Margin = new Thickness(2, 1, 1, 0) };

                // Adaug in zona de lucru textBlockul
                wp.Children.Add(r);
                sp_lucru.Items.Add(wp);

                index++;
            }
        }

        //SETEAZA VARIABILA
        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            int index = 0;

            Label lpv = new Label() { Name = "lpv" + index };
            lpv.Content = ";";

            //Daca nu exista variabile in lista, le adauga in lista si apoi in ComboBox
            if (!variabile.Any())
            {
                MessageBox.Show("Nu ati definit variabila", "Eroare!");
            }
            //Altfel, daca exista variabile in lista, le adauga in ComboBox
            else
            {
                ComboBox cb = new ComboBox();
                foreach (var item in variabile)
                {
                    cb.Items.Add(item);
                    Console.WriteLine("Variabila: "+item);
                }
                cb.Margin = new Thickness(10, 0, 0, 0);

                WrapPanel wp = new WrapPanel() { Name = "set_var" + index, Height = 30, MinWidth = 75, MaxWidth = 165, Background = new SolidColorBrush(Color.FromRgb(155, 255, 150)), Margin = new Thickness(2, 1, 1, 0) };
                Label l = new Label() { Content = "=" };
                TextBox i = new TextBox() { Name = "setVar_" + index, Height = 23, MinWidth = 23 };

                wp.Children.Add(cb);
                wp.Children.Add(l);
                wp.Children.Add(i);
                wp.Children.Add(lpv);
                sp_lucru.Items.Add(wp);

                index++;
            }
        }

        //ADAUGA VARIABILA
        private void Button_Click6(object sender, RoutedEventArgs e)
        {
            int x = Convert.ToInt32(varnou_wp.Height);
            if (x == 41)
                varnou_wp.Height = 172;
            else
                varnou_wp.Height = 41;
        }
        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            var index = 0;

            Label lpv = new Label();
            lpv.Content = ";";

            WrapPanel wp = new WrapPanel() { Name = "add_var" + index, Height = 30, MinWidth = 75, MaxWidth = 165, Background = new SolidColorBrush(Color.FromRgb(155, 255, 150)), Margin = new Thickness(2, 1, 1, 0) };
            TextBlock txtbl = new TextBlock() { Text = varnou_tip.Text + ' ', Foreground = Brushes.Black, Margin = new Thickness(10, 0, 0, 0), HorizontalAlignment = HorizontalAlignment.Left };
            TextBox txtbx = new TextBox() { Name = "textBox_" + index, Text = varnou_nume.Text, Margin = new Thickness(2, 0, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, Height = 23, MinWidth = 23, MaxWidth = 117 };
            Border brd = new Border() { CornerRadius = new CornerRadius(2), BorderBrush = Brushes.Gray, Background = Brushes.LightGray, BorderThickness = new Thickness(2) };
            wp.Children.Add(txtbl);
            wp.Children.Add(txtbx);
            wp.Children.Add(lpv);
            sp_lucru.Items.Add(wp);

            bool alreadyExist = variabile.Contains(txtbx.Text);
            if(!alreadyExist)
            {
                variabile.Add(txtbx.Text);
                lista_variabile.ItemsSource = variabile;
            }

            index++;
        }

        //RETURN BUTTON
        private void Button_Click7(object sender, RoutedEventArgs e)
        {
            var index = 0;
            int zero = 0;

            Label lpv = new Label();
            lpv.Content = ";";

            ComboBox cb = new ComboBox();
            foreach (var item in variabile)
            {
                cb.Items.Add(item);
                Console.WriteLine("Variabila: "+item);
            }
            cb.Margin = new Thickness(10, 0, 0, 0);
            cb.Items.Add(zero);

            WrapPanel wp = new WrapPanel() { Name = "add_var" + index, Height = 30, MinWidth = 75, MaxWidth = 165, Background = new SolidColorBrush(Color.FromRgb(255, 255, 150)), Margin = new Thickness(2, 1, 1, 0) };
            TextBlock txtbl = new TextBlock() { Text = "return ", Foreground = Brushes.Black, Margin = new Thickness(10, 0, 0, 0), HorizontalAlignment = HorizontalAlignment.Left };
            Border brd = new Border() { CornerRadius = new CornerRadius(2), BorderBrush = Brushes.Gray, Background = Brushes.LightGray, BorderThickness = new Thickness(2) };
            wp.Children.Add(txtbl);
            wp.Children.Add(cb);
            wp.Children.Add(lpv);
            sp_lucru.Items.Add(wp);

            index++;
        }

        //TEXT PRINT
        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            var index = 0;

            Label ldp = new Label();
            ldp.Content = "(";
            Label lip = new Label();
            lip.Content = ")";
            Label lpv = new Label();
            lpv.Content = ";";

            WrapPanel wp = new WrapPanel() { Name = "print" + index, Height = 30, MinWidth = 75, MaxWidth = 285, Background = new SolidColorBrush(Color.FromRgb(80, 255, 255)), Margin = new Thickness(2, 1, 1, 0) };
            TextBlock txtbl = new TextBlock() { Text = "Console.WriteLine ", Foreground = Brushes.Black, Margin = new Thickness(10, 0, 0, 0), HorizontalAlignment = HorizontalAlignment.Left };
            TextBox txtbx = new TextBox() { Name = "textBoxP_" + index, Margin = new Thickness(2, 0, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, Height = 23, MinWidth = 23, MaxWidth = 117 };

            wp.Children.Add(txtbl);
            wp.Children.Add(ldp);
            wp.Children.Add(txtbx);
            wp.Children.Add(lip);
            wp.Children.Add(lpv);
            sp_lucru.Items.Add(wp);

            index++;
        }

        //ACOLADE
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var elem = sender as Button;
            if (elem != null)
            {

                TextBlock r = new TextBlock();
                r.Text = elem.Content.ToString();
                r.Margin = new Thickness(10, 0, 0, 0);

                if ((r.Text == "{") || (r.Text == "}"))
                {
                    int index = 0;

                    WrapPanel wp = new WrapPanel() { Name = "Acolada" + index, Height = 25, MinWidth = 20, MaxWidth = 50, Background = new SolidColorBrush(Color.FromRgb(150, 100, 50)), Margin = new Thickness(2, 1, 1, 0) };

                    // Adaug in zona de lucru textBlockul
                    wp.Children.Add(r);
                    sp_lucru.Items.Add(wp);

                    index++;
                }
            }
        }

        //COMPILEAZA
        private void Button_Click5(object sender, RoutedEventArgs e)
        {
            code_sect.Text = "";
            TextWriter tw = new StreamWriter("D://logfile.txt");

            code_sect.AppendText("using System; \n");
            code_sect.AppendText("namespace " + my_namespace.Text + "{\n");
            code_sect.AppendText("public class " + my_class.Text + "{\n");
            code_sect.AppendText("public float " + my_function.Text + "{\n");

            foreach (object wrap in sp_lucru.Items)
            {
                WrapPanel wrp = wrap as WrapPanel;
                foreach (var elem in wrp.Children)
                {
                    if (elem is TextBlock)
                    {
                        TextBlock tb = elem as TextBlock;
                        code_sect.AppendText(tb.Text);
                        //tw.Write(tb.Text);
                    }
                    else if (elem is TextBox)
                    {
                        TextBox tb = elem as TextBox;
                        code_sect.AppendText(tb.Text);
                        //tw.Write(tb.Text);
                    }
                    else if (elem is Label)
                    {
                        Label tb = elem as Label;
                        code_sect.AppendText(tb.Content.ToString());
                        //tw.Write(tb.Content.ToString());
                    }
                    else if (elem is ComboBox)
                    {
                        ComboBox tb = elem as ComboBox;
                        code_sect.AppendText(tb.SelectedItem.ToString());
                        //tw.Write(tb.SelectedItem.ToString());
                    }
                }
                code_sect.AppendText(Environment.NewLine);
            }
            code_sect.AppendText("}\n");
            code_sect.AppendText("}\n");
            code_sect.AppendText("}\n");
            code_sect.Text = code_sect.Text.Replace("var", "int");
            tw.Write(code_sect.Text);
            tw.Close();
        }

        //Menu button 'Aplication restart'
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        //Execute code button
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string code; //Codul text
            code = File.ReadAllText(@"D:\\logfile.txt"); //Codul preluat din fisierul text
            code_sect.Text = code.ToString(); //Afisare cod in TextBox
            string myfunction = my_function.Text.Remove(my_function.Text.Length - 2);
            string rez = ExecuteCode(code, my_namespace.Text, my_class.Text, myfunction, false, null).ToString();
            Console.WriteLine("===========================");
            System.Windows.Forms.DialogResult res = System.Windows.Forms.MessageBox.Show("Programul a returnat: "+rez, "Confirmare", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Information);
            if (res == System.Windows.Forms.DialogResult.OK)
            {
                //Some task…  
            }
            if (res == System.Windows.Forms.DialogResult.Cancel)
            {
                //Some task…  
            }
            Console.WriteLine("Programul a returnat: " + ExecuteCode(code, my_namespace.Text, my_class.Text, myfunction, false, null));
        }
        private Assembly BuildAssembly(string code)
        {
            CSharpCodeProvider provider = new CSharpCodeProvider();
            CompilerParameters compilerparams = new CompilerParameters();
            compilerparams.GenerateExecutable = false;
            compilerparams.GenerateInMemory = true;
            //ICodeCompiler compiler = provider.CreateCompiler();
            CompilerResults results = provider.CompileAssemblyFromSource(compilerparams, code);
            if (results.Errors.HasErrors)
            {
                StringBuilder errors = new StringBuilder("Compiler Errors :\r\n");
                foreach (CompilerError error in results.Errors)
                {
                    errors.AppendFormat("Line {0},{1}\t: {2}\n",
                           error.Line, error.Column, error.ErrorText);
                }
                System.Windows.Forms.DialogResult eror = System.Windows.Forms.MessageBox.Show(errors.ToString(), "Eroare de compilare", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Information);
                if (eror == System.Windows.Forms.DialogResult.OK)
                {
                    System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
                    Application.Current.Shutdown();
                }
                if (eror == System.Windows.Forms.DialogResult.Cancel)
                {
                    //Some task…  
                }
                throw new Exception(errors.ToString());
            }
            else
            {
                return results.CompiledAssembly;
            }
        }
        public object ExecuteCode(string code, string namespacename, string classname, string functionname, bool isstatic, params object[] args)
        {
            object returnval = null;
            Assembly asm = BuildAssembly(code);
            object instance = null;
            Type type = null;
            if (isstatic)
            {
                type = asm.GetType(namespacename + "." + classname);
            }
            else
            {
                instance = asm.CreateInstance(namespacename + "." + classname);
                type = instance.GetType();
            }
            MethodInfo method = type.GetMethod(functionname);
            returnval = method.Invoke(instance, args);
            return returnval;
        }

        //Butonul 'X' cosul de reciclare
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            recycle.Items.Clear();
        }

        //Butonul 'Help' din Meniu
        private void menuItem3_Click(object sender, System.EventArgs e)
        {
            Form1 helpForm = new Form1();
            helpForm.ShowDialog();
        }

        //Butonul 'Test' din Meniu
        private void menuItem2_Click(object sender, System.EventArgs e)
        {
            Form3 testForm = new Form3();
            testForm.ShowDialog();
        }

        //WHILE
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            var index = 0;

            Label ldp = new Label();
            ldp.Content = "(";
            Label lip = new Label();
            lip.Content = ")";
            Label lpv = new Label();
            lpv.Content = ";";

            ComboBox cb1 = new ComboBox();
            foreach (var item in variabile)
            {
                cb1.Items.Add(item);
            }
            cb1.Name = "Primul";
            cb1.Width = 100;
            cb1.Margin = new Thickness(10, 0, 0, 0);

            ComboBox cb = new ComboBox();
            cb.Items.Add("<");
            cb.Items.Add(">");
            cb.Items.Add("=");
            cb.Items.Add("!=");
            cb.Margin = new Thickness(10, 0, 0, 0);

            ComboBox cb2 = new ComboBox();
            foreach (var item in variabile)
            {
                cb2.Items.Add(item);
            }
            cb2.Name = "Secundul";
            cb2.Width = 100;
            cb2.Margin = new Thickness(10, 0, 0, 0);

            WrapPanel wp = new WrapPanel() { Name = "while" + index, Height = 30, MinWidth = 75, MaxWidth = 365, Background = new SolidColorBrush(Color.FromRgb(160, 80, 200)), Margin = new Thickness(2, 1, 1, 0) };
            TextBlock txtbl1 = new TextBlock() { Text = "while ", Foreground = Brushes.Black, Margin = new Thickness(10, 0, 0, 0), HorizontalAlignment = HorizontalAlignment.Left };

            wp.Children.Add(txtbl1);
            wp.Children.Add(ldp);
            wp.Children.Add(cb1);
            wp.Children.Add(cb);
            wp.Children.Add(cb2);
            wp.Children.Add(lip);
            sp_lucru.Items.Add(wp);

            index++;
        }

        //FOR
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            int x = Convert.ToInt32(for_wp.Height);
            if (x == 41)
                for_wp.Height = 126;
            else
                for_wp.Height = 41;
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            var index = 0;

            Label ldp = new Label();
            ldp.Content = "(";
            Label lip = new Label();
            lip.Content = ")";
            Label lpv1 = new Label();
            lpv1.Content = ";";
            Label lpv2 = new Label();
            lpv2.Content = ";";

            ComboBox cb1 = new ComboBox();
            cb1.Items.Add("<");
            cb1.Items.Add(">");
            cb1.Items.Add("=");
            cb1.Items.Add("!=");
            cb1.Margin = new Thickness(10, 0, 0, 0);

            ComboBox cb2 = new ComboBox();
            cb2.Items.Add("<");
            cb2.Items.Add(">");
            cb2.Items.Add("=");
            cb2.Items.Add("!=");
            cb2.Margin = new Thickness(10, 0, 0, 0);

            ComboBox cb3 = new ComboBox();
            cb3.Items.Add("++");
            cb3.Items.Add("--");
            cb3.Margin = new Thickness(10, 0, 0, 0);

            WrapPanel wp = new WrapPanel() { Name = "for" + index, Height = 30, MinWidth = 75, MaxWidth = 365, Background = new SolidColorBrush(Color.FromRgb(160, 80, 200)), Margin = new Thickness(2, 1, 1, 0) };
            TextBlock txtbl1 = new TextBlock() { Text = "for ", Foreground = Brushes.Black, Margin = new Thickness(10, 0, 0, 0), HorizontalAlignment = HorizontalAlignment.Left };
            Label lbl1 = new Label() { Name = "lblFor_1", Content = varnoua_nume.Text };
            Label lbl2 = new Label() { Name = "lblFor_2", Content = varnoua_nume.Text };
            Label lbl3 = new Label() { Name = "lblFor_3", Content = varnoua_nume.Text };
            Label lbl4 = new Label() { Name = "lblFor_4", Content = "int " };
            TextBox txtbx1 = new TextBox() { Name = "textBoxFor_1", Margin = new Thickness(2, 0, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, Height = 23, MinWidth = 23, MaxWidth = 117 };
            TextBox txtbx2 = new TextBox() { Name = "textBoxFor_2", Margin = new Thickness(2, 0, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, Height = 23, MinWidth = 23, MaxWidth = 117 };

            wp.Children.Add(txtbl1);
            wp.Children.Add(ldp);
            wp.Children.Add(lbl4);
            wp.Children.Add(lbl1);
            wp.Children.Add(cb1);
            wp.Children.Add(txtbx1);
            wp.Children.Add(lpv1);
            wp.Children.Add(lbl2);
            wp.Children.Add(cb2);
            wp.Children.Add(txtbx2);
            wp.Children.Add(lpv2);
            wp.Children.Add(lbl3);
            wp.Children.Add(cb3);
            wp.Children.Add(lip);
            sp_lucru.Items.Add(wp);

            index++;
        }

        //BASIC MATH
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            int x = Convert.ToInt32(math_wp.Height);
            if (x == 41)
                math_wp.Height = 200;
            else
                math_wp.Height = 41;
        }
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            var index = 0;

            Label ldp = new Label();
            ldp.Content = "(";
            Label lip = new Label();
            lip.Content = ")";
            Label lpv = new Label();
            lpv.Content = ";";

            ComboBox cb1 = new ComboBox();
            foreach (var item in variabile)
            {
                cb1.Items.Add(item);
            }
            cb1.Name = "cbMath1_" + index;
            cb1.MinWidth = 23;
            cb1.MaxWidth = 100;
            cb1.Margin = new Thickness(10, 0, 0, 0);

            ComboBox cb2 = new ComboBox();
            cb2.Items.Add("+");
            cb2.Items.Add("-");
            cb2.Items.Add("*");
            cb2.Items.Add("/");
            cb2.Margin = new Thickness(10, 0, 0, 0);

            ComboBox cb3 = new ComboBox();
            foreach (var item in variabile)
            {
                cb3.Items.Add(item);
            }
            cb3.Name = "cbMath2_" + index;
            cb3.MinWidth = 23;
            cb3.MaxWidth = 100;
            cb3.Margin = new Thickness(10, 0, 0, 0);

            WrapPanel wp = new WrapPanel() { Name = "math" + index, Height = 30, MinWidth = 75, MaxWidth = 365, Background = new SolidColorBrush(Color.FromRgb(255, 190, 50)), Margin = new Thickness(2, 1, 1, 0) };
            TextBlock txtbl1 = new TextBlock() { Text = varMath_tip.Text + ' ', Foreground = Brushes.Black, Margin = new Thickness(10, 0, 0, 0), HorizontalAlignment = HorizontalAlignment.Left };
            Label lbl1 = new Label() { Name = "lblMath_1", Content = "=" };
            TextBox txtbx1 = new TextBox() { Name = "textBoxMath_1", Text = varMath_nume.Text, Margin = new Thickness(2, 0, 0, 0), HorizontalAlignment = HorizontalAlignment.Left, Height = 23, MinWidth = 23, MaxWidth = 117 };

            if (varMath_check.IsChecked == false)
                txtbl1.Text = "";

            wp.Children.Add(txtbl1);
            wp.Children.Add(txtbx1);
            wp.Children.Add(lbl1);
            wp.Children.Add(cb1);
            wp.Children.Add(cb2);
            wp.Children.Add(cb3);
            wp.Children.Add(lpv);
            sp_lucru.Items.Add(wp);

            bool alreadyExist = variabile.Contains(txtbx1.Text);
            if (!alreadyExist)
            {
                variabile.Add(txtbx1.Text);
                lista_variabile.ItemsSource = variabile;
            }

            index++;
        }

        //Increment
        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            var index = 0;

            Label ldp = new Label();
            ldp.Content = "(";
            Label lip = new Label();
            lip.Content = ")";
            Label lpv = new Label();
            lpv.Content = ";";

            ComboBox cb1 = new ComboBox();
            foreach (var item in variabile)
            {
                cb1.Items.Add(item);
            }
            cb1.Name = "cbInc1_" + index;
            cb1.MinWidth = 23;
            cb1.MaxWidth = 100;
            cb1.Margin = new Thickness(10, 0, 0, 0);

            ComboBox cb2 = new ComboBox();
            cb2.Items.Add("++");
            cb2.Items.Add("--");
            cb2.Margin = new Thickness(10, 0, 0, 0);

            WrapPanel wp = new WrapPanel() { Name = "increment" + index, Height = 30, MinWidth = 75, MaxWidth = 365, Background = new SolidColorBrush(Color.FromRgb(255, 190, 50)), Margin = new Thickness(2, 1, 1, 0) };
            //TextBlock txtbl1 = new TextBlock() { Text = ' ', Foreground = Brushes.Black, Margin = new Thickness(10, 0, 0, 0), HorizontalAlignment = HorizontalAlignment.Left };

            //wp.Children.Add(txtbl1);
            wp.Children.Add(cb1);
            wp.Children.Add(cb2);
            wp.Children.Add(lpv);
            sp_lucru.Items.Add(wp);

            index++;
        }
        /*
        //Save file
        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {
            FormSerialisor.Serialise(this, System.Windows.Forms.Application.StartupPath + @"D:\\save.xml");

            MessageBox.Show("Program salvat!");
        }

        //Open file
        private void btn_Open_Click(object sender, RoutedEventArgs e)
        {
            FormSerialisor.Deserialise(this, System.Windows.Forms.Application.StartupPath + @"D:\\save.xml");

            MessageBox.Show("Program deschis!");
        }
        */
    }

    //Consola aplicatiei
    public class ConsoleTextWriter : TextWriter
    {
        // The control where we will write text.
        private Control MyControl;
        public ConsoleTextWriter(Control control)
        {
            MyControl = control;
        }

        public override void Write(char value)
        {
            ((TextBox)MyControl).Text += value;
        }

        public override void Write(string value)
        {
            ((TextBox)MyControl).Text += value;
        }

        public override Encoding Encoding
        {
            get { return Encoding.Unicode; }
        }
    }
}
