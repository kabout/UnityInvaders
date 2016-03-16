namespace UnityInvaders.Model
{
    public class Position
    {
        #region Properties

        public int X { get; set; }
        public int Y { get; set; }

        #endregion

        #region Constructors

        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        #endregion
    }
}
