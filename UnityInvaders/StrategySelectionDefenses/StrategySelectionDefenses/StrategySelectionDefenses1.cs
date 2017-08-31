using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrategySelectionDefenses
{ 
    // Random
    public class StrategySelectionDefenses : IStrategySelectionDefenses
    {
        public IEnumerable<IDefense> GetDefenses(IEnumerable<IDefense> defenses, int maxAses)
        {
            int ases = maxAses;
            var selectedDefenses = new List<IDefense>();

            List<IDefense> possibleDefenses = new List<IDefense>(defenses);

            while(possibleDefenses.Any(x => x.Cost <= ases))
            {
                var possibleDefensesByCost = possibleDefenses.Where(x => x.Cost <= ases);

                IDefense defense = possibleDefensesByCost.First();
                selectedDefenses.Add(defense);
                possibleDefenses.Remove(defense);
                ases -= defense.Cost;
            }

            return selectedDefenses;
        }
    }
}
