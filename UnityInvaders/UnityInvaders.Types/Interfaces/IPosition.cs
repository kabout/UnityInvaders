public interface IPosition
{
    float X { get; set; }
    float Y { get; set; }
    float Z { get; set; }

    /// <summary>
    /// Multiplica la posición por n
    /// </summary>
    /// <param name="n">Número por el que se multiplica la posición</param>
    /// <returns>Devuelve la posición resultado</returns>
    IPosition Multiply(int n);
}
