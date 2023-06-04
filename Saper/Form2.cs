using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Saper
{
    public partial class Form2 : Form
    {
        private const int ButtonSize = 40;
        private readonly int boardSize;
        private Button[,] buttons;
        private HashSet<Button> targets;
        private int timeLeft;
        

        public Form2(int wielkosc_planszy, int ilosc_bomb)
        {
            InitializeComponent();
            boardSize = wielkosc_planszy;
            //tutaj miałem problem, bo poniżej rozmiaru 5x5 przyciskow czas jest ucięty, a zwiększenie rozmiaru okna powoduje złe rozmieszczenie przycisków
            this.ClientSize = new Size(ButtonSize * boardSize, ButtonSize * boardSize);

            buttons = new Button[boardSize, boardSize];
            targets = new HashSet<Button>();
            CreateButtons();
            AddTargets(ilosc_bomb);
            int totalTime = 3 * boardSize;
            timeLeft = totalTime;

            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            timer1.Start();


        }


        
        private void CreateButtons()
        {
            

            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.Controls.Clear();

            tableLayoutPanel1.ColumnCount = boardSize;
            tableLayoutPanel1.RowCount = boardSize;
            int tableWidth = ButtonSize * boardSize;
            int tableHeight = ButtonSize * boardSize;
            tableLayoutPanel1.Size = new Size(tableWidth, tableHeight);


            for (int row = 0; row < boardSize; row++)
            {
                tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 50));
                for (int col = 0; col < boardSize; col++)
                {
                    tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));

                    Button button = new Button
                    {
                        Size = new Size(ButtonSize, ButtonSize),
                        Margin = Padding.Empty,
                        Padding = Padding.Empty
                    };

                    buttons[row, col] = button;
                    //button.Dock = DockStyle.Fill;
                    tableLayoutPanel1.Controls.Add(button, col, row);

                    button.Click += Button_Click;
                }

            }
        }

        private void AddTargets(int targetCount)
        {
            if(targetCount == 0)
            {
                MessageBox.Show("Nie wybrano żadnych celów", "UWAGA!!!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Random random = new Random();
            
            int count = 0;
            while (count < targetCount)
            {
                int row = random.Next(boardSize);
                int col = random.Next(boardSize);

                Button targetButton = buttons[row, col];

                if (!targets.Contains(targetButton))
                {
                    targets.Add(targetButton);
                    count++;
                }
            }
        }
        private void Button_Click(object sender, EventArgs e)
        {

            Button clickedButton = (Button)sender;
            if (!targets.Contains(clickedButton))
            {
            clickedButton.BackColor = Color.Gray;
            }
            if (targets.Contains(clickedButton))
            {
                clickedButton.BackColor = Color.Green;
                clickedButton.Enabled = false;
                targets.Remove(clickedButton);
                if (targets.Count == 0)
                {
                    timer1.Stop();
                    MessageBox.Show("Gratulacje, znaleziono wszystkie bomby!", "Wygrałeś!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            label1.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timeLeft--;

            
            if (timeLeft <= 0)
            {
                timer1.Stop();
                if(!this.IsDisposed && this.Visible)
                {
                    MessageBox.Show("Skończył się czas.", "Przegrałeś", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            UpdateTimerLabel();
        }
        private void UpdateTimerLabel()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(timeLeft);
            string timeString = string.Format("{0}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
            this.Text = "Czas: " + timeString;
        }
    }


}


