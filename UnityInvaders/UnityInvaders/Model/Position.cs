namespace UnityInvaders.Model
{
    public class Position
    {
        #region Properties

        public uint X { get; set; }
        public uint Y { get; set; }

        #endregion

        #region Constructors

        public Position(uint x, uint y)
        {
            X = x;
            Y = y;
        }

        #endregion
    }
}
