using Assets.Scripts.Interfaces;
using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading;

namespace Assets.Scripts.Manager
{
    public class StrategyFactory : IStrategyFactory
    {
        GameConfiguration gameConfiguration;
        IStrategyAlienAttack strategyAlienAttack;
        IStrategyLocationDefenses strategyLocationDefenses;
        IStrategySelectionDefenses strategySelectionDefenses;

        public StrategyFactory(GameConfiguration gameConfiguration)
        {
            this.gameConfiguration = gameConfiguration;
        }

        public IStrategyAlienAttack GetStrategyAlientAttack()
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork_StrategyAlientAttack;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted_StrategyAlientAttack;
            backgroundWorker.RunWorkerAsync(gameConfiguration);

            Thread.Sleep(100);

            while(backgroundWorker.IsBusy)
            { }

            Thread.Sleep(100);

            return strategyAlienAttack;
        }

        private void BackgroundWorker_RunWorkerCompleted_StrategyAlientAttack(object sender, RunWorkerCompletedEventArgs e)
        {
            strategyAlienAttack = e.Result as IStrategyAlienAttack;
        }

        private void BackgroundWorker_DoWork_StrategyAlientAttack(object sender, DoWorkEventArgs e)
        {
            GameConfiguration gameConfiguration = e.Argument as GameConfiguration;
            Assembly strategyAttackAliensAssembly = Assembly.LoadFile(gameConfiguration.StrategyAttackAliensDllPath);
            e.Result = (IStrategyAlienAttack)strategyAttackAliensAssembly.CreateInstance("StrategyAlienAttack.StrategyAlienAttack");            
        }

        public IStrategyLocationDefenses GetStrategyLocationDefenses()
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork_LocationDefenses;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted_LocationDefenses;
            backgroundWorker.RunWorkerAsync(gameConfiguration);

            Thread.Sleep(100);

            while (backgroundWorker.IsBusy)
            { }

            Thread.Sleep(100);

            return strategyLocationDefenses;
        }

        private void BackgroundWorker_RunWorkerCompleted_LocationDefenses(object sender, RunWorkerCompletedEventArgs e)
        {
            strategyLocationDefenses = e.Result as IStrategyLocationDefenses;
        }

        private void BackgroundWorker_DoWork_LocationDefenses(object sender, DoWorkEventArgs e)
        {
            GameConfiguration gameConfiguration = e.Argument as GameConfiguration;
            Assembly strategyLocationDefensesAssembly = Assembly.LoadFile(gameConfiguration.StrategyLocationDefensesDllPath);
            e.Result = (IStrategyLocationDefenses)strategyLocationDefensesAssembly.CreateInstance("StrategyLocationDefenses.StrategyLocationDefenses");
        }

        public IStrategySelectionDefenses GetStrategySelectionDefeses()
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork_SelectionDefenses;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted_SelectionDefenses;
            backgroundWorker.RunWorkerAsync(gameConfiguration);

            Thread.Sleep(100);

            while (backgroundWorker.IsBusy)
            { }

            Thread.Sleep(100);

            return strategySelectionDefenses;
        }

        private void BackgroundWorker_RunWorkerCompleted_SelectionDefenses(object sender, RunWorkerCompletedEventArgs e)
        {
            strategySelectionDefenses = e.Result as IStrategySelectionDefenses;
        }

        private void BackgroundWorker_DoWork_SelectionDefenses(object sender, DoWorkEventArgs e)
        {
            GameConfiguration gameConfiguration = e.Argument as GameConfiguration;
            Assembly strategySelectionDefensesAssembly = Assembly.LoadFile(gameConfiguration.StrategySelectionDefensesDllPath);
            e.Result = (IStrategySelectionDefenses)strategySelectionDefensesAssembly.CreateInstance("StrategySelectionDefenses.StrategySelectionDefenses");
        }
    }
}
