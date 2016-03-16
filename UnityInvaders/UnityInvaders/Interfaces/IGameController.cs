namespace UnityInvaders.Interfaces
{
    public interface IGameController
    {
        /// <summary>
        /// Muestra el menú del juego
        /// </summary>
        /// <returns>Devuelve la opción seleccionada</returns>
        int ShowMenu();
        /// <summary>
        /// Inicia el juego de jugador contra la máquina
        /// </summary>
        void InitUserVsComputer();
        /// <summary>
        /// Inicia el juego de jugador contra jugador
        /// </summary>
        void InitUserVsUsers();
    }
}
