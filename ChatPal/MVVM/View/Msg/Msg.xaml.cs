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

namespace ChatPal.MVVM.View.Msg
{
    /// <summary>
    /// Interakční logika pro Msg.xaml
    /// </summary>
    public partial class Msg : UserControl
    {
        public Msg()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty UsernameProperty =
            DependencyProperty.Register("Msg", typeof(string), typeof(Msg), new PropertyMetadata(string.Empty));

        public string Username
        {
            get { return (string)GetValue(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }
    }
}
