

public interface IMapController
{
    /// <summary>
    /// Devuelve un mapa vacío con tamaño size x size
    /// </summary>
    /// <param name="size">Tamaño del mapa</param>
    /// <param name="cellSize">Tamaño de la celda</param>
    /// <returns></returns>
    IMap GetEmptyMap(int size, int cellSize);
    /// <summary>
    /// Inicializa el mapa map para un nivel de dificultad difficultLevel.
    /// Añade los obstaculos y las defensas.
    /// El mapa debe estar vacío para poder inicializarlo.
    /// </summary>
    /// <param name="map">Mapa a inicializar</param>
    void InitMap(IMap map);
    /// <summary>
    /// Añade un alien al mapa en una posición libre del mapa.
    /// </summary>
    /// <param name="map">MApa en donde añadir el alien</param>
    void AddAliens(IMap map);
}