using System;

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

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            Position objAsPart = obj as Position;

            if (objAsPart == null)
                return false;

            else return this.X == objAsPart.X && this.Y == objAsPart.Y;
        }
        public override int GetHashCode ()
        {
            return Convert.ToInt32(string.Format("{0}{1}", X, Y));
        }
    }
}
