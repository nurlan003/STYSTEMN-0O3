using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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

namespace WpfApp4
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
        string FileText;
       
        static string XOREncrypt(string text, string key)
        {
            StringBuilder encryptedText = new StringBuilder();
            
            for (int i = 0; i < text.Length; i++)
            {
         
                int charValue = text[i];
                int keyValue = key[i % key.Length];

                int encryptedValue = charValue ^ keyValue;
                encryptedText.Append((char)encryptedValue);
                
            }

            return encryptedText.ToString();
        }
        static string DosyadanMetinCek(string dosyaYolu)
        {
            string metin = string.Empty;

            try
            {
                // FileStream ve StreamReader kullanarak dosyadan metni oku
                using (FileStream fileStream = new FileStream(dosyaYolu, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        // Dosyanın sonuna kadar okuma yap
                        metin = streamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda mesajı ekrana yazdır
                Console.WriteLine("Hata: " + ex.Message);
            }

            return metin;
        }
        static void StringiDosyayaYaz(string dosyaYoluu, string metin)
        {
            try
            {
                // FileStream ve StreamWriter kullanarak dosyaya yazma işlemi gerçekleştir
                using (FileStream fileStream = new FileStream(dosyaYoluu, FileMode.Create, FileAccess.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.UTF8))
                    {
                        // String'i dosyaya yaz
                        streamWriter.Write(metin);
                    }
                }
            }
            catch (Exception ex)
            {
                // Hata durumunda mesajı ekrana yazdır
                Console.WriteLine("Hata: " + ex.Message);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            prgbar.Maximum=FileText.Length;
            if (encbtn.IsChecked==true) {
                string dosyaYolu = txtpath.Text;
                string keyy =passtxt.Text;
                string sifreliMetin = XOREncrypt(FileText, keyy);
                StringiDosyayaYaz(dosyaYolu, sifreliMetin);
                MessageBox.Show(sifreliMetin);

            };



            if (dencbtn.IsChecked == true) {
                string dosyaYolu = txtpath.Text;
                string keyy = passtxt.Text;
                string sifreliMetin = XOREncrypt(FileText, keyy);
                StringiDosyayaYaz(dosyaYolu, FileText);
                MessageBox.Show(FileText);
            };
            for (int i = 0;i< prgbar.Maximum;i++) {
                Thread.Sleep(100);
                prgbar.Value++;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Dosya yolu TextBox'a atanır.
                    txtpath.Text = openFileDialog.FileName;

                    // Dosya içeriği bir string'e okunur.
                    FileText = File.ReadAllText(openFileDialog.FileName);

                    // Dosya içeriğini kullanabilirsiniz.
                    // Örneğin, bir MessageBox'ta gösterelim:
                    MessageBox.Show("Dosya İçeriği:\n\n" + FileText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Dosya açma hatası: " + ex.Message);
                }
            }
        }
    }
}
