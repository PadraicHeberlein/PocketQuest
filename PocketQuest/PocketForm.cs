using PocketQuest.GamePlay;

namespace PocketQuest
{
    public partial class PocketForm : Form
    {
        public static int WIDTH = 2048;
        public static int HEIGHT = 1028;
        public static int HORIZON = 512 - 100;

        public static string NO_PIXEL = "0";

        PictureBox background;
        Player player;

        public PocketForm()
        {
            InitializeComponent();
            this.Text = "Pocket Quest";
            background = new PictureBox();
            background.SizeMode = PictureBoxSizeMode.StretchImage;
            Load += new EventHandler(PocketForm_Load);
            WindowState = FormWindowState.Maximized;
            player = new Player();
        }

        private void PocketForm_Load(object sender, EventArgs e)
        {
            background.Width = WIDTH;
            background.Height = HEIGHT;
            background.Name = "background";
            background.Location = new Point(0, 0);
            background.Image = 
                (Bitmap)Bitmap.FromFile("scenery//background.png");        

            this.Controls.Add(background);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            Bitmap alteredBackground = 
                (Bitmap)Bitmap.FromFile("scenery//background.png");
            char keyValue = (char)e.KeyValue;

            switch (keyValue)
            {
                case 'W':
                    background.Image = 
                        player.Move(Direction.Up, alteredBackground);
                    break;
                case 'D':
                    background.Image = 
                        player.Move(Direction.Right, alteredBackground);
                    break;
                case 'S':
                    background.Image = 
                        player.Move(Direction.Down, alteredBackground);
                    break;
                case 'A':
                    background.Image = 
                        player.Move(Direction.Left, alteredBackground);
                    break;
            }
        }

        private void PocketForm_Load_1(object sender, EventArgs e)
        {

        }
    }
}