using System.Collections.Generic;

public interface IStrategySelectionDefenses
{
    /// <summary>
    /// Selecciona las defensas más eficientes para incluiren el mapa con el tope de coste de maxAses.
    /// </summary>
    /// <param name="defenses">Lista de defensas generadas para elegir</param>
    /// <param name="maxAses">Gasto total</param>
    IEnumerable<IDefense> GetDefenses(IEnumerable<IDefense> defenses, int maxAses);
}
