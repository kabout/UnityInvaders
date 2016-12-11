using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Position : IPosition
{
    #region Properties

    public float X { get; set; }

    public float Y { get; set; }

    public float Z { get; set; }

    #endregion

    #region Constructors

    public Position(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    #endregion

    public IPosition Multiply(int n)
    {
        return new Position(X * n, Y * n, Z * n);
    }

    public override bool Equals(object obj)
    {
        if (obj == null)
            return false;

        Position objAsPart = obj as Position;

        if (objAsPart == null)
            return false;
        return X == objAsPart.X && Z == objAsPart.Z;
    }

    public override int GetHashCode()
    {
        return Convert.ToInt32(string.Format("{0}{1}", X, Z));
    }
}
