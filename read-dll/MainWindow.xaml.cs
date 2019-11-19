using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

namespace read_dll
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // dll 路径
        private string dllUrl = "";

        // dll 数据类型
        Assembly assembly = null;

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new Microsoft.Win32.OpenFileDialog()
            {
                Filter = "Text documents (.dll)|*.dll|All files (*.*)|*.*"
            };
            var result = openFileDialog.ShowDialog();
            if (result == true)
            {
                dllUrl = openFileDialog.FileName;
                text1.Text= openFileDialog.FileName;


                //加载程序集(dll文件地址)，使用Assembly类   
                assembly = Assembly.LoadFile(dllUrl);

                //获取类型，参数（名称空间+类）   
                Type[] types = assembly.GetTypes();


                this.listbox1.Items.Clear();

                foreach (Type t in types)
                {
                    this.listbox1.Items.Add(t.FullName);
                }
            }


          //  MessageBox.Show(assembly.GetType("JavaClass").Name);

            ////创建该对象的实例，object类型，参数（名称空间+类）   
            //object instance = assembly.CreateInstance("assembly_name.assembly_class");

            ////设置Show_Str方法中的参数类型，Type[]类型；如有多个参数可以追加多个   
            //Type[] params_type = new Type[1];
            //params_type[0] = Type.GetType("System.String");
            ////设置Show_Str方法中的参数值；如有多个参数可以追加多个   
            //Object[] params_obj = new Object[1];
            //params_obj[0] = "jiaopeng";

            ////执行Show_Str方法   
            //object value = type.GetMethod("Show_Str", params_type).Invoke(instance, params_obj);



            //else
            //{
            //    return null;
            //}
        }

        private void Listbox1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void Listbox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (assembly == null) return;
            string item = this.listbox1.SelectedItem.ToString().Trim();
            // MessageBox.Show(item);
            Type t = assembly.GetType(item);
            if (t != null)
            {
                this.groupbox.Content = "属性 ：" + "\r\n";
               PropertyInfo[] properties = t.GetProperties();
                
                foreach(PropertyInfo info in properties)
                {
                    this.groupbox.Content += info.PropertyType.Name + "\t" + info.Name + ";\r\n";
                }

                FieldInfo[] fieldInfos = t.GetFields();

                foreach (FieldInfo info in fieldInfos)
                {
                    this.groupbox.Content += info.FieldType.Name + "\t" + info.Name + ";\r\n";
                }

                this.groupbox.Content +="\r\n";
                this.groupbox.Content += "方法 ：" + "\r\n";
                MethodInfo[] methodInfos = t.GetMethods();

                foreach (MethodInfo info in methodInfos)
                {
                    this.groupbox.Content += info.ReturnType.Name + "\t " + info.Name + ";\r\n";
                }

               


            }



        }
    }
}
