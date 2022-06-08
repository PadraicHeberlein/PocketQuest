namespace PocketQuest.GamePlay
{
    public class Player
    {
        public readonly Point spawnLocation;
        public readonly int width, height;

        Bitmap skin, mask;
        Point position;

        public Player()
        {
            spawnLocation = new Point(0, PocketForm.HORIZON - height);
            position = spawnLocation;

            skin = (Bitmap)Image.FromFile("player//skin.png");
            mask = (Bitmap)Image.FromFile("player//mask.png");

            width = skin.Width;
            height = skin.Height;
        }

        public Bitmap Draw(Bitmap background)
        {
            int screenWidth = background.Width;
            int screenHeight = background.Height;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Color nextPixel = mask.GetPixel(i, j);

                    if (PixelIsVoid(nextPixel))
                    {
                        background.SetPixel(
                            position.X + i, position.Y + j, skin.GetPixel(i, j)
                            );
                    }
                }
            }

            return background;
        }

        public Bitmap Move(Direction d, Bitmap background)
        {
            switch (d)
            {
                case Direction.Up:
                    position.Y--;
                    break;
                case Direction.Down:
                    position.Y++;
                    break;
                case Direction.Left:
                    position.X--;
                    break;
                case Direction.Right:
                    position.X++;
                    break;
            }

            return Draw(background);
        }

        private bool PixelIsVoid(Color pixel)
        {
            return !pixel.Name.Equals(PocketForm.NO_PIXEL);
        }
    }
}
