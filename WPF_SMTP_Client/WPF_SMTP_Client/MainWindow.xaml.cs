using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

namespace WPF_SMTP_Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string filePath;
        private bool isHasAttachment;
        

        public MainWindow()
        {
            InitializeComponent();
            TextBlock_Password.Password = "rwypqtycrxiaclwt";
            ListBlox_Adressat.Items.Add("testEmail.SHAG@gmail.com");
        }



        private void Button_Send_Click(object sender, RoutedEventArgs e)
        {
            if(TextBlock_Sender.Text.Length < 12) 
            {
                Label_StatusInfo.Foreground = Brushes.Red;
                Label_StatusInfo.Content = "Insert please sender email";
                return;
            }
            if (TextBlock_Theme.Text.Length < 3)
            {
                Label_StatusInfo.Foreground = Brushes.Red;
                Label_StatusInfo.Content = "Insert please theme email";
                return;
            }
            //if (TextBlock_Adressat.Text.Length < 12)
            //{
            //    Label_StatusInfo.Foreground = Brushes.Red;
            //    Label_StatusInfo.Content = "Insert please reciver email";
            //    return;
            //}
            if (TextBox_Message.Text.Length < 3)
            {
                Label_StatusInfo.Foreground = Brushes.Red;
                Label_StatusInfo.Content = "Insert please message";
                return;
            }
            if (TextBlock_Password.Password.Length < 8)
            {
                Label_StatusInfo.Foreground = Brushes.Red;
                Label_StatusInfo.Content = "Insert please password";
                return;
            }

            ///mail ----------------------------------- start
            MailMessage mail = new MailMessage();
            
            mail.From = new MailAddress(TextBlock_Sender.Text);
            
            //mail.To.Add(TextBlock_Adressat.Text);
            foreach (string s in ListBlox_Adressat.Items) 
            { 
                mail.To.Add(s);
            }

            mail.Subject = TextBlock_Theme.Text;

            mail.Body = TextBox_Message.Text;

            if(isHasAttachment == true)
            {
                mail.Attachments.Add(new Attachment(filePath));
            }

            ///mail ----------------------------------- END

            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(TextBlock_Sender.Text, TextBlock_Password.Password);
            
            smtpClient.Send(mail);
            mail.Dispose();

            MessageBox.Show("Sended");
            ResetAllFormDate();
            isHasAttachment = false;
        }

        private void Button_Reset_Click(object sender, RoutedEventArgs e)
        {
            ResetAllFormDate();

        }

        private void ResetAllFormDate()
        {
            foreach (var item in Grid_MainControl.Children)
            {
                if (item is TextBox)
                {
                    ((TextBox)item).Text = string.Empty;
                }
                else if (item is PasswordBox)
                {
                    ((PasswordBox)item).Password = string.Empty;
                }

            }
            Label_StatusInfo.Foreground = Brushes.Black;
            Label_StatusInfo.Content = "READY";

            Label_FileUpload.Content = "File upload";
            Label_FileUpload.Background = null;

            ListBlox_Adressat.Items.Clear();
            TextBox_Address.Text = string.Empty;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            openFileDialog.InitialDirectory = "C:\\Users\\Admin\\Desktop\\";
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;

            if (openFileDialog.ShowDialog() == true)
            {
              //Get the path of specified file
              filePath = openFileDialog.FileName;
              isHasAttachment = true;
                Label_FileUpload.Content = "File uploadet";
                Label_FileUpload.Background = Brushes.Green;
            }

        }

        private void Button_addAdressat_Click(object sender, RoutedEventArgs e)
        {
            if (TextBox_Address.Text.Length > 12)
            {
                ListBlox_Adressat.Items.Add(TextBox_Address.Text);
                TextBox_Address.Text = string.Empty;
            }
            else
            {
                Label_StatusInfo.Foreground = Brushes.Red;
                Label_StatusInfo.Content = "Insert please reciver email";
            }
        }
    }
}
