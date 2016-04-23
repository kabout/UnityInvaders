using System;

namespace UnityInvaders.Utils
{
    public class RandomManager
    {
        #region Fields

        private static Random r = new Random();

        #endregion Fields

        #region Properties

        /// <summary>
        /// Semilla del random
        /// </summary>
        public static int Seed { set { r = new Random(value); } }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Devuelve un número entre a y b
        /// </summary>
        /// <param name="a">Intervalo inferior</param>
        /// <param name="b">Intervalo superior</param>
        public static float GetRandomNumber(float a, float b)
        {
            return (float)(a + r.NextDouble() * (b - a));
        }

        /// <summary>
        /// Devuelve un número entre 0 y b
        /// </summary>
        /// <param name="a">Intervalo inferior</param>
        /// <param name="b">Intervalo superior</param>
        public static float GetRandomNumber(float b)
        {
            return GetRandomNumber(0, b);
        }

        /// <summary>
        /// Devuelve un número entre 0 y 1
        /// </summary>
        /// <param name="a">Intervalo inferior</param>
        /// <param name="b">Intervalo superior</param>
        public static float GetRandomFloatNumber()
        {
            return GetRandomNumber(0, 1);
        }

        /// <summary>
        /// Devuelve un número entre [a y b)
        /// </summary>
        /// <param name="a">Intervalo inferior</param>
        /// <param name="b">Intervalo superior</param>
        public static int GetRandomNumber(int a, int b)
        {
            return r.Next(a, b);
        }

        /// <summary>
        /// Devuelve un número entre 0 y b
        /// </summary>
        /// <param name="a">Intervalo inferior</param>
        /// <param name="b">Intervalo superior</param>
        public static int GetRandomNumber(int b)
        {
            return GetRandomNumber(0, b);
        }

        /// <summary>
        /// Devuelve un número entre 0 y 1
        /// </summary>
        /// <param name="a">Intervalo inferior</param>
        /// <param name="b">Intervalo superior</param>
        public static int GetRandomIntNumber()
        {
            return GetRandomNumber(0, 1);
        }

        #endregion Methods
    }
}
