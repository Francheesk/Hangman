using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hangman
{
    public partial class Form1 : Form
    {
        Bitmap DrawArea;
        Bitmap hangmanImage;
        char[] alfabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        string[] beginnerWords = File.ReadAllLines("file.txt", Encoding.UTF8);
        string[] advancedWords = { "PARALELIPIPED", "INTANGIBIL", "AVANSAT" };
        int beginnerHints = 2;
        int advancedHints = 3;
        List<char> solved = new List<char>();
        string temporar;
        int hangmanCounter = 1;
        string concatenat;
        string word;
        char[] wordLetters;
        bool start=false;
        public Form1()
        {
            InitializeComponent();
            DrawArea = new Bitmap(pictureBox1.Size.Width, pictureBox1.Size.Height);
            pictureBox1.Image = DrawArea;
            hangmanImage = new Bitmap(pictureBox2.Size.Width, pictureBox2.Size.Height);
            pictureBox2.Image = hangmanImage;
            this.KeyPreview = true;
    
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
       
        private void Button1_Click(object sender, EventArgs e)
        {
            start = true;
            Graphics graphic = Graphics.FromImage(pictureBox1.Image);
            graphic.Clear(Color.White);
            solved.Clear();
            wordLetters = null;
            hangmanCounter = 1;

            if (radioButton1.Checked)
            {
                word= alesRandom(beginnerWords, beginnerHints);
            }
            else {
               word= alesRandom(advancedWords, advancedHints);
            }

            wordLetters = word.ToCharArray();
            temporar = word;
            
            printWord(wordLetters);
           
            drawHangman(1);

        }

        
        private void printWord(char[] word)
        {
            Graphics g;
            g = Graphics.FromImage(DrawArea);

            Font myFont = new System.Drawing.Font("Helvetica", 15);
            Brush myBrush = new SolidBrush(System.Drawing.Color.Black);
            int dist = 0;
            foreach (char letter in word)
           {
                if(solved.Contains(letter))
                {
                    g.DrawString(letter.ToString(), myFont, myBrush, dist, 15);
                } else
                {
                    g.DrawString("_", myFont, myBrush, dist, 15);
                }
                
                dist = dist + 40;
            }
            pictureBox1.Image = DrawArea;
            g.Dispose();
        }
        private string alesRandom(string[] cuvinte, int hinturi)
        {

            Random rnd = new Random();
            string word;
            int ales;
            ales = rnd.Next(cuvinte.Length);
            word = cuvinte[ales];
            char[] wordLetters = word.ToCharArray();
            for (int i = 1; i <= hinturi; i++)
            {
                ales = rnd.Next(word.Length);
                solved.Add(wordLetters[ales]);
            }
            return word;
        }

        private void RadioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void drawHangman(int counter)
        {
         
            concatenat = String.Concat(counter, ".png");
            pictureBox2.Load(concatenat);
          
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool esteLiterabuna = false;
            if (!start){
                MessageBox.Show("N-ati dat start", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if ((e.KeyChar >= 'a' && e.KeyChar <= 'z' )|| (e.KeyChar >= 'A' && e.KeyChar <= 'Z'))
            {
                if (solved.Contains(Char.ToUpper(e.KeyChar)))
                {
                    MessageBox.Show("Litera ii deja in cuvant", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    
                    return;
                }
                for (int i = 0; i < wordLetters.Length; i++)
                {

                    if (Char.ToUpper(e.KeyChar) == wordLetters[i])
                    {
                       
                        
                            solved.Add(Char.ToUpper(e.KeyChar));
                            printWord(wordLetters);
                            esteLiterabuna = true;
                        
                    }
                }
                if (!esteLiterabuna)
                {
                    hangmanCounter++;
                    drawHangman(hangmanCounter);
                }
               
            }
          
            else
            {
                
                MessageBox.Show("Ati introdus o nonlitera, " + e.KeyChar, "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            if (hangmanCounter == 7) {
                MessageBox.Show("Ati pierdut jocul, apasati ok pentru a reincepe" , "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                button1.PerformClick();
            }
            e.Handled = true;
           
        }
    }
}
