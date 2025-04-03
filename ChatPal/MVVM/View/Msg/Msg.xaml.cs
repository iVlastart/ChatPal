using ChatPal.db;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChatPal.MVVM.View.Msg
{
    public partial class Msg : UserControl
    {
        public Msg()
        {
            InitializeComponent();
        }

        // PropertyChangedCallback to handle Username change
        public static readonly DependencyProperty UsernameProperty =
            DependencyProperty.Register("Username", typeof(string), typeof(Msg), new PropertyMetadata(string.Empty, OnPropertiesChanged));

        // PropertyChangedCallback to handle Content change
        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(string), typeof(Msg), new PropertyMetadata(string.Empty, OnPropertiesChanged));

        public string Username
        {
            get { return (string)GetValue(UsernameProperty); }
            set { SetValue(UsernameProperty, value); }
        }

        public string Content
        {
            get { return (string)GetValue(ContentProperty); }
            set { SetValue(ContentProperty, value); }
        }

        // PropertyChangedCallback for both properties
        private static void OnPropertiesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Msg msg)
            {
                // Check if both properties are set, and call createDelBtn
                if (!string.IsNullOrEmpty(msg.Username) && !string.IsNullOrEmpty(msg.Content))
                {
                    msg.createDelBtn();
                }
            }
        }

        private void createDelBtn()
        {
            // Only create the button if the Username matches the session username
            if (Username.Trim() == Session.Session.username.Trim())
            {
                Button btn = new()
                {
                    Content = "Delete",
                    Height = 50,
                    Width = 75,
                    Background = Brushes.Red,
                    Margin = new Thickness(5),
                };
                btn.Click += Btn_Click; // Attach click event handler
                Grid.SetColumn(btn, 1);
                Grid.SetRow(btn, 1);
                grid.Children.Add(btn);
            }
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            // Delete the message using Db.delMsg method
            Db.delMsg(Username, Content);

            // Show a message box confirming deletion
            MessageBox.Show("Message deleted");
            var parentStackPanel = this.Parent as StackPanel;
            if (parentStackPanel != null)
            {
                parentStackPanel.Children.Remove(this); // Remove this Msg UserControl from the StackPanel
            }
        }
    }
}