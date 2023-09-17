using System;
using System.Reflection.Emit;
using System.Windows.Forms;

namespace LabForm6
{
    public partial class Form1 : Form
    {
        private Random random = new Random();
        private const int step = 8;
        private bool isGameRunning = true;
        private int score = 0;
        public Form1()
        {
            InitializeComponent();
            MovePictureBoxAsynñ();
            this.KeyPreview = true;
            this.KeyDown += Form1_KeyDown;
        }


        private async void MovePictureBoxAsynñ()
        {
            while (true)
            {
                if (isGameRunning)
                {
                    Point newLocation = GenerateRandomLocation();

                    for (int i = 0; i < 100; i++)
                    {
                        double progress = i / 100.0;
                        int newX = (int)(pictureBox1.Location.X + (newLocation.X - pictureBox1.Location.X) * progress);
                        int newY = (int)(pictureBox1.Location.Y + (newLocation.Y - pictureBox1.Location.Y) * progress);
                        pictureBox1.Location = new Point(newX, newY);
                        await Task.Delay(10);
                    }

                    await Task.Delay(500);
                }

            }
        }
        private void RestartGame()
        {
            isGameRunning = true;
            pictureBox1.Location = GenerateRandomLocation();
        }

        private Point GenerateRandomLocation()
        {
            int x = random.Next(this.ClientSize.Width - pictureBox1.Width);
            int y = random.Next(this.ClientSize.Height - pictureBox1.Height);
            return new Point(x, y);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    movePicture(pictureBox2, 0, -step);
                    break;

                case Keys.S:
                    movePicture(pictureBox2, 0, step);
                    break;

                case Keys.A:
                    movePicture(pictureBox2, -step, 0);
                    break;

                case Keys.D:
                    movePicture(pictureBox2, step, 0);
                    break;

            }

            if (e.KeyCode == Keys.Enter)
            {
                if (pictureBox2.Bounds.IntersectsWith(pictureBox1.Bounds))
                {

                    score += 10;
                    label2.Text = "Score: " + score.ToString();
                    RestartGame();
                }
            }

        }
        private void movePicture(PictureBox pictureBox, int x, int y)
        {
            pictureBox.Location = new Point(pictureBox.Location.X + x, pictureBox.Location.Y + y);
        }

    }
}