using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            numericUpDown1.Value = 1; // Ustawia początkową wartość numericUpDown1 na minimalną wartość
            numericUpDown1.Minimum = 1; // Ustawia minimalną wartość dla numericUpDown1 na 1

            numericUpDown2.Value = 2; // Ustawia początkową wartość numericUpDown2 na 2
            numericUpDown2.Maximum = 20; // Ustawia maksymalną wartość dla numericUpDown2 na 20
            numericUpDown2.Minimum = 2;

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        

        
        private void numericUpDown1_ValueChanged(object sender, EventArgs e) //ilosc bomb
        {
            int gridSize = (int)numericUpDown2.Value;
            int maxBombCount = (gridSize * gridSize);

            
            
            numericUpDown1.Maximum = maxBombCount;
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e) // wielkosc planszy
        {
            int gridSize = (int)numericUpDown2.Value;
            int maxBombCount = (gridSize * gridSize);

            
            numericUpDown1.Maximum = maxBombCount;
        }
        private void buttonGRAJ_Click(object sender, EventArgs e)
        {
            int bomby = (int)numericUpDown1.Value;
            int plansza = (int)numericUpDown2.Value;
            Form2 form = new Form2(plansza, bomby);
            form.Show();
        }


        
    }
}
