using PocketQuest.Graphics.LinearAlgebraR3;

namespace PocketQuest.Environment
{
    public class ViewScreen : IGameObject3D
    {
        // named constants for the indices of the position vector bundle
        private const int EYE = 0;
        private const int PIXEL_OFFSET = 1;
        private const int LL_CORNER = 2;    // lower left-hand corner
        private const int UR_CORNER = 3;    // upper right-hand corner
        private const int BUNDLE_SIZE = 4;
        // named points for constructor initialization, MKS units
        private PointR3 SCREEN_WIDTH = new PointR3(0, 0.8, 0);
        private PointR3 SCREEN_HEIGHT = new PointR3(0, 0, 0.5);
        // center of the view screen
        private PointR3 INIT_POSITION = new PointR3(0, 0, 1.6);
        // where the viewer's eye is
        private PointR3 DIST_FROM_SCREEN = new PointR3(-0.7, 0, 0.1);   
        // private class members
        private int width, height;
        private VectorR3[] positionBundle;
        // constructor
        public ViewScreen(int theWidth, int theHeight)
        {
            width = theWidth;
            height = theHeight;
            positionBundle = GeneratePositionBundle();
        }
        public PlaneR3 GetViewPlane()
        {
            // origin is in upper left-hand corner
            PointR3 screenOrigin = positionBundle[PIXEL_OFFSET].ToPoint();
            PointR3 upperRightCorner = screenOrigin.Sub(SCREEN_WIDTH);
            PointR3 lowerLeftCorner = screenOrigin.Sub(SCREEN_HEIGHT);

            return 
                new PlaneR3(screenOrigin, upperRightCorner, lowerLeftCorner);
        }
        // returns the current position for a given pixel
        public PointR3 GetPixelPosition(int i, int j)
        {
            double iDistance = i * GetWidthOfPixel();
            double jDistance = j * GetHeightOfPixel();
            VectorR3 iVector = GetIHat().Sx(iDistance);
            VectorR3 jVector = GetJHat().Sx(jDistance);
            VectorR3 position = iVector.Add(jVector);

            return position.ToPoint().Add(GetPixelOffset());
        }
        // return vector (i hat) x (j hat)
        public VectorR3 GetViewDirection() 
            { return GetIHat().Cross(GetJHat()); }
        // returns the position of the viewer
        public PointR3 GetEyePosition() 
            { return positionBundle[EYE].ToPoint(); }
        // for rotating and moving the view screen,
        // required by IGameObject3D parent class
        public VectorR3[] GetVertices() { return positionBundle; }
        public void SetVertex(int index, VectorR3 vertex) 
            { positionBundle[index] = vertex; }
        // private methods for constructor intialization...
        private PointR3 GeneratePixelOffsetPoint()
        {
            PointR3 offset = new PointR3();

            return offset.Add(INIT_POSITION)
                .Add(SCREEN_HEIGHT.Sx(0.5))
                .Add(SCREEN_WIDTH.Sx(0.5));
        }
        private PointR3 GenerateEyePoint() 
        { 
            return DIST_FROM_SCREEN.Add(INIT_POSITION); 
        }
        private VectorR3[] GeneratePositionBundle()
        {
            VectorR3[] bundle = new VectorR3[BUNDLE_SIZE];

            bundle[EYE] = GenerateEyePoint().ToVector();
            bundle[PIXEL_OFFSET] = GeneratePixelOffsetPoint().ToVector();
            bundle[LL_CORNER] = 
                bundle[PIXEL_OFFSET].Sub(SCREEN_HEIGHT.ToVector());
            bundle[UR_CORNER] = 
                bundle[PIXEL_OFFSET].Sub(SCREEN_WIDTH.ToVector());

            return bundle;
        }
        // private methods for geting screen vetors i hat and j hat
        private VectorR3 GetIHat() 
        { 
            return positionBundle[UR_CORNER]
                .Sub(positionBundle[PIXEL_OFFSET]); 
        }
        private VectorR3 GetJHat() 
        { 
            return positionBundle[LL_CORNER]
                .Sub(positionBundle[PIXEL_OFFSET]); 
        }
        // private methods for the physical dimensions of each pixel
        private double GetWidthOfPixel() 
            { return SCREEN_WIDTH.Get(CoordinateR3.Y) / width; }
        private double GetHeightOfPixel() 
            { return SCREEN_HEIGHT.Get(CoordinateR3.Z) / height; }
        // upper left hand corner of the screen or (0,0) relative to screens coorddinates (i,j)
        private PointR3 GetPixelOffset() 
            { return new PointR3(positionBundle[PIXEL_OFFSET]); }
    }
}
