using PocketQuest.Graphics.LinearAlgebraR3;

/* SIDE VIEW OF A POSSIBLE LANE:
 * 
 * TILE_WIDTH = width of '_'
 * 
 *  z   <---------------------------------- MAX_PATH_HEIGHT
 *  ^
 *  |__> x        _______      ______
 *  \        ____/\______\____/\_____\___________
 *   y  ____/\___\/       \___\/      \__________\_____
 *       ___\/                                    \______ */

namespace PocketQuest.Environment
{
    public class Lane
    {
        private PointR3 origin;
        private List<double> elevations;
        private List<Tile> mesh;

        public Lane(PointR3 origin)
        {
            this.origin = origin;
            GenerateElevations();
            ConstructMesh();
        }
        // Procedurally generate lane elevations: 
        private void GenerateElevations()
        {
            double previousElevation = origin.Get(CoordinateR3.X);

            elevations = new List<double>();

            Random rando = new Random();

            while (elevations.Count < Constants.MAX_PATH_HEIGHT)
            {
                elevations.Add(previousElevation);

                LaneDirection direction = (LaneDirection)rando.NextInt64(3);

                if (direction.Equals(LaneDirection.Down))
                    previousElevation--;
                else if (direction.Equals(LaneDirection.Up))
                    previousElevation++;

                if (previousElevation < Constants.MAX_PATH_HEIGHT)
                    previousElevation--;
            }
        }
        // Construct trianle mesh from generated elevations:
        private void ConstructMesh()
        {
            mesh = new List<Tile>();

            List<PointR3> backPoints = GetPoints();
            List<PointR3> frontPoints = GetPoints(false);

            for (int tile = 0; tile < Constants.MAX_PATH_LENGTH; tile++)
            {
                List<PointR3> corners = new List<PointR3>();
                /* NOTE: The order these points are added to
                 * corners matters for Tile() instantiation!    */
                corners.Add(backPoints[tile]);
                corners.Add(backPoints[tile + 1]);
                corners.Add(frontPoints[tile]);
                corners.Add(frontPoints[tile + 1]);

                mesh.Add(new Tile(corners.ToArray()));
            }
        }

        /* Clarification for GetPoints():
         * 
         *  y-->x   back points ->  -._._._._._
         *  |                        | | | | |   <-lane->
         *  V       front points->  -'-'-'-'-'-
         *                                                      */
        private List<PointR3> GetPoints(bool back = true)
        {
            List<PointR3> points = new List<PointR3>();
            
            double xp = origin.Get(CoordinateR3.X);
            double yp = back ? origin.Get(CoordinateR3.Y) :
                origin.Get(CoordinateR3.Y) + Constants.TILE_WIDTH;
            double zp;

            for (int i = 0; i < elevations.Count; i++)
            {
                /* Ever iteration:
                 * x increments by the width of a tile,
                 * y stays the same,
                 * z becomes the next elevation value.      */
                xp += Constants.TILE_WIDTH;
                zp = elevations[i];

                PointR3 nextPoint = new PointR3(xp, yp, zp);
                points.Add(nextPoint);
            }

            return points;
        }
    }
}
