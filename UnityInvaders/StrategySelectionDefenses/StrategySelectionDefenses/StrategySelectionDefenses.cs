using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StrategySelectionDefenses
{ 
    public class StrategySelectionDefenses : IStrategySelectionDefenses
    {
        public IEnumerable<IDefense> GetDefenses(IEnumerable<IDefense> defenses, int maxAses)
        {
            int ases = maxAses;
            var selectedDefenses = new List<IDefense>();

            List<PonderatedDefense> possibleDefenses = GetWeightedDefensesList(defenses);
            possibleDefenses = possibleDefenses.OrderByDescending(x => x.Weight).ToList();

            while(possibleDefenses.Any(x => x.Defense.Cost <= ases))
            {
                var possibleDefensesByCost = possibleDefenses.Where(x => x.Defense.Cost <= ases);

                PonderatedDefense ponderatedDefense = possibleDefensesByCost.First();
                selectedDefenses.Add(ponderatedDefense.Defense);
                possibleDefenses.Remove(ponderatedDefense);
                ases -= ponderatedDefense.Defense.Cost;
            }

            return selectedDefenses;
        }

        private List<PonderatedDefense> GetWeightedDefensesList(IEnumerable<IDefense> defenses)
        {
            var ponderatedDefenses = new List<PonderatedDefense>();

            foreach(IDefense defense in defenses)
                ponderatedDefenses.Add(new PonderatedDefense(defense, GetWeight(defense)));

            return ponderatedDefenses;
        }

        private double GetWeight(IDefense defense)
        {
            return defense.Health * 0.2 + defense.Range * 0.3 + defense.Damage * 0.4 + defense.Dispersion * 0.1;
        }
    }

    public class PonderatedDefense
    {
        public IDefense Defense { get; set; }
        public double Weight { get; set; }

        public PonderatedDefense(IDefense defense, double weight)
        {
            Defense = defense;
            Weight = weight;
        }
    }
}
