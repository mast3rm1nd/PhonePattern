using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


using System.IO;
using System.Text.RegularExpressions;

namespace PhonePattern
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private string[] words;

        public MainWindow()
        {
            InitializeComponent();

            if (!File.Exists("dict.txt"))
            {
                MessageBox.Show("Отсутствует словарь dict.txt");
                PhoneNum_TextBox.IsEnabled = false;
                Results_TextBox.IsEnabled = false;

                return;
            }

            words = File.ReadAllLines("dict.txt");
        }

        private void PhoneNum_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var nums = PhoneNum_TextBox.Text.Split(new char[] { ' ', '-'}, StringSplitOptions.RemoveEmptyEntries);

            if (nums.Any(num => !Regex.IsMatch(num, @"\d+")))
            {
                return;
            }


            var output = "";



            foreach (var num in nums) // num = "521"
            {
                output += $"{num}: ";

                var regex = GetRegexPhoneNumPatternFromNum(num);

                var foundWords = words.Where(word => Regex.IsMatch(word, regex)).ToArray();


                for (int i = 0; i < foundWords.Length; i++)
                {
                    if (i != foundWords.Length - 1)
                        output += $"{foundWords[i]}, ";
                    else
                        output += $"{foundWords[i]}.{Environment.NewLine}";
                }

                if(foundWords.Length == 0)
                    output += $"No matches.{Environment.NewLine}";


                //foreach (var digit in num) // digit = '5'
                //{
                //    var digitChar = int.Parse(digit.ToString());


                //}
                Results_TextBox.Text = output;
            }
        }



        string GetRegexPhoneNumPatternFromNum(string number)
        {
            var regex = "^";

            foreach (var digit in number)
            {
                switch (digit)
                {
                    case '2':
                        regex += "[abc]"; break;
                    case '3':
                        regex += "[def]"; break;
                    case '4':
                        regex += "[ghi]"; break;
                    case '5':
                        regex += "[jkl]"; break;
                    case '6':
                        regex += "[mno]"; break;
                    case '7':
                        regex += "[pqrs]"; break;
                    case '8':
                        regex += "[tuv]"; break;
                    case '9':
                        regex += "[wxyz]"; break;

                }
            }

            regex += "$";


            return regex;
        }
    }
}
