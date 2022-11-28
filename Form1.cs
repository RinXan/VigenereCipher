using System.Text;

namespace VigenereCipher
{
    public partial class Form1 : Form
    {
        static readonly string cyrillic = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя0123456789";
        static readonly string latin = "abcdefghijklmnopqrstuvwxyz0123456789";

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string text = textBox1.Text;
                string key = textBox3.Text;
                bool lang = checkBox1.Checked; // if true -> cyrillic otherwise -> latin

                if (lang)
                {
                    Is_key_correct(key, cyrillic);
                    Is_text_correct(text, cyrillic);
                    textBox2.Text = Encrypt(text, key, cyrillic);
                }
                else
                {
                    Is_key_correct(key, latin);
                    Is_text_correct(text, latin);
                    textBox2.Text = Encrypt(text, key, latin);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                textBox2.Clear();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string text = textBox2.Text;
                string key = textBox3.Text;
                bool lang = checkBox1.Checked; // if true -> cyrillic otherwise -> latin

                if (lang)
                {
                    Is_key_correct(key, cyrillic);
                    Is_text_correct(text, cyrillic);
                    textBox1.Text = Decrypt(text, key, cyrillic);
                }
                else
                {
                    Is_key_correct(key, latin);
                    Is_text_correct(text, latin);
                    textBox1.Text = Decrypt(text, key, latin);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка");
                textBox1.Clear();
            }
        }

        private static string Encrypt(string text, string key, string alphabet)
        {
            try
            {
                var result = new StringBuilder();

                for (int i = 0; i < text.Length; i++)
                {
                    if (alphabet.Contains(text[i]))
                    {
                        result.Append(alphabet[
                            (alphabet.IndexOf(text[i]) + alphabet.IndexOf(key[i % key.Length])) % alphabet.Length
                        ]);
                    }
                    else
                    {
                        result.Append(text[i]);
                    }
                }

                return result.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
                return "Error";
            }
        }

        private static string Decrypt(string text, string key, string alphabet)
        {
            try
            {
                var result = new StringBuilder();

                for (int i = 0; i < text.Length; i++)
                {
                    if (alphabet.Contains(text[i]))
                    {
                        result.Append(alphabet[
                            (alphabet.IndexOf(text[i]) + alphabet.Length - alphabet.IndexOf(key[i % key.Length])) % alphabet.Length
                        ]);
                    } else
                    {
                        result.Append(text[i]);
                    }
                }

                return result.ToString();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
                return "Error";
            }
        }

        private static void Is_key_correct(string key, string alphabet)
        {
            alphabet = alphabet.Replace("0123456789", "");

            foreach (char s in key)
            {
                if (!alphabet.Contains(s))
                {
                    throw new Exception("Ключ должен состоять из соответствующих алфавиту букв и не содержать пробелов.");
                }
            }
        }

        private static void Is_text_correct(string text, string alphabet)
        {
            foreach (char s in text)
            {
                if (!alphabet.Contains(s))
                {
                    throw new Exception("Текст содержит не соответствующие алфавиту буквы или пробелы.");
                }
            }
        }
    }
}