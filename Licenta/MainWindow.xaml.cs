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

namespace Licenta
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<string> variabile = new List<string>();
        public MainWindow()
        {
            InitializeComponent();
        }

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
                    Console.WriteLine(item);
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
                variabile.Add(txtbx.Text);

            index++;
        }

        //RETURN BUTTON
        private void Button_Click7(object sender, RoutedEventArgs e)
        {
            var index = 0;

            Label lpv = new Label();
            lpv.Content = ";";

            ComboBox cb = new ComboBox();
            foreach (var item in variabile)
            {
                cb.Items.Add(item);
                Console.WriteLine(item);
            }
            cb.Margin = new Thickness(10, 0, 0, 0);

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

            WrapPanel wp = new WrapPanel() { Name = "print" + index, Height = 30, MinWidth = 75, MaxWidth = 185, Background = new SolidColorBrush(Color.FromRgb(80, 255, 255)), Margin = new Thickness(2, 1, 1, 0) };
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
            string res = ExecuteCode(code, my_namespace.Text, my_class.Text, myfunction, false, null).ToString();
            MessageBox.Show(res, "Raspuns:");
            Console.WriteLine(ExecuteCode(code, my_namespace.Text, my_class.Text, myfunction, false, null));
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

        //Sterge blocuri
        public void DeleteBlocks()
        {
            recycle.Items.Clear();
            MessageBox.Show("Deleted");
        }
    }

    class CustomDropHandler : IDropTarget
    {
        void IDropTarget.DragOver(IDropInfo dropInfo)
        {
            dropInfo.Effects = DragDropEffects.Move;
            dropInfo.DropTargetAdorner = DropTargetAdorners.Insert;
            Console.WriteLine("Draged");
        }

        void IDropTarget.Drop(IDropInfo dropInfo)
        {
            // Not done yet...
            MainWindow mainWindow = new MainWindow();
            mainWindow.DeleteBlocks();
            MessageBox.Show("Droped");
            Console.WriteLine("Droped");
        }
    }
}
