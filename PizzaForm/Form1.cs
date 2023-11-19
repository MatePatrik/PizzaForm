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

namespace PizzaForm
{
    public partial class Form1 : Form
    {
        int osszertek = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            openFileDialog1.InitialDirectory = Environment.CurrentDirectory;
            openFileDialog1.FileName = "pizza.csv";
            using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
            {
                sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    listBox_Pizzak.Items.Add(new Pizza(sr.ReadLine()));
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int osszar=0;
            double atlag = 0;
            foreach (Pizza item in listBox_Pizzak.Items)
            {
                osszar+=item.Ar;
            }
            atlag=osszar/listBox_Pizzak.Items.Count;
            MessageBox.Show($"A pizzák átlagára: {atlag}","Átlagár", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void textBox_Rendeles_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void listBox_Pizzak_SelectedIndexChanged(object sender, EventArgs e)
        {
            Pizza Rendeles = (Pizza)listBox_Pizzak.SelectedItem;
            textBox_Rendeles.Text = Rendeles.PizzaNev;
            int pizzakossz = 0;
            pizzakossz += Rendeles.Ar * (int)numericUpDown_Mennyiseg.Value;
            textBox_osszeg.Text = pizzakossz.ToString();
        }
        private void numericUpDown_Mennyiseg_ValueChanged(object sender, EventArgs e)
        {

            if (listBox_Pizzak.SelectedIndex >= 0) 
            {
                int pizzakOssz = 0;
                Pizza Rendeles = (Pizza)listBox_Pizzak.SelectedItem;
                pizzakOssz += Rendeles.Ar * (int)numericUpDown_Mennyiseg.Value;
                textBox_osszeg.Text = pizzakOssz.ToString();
            }
            else
            {
                numericUpDown_Mennyiseg.Value = 0;
                MessageBox.Show("Válassz egy pizzát","Nincs kiválasztva pizza",MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
            
        }

        private void button_hozzad_Click(object sender, EventArgs e)
        {
            
            if (listBox_Pizzak.SelectedIndex>=0 && numericUpDown_Mennyiseg.Value>0)
            {
                int ossz = int.Parse(textBox_osszeg.Text);
                osszertek += ossz;
                listBox_Kosar.Items.Add($"{listBox_Pizzak.SelectedItem} {numericUpDown_Mennyiseg.Value}db.");
            }
           
        }

        private void button_fizetes_Click(object sender, EventArgs e)
        {
            if (radioButton_Keszpenz.Checked && listBox_Kosar.Items.Count >= 1 || radioButton_Bankkartya.Checked&&listBox_Kosar.Items.Count>=1)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Szöveges fájl|*.txt";
                saveFileDialog.InitialDirectory = Environment.CurrentDirectory;
                saveFileDialog.FileName = "rendeles.txt";
                string rendelesfajl = saveFileDialog.FileName;


                using (StreamWriter sw=new StreamWriter(rendelesfajl, true))
                {
                    string fizetesimod="";
                    string szamla="";

                    if (radioButton_Bankkartya.Checked)
                    {
                        fizetesimod="Bankkártya";
                    }
                    else if (radioButton_Keszpenz.Checked)
                    {
                        fizetesimod="Készpénz";
                    }

                    if (checkBox_Szamla.Checked)
                    {
                        szamla="Kér";
                    }
                    else 
                    {
                        szamla="Nem kér";
                    }

                    sw.WriteLine($"Számlát {szamla}, Fizetési mód: {fizetesimod}");
                    foreach (var item in listBox_Kosar.Items)
                    {
                        sw.WriteLine(item);
                    }
                    sw.WriteLine($"Fizetendő összeg: {osszertek}");
                    osszertek = 0;
                    listBox_Kosar.Items.Clear();
                }
            }
            else
            {
                MessageBox.Show("Minden adatot tölts ki!");
            }
        }

        private void groupBox_Kosar_MouseHover(object sender, EventArgs e)
        {
            groupBox_Kosar.BackColor = Color.LightGray;
        }

        private void Form1_MouseEnter(object sender, EventArgs e)
        {
            groupBox_Kosar.BackColor = DefaultBackColor;
            groupBox_Fizetes.BackColor = DefaultBackColor;
        }

        private void groupBox_Fizetes_MouseHover(object sender, EventArgs e)
        {
            groupBox_Fizetes.BackColor = Color.LightGray;
        }
    }
}
