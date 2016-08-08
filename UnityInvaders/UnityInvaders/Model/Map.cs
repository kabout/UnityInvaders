using System;
using System.Collections.Generic;
using System.Linq;
using UnityInvaders.Interfaces;

namespace UnityInvaders.Model
{
    public class Map : IMap
    {
        #region Enums

        private enum ObjectType
        {
            Obstacle = 0,
            Defense = 1,
            Alien = 2
        }

        #endregion

        #region Fields

        private int[,] map;
        private List<IObstacle> obstacles;
        private List<IDefense> defenses;
        private List<IAlien> aliens;
        private Dictionary<ObjectType, List<Position>> freePositions = new Dictionary<ObjectType, List<Position>>();

        #endregion

        #region Properties

        public IList<IObstacle> Obstacles { get { return obstacles; } }
        public IList<IDefense> Defenses { get { return defenses; } }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public IList<IAlien> Aliens { get { return aliens; } }

        #endregion

        #region Constructors

        public Map(int width, int height)
        {
            Width = width;
            Height = height;
            obstacles = new List<IObstacle>();
            defenses = new List<IDefense>();
            aliens = new List<IAlien>();
            freePositions.Add(ObjectType.Obstacle, new List<Position>());
            freePositions.Add(ObjectType.Defense, new List<Position>());
            freePositions.Add(ObjectType.Alien, new List<Position>());

            InitMap(width, height);
        }

        #endregion

        #region Methods

        public bool AddObstacle(IObstacle obstacle)
        {
            if (!IsValidPosition(obstacle))
                return false;

            obstacles.Add(obstacle);
            
            int xEnd = obstacle.Position.X + obstacle.Width;
            int yEnd = obstacle.Position.Y + obstacle.Height;

            for (int x = obstacle.Position.X; x < xEnd; x++)
                for (int y = obstacle.Position.Y; y < yEnd; y++)
                    map[x, y] = 1;

            #region Update defense free positions

            freePositions[ObjectType.Defense].RemoveAll(p => p.X >= obstacle.Position.X && p.X < xEnd && p.Y >= obstacle.Position.Y && p.Y < yEnd);
            
            // Si por debajo no hay espacio, eliminamos esas posiciones
            if (obstacle.Position.X - Constants.DEFENSE_SIZE < 0)
                freePositions[ObjectType.Defense].RemoveAll(p => p.X <= obstacle.Position.X && p.Y >= obstacle.Position.Y && p.Y < yEnd);

            // Si por el lado izquierdo no hay espacio, eliminamos esas posiciones
            if (obstacle.Position.Y - Constants.DEFENSE_SIZE < 0)
                freePositions[ObjectType.Defense].RemoveAll(p => p.Y <= obstacle.Position.Y && p.X >= obstacle.Position.X && p.X < xEnd);

            // Si por arriba no hay espacio, eliminamos esas posiciones
            if (xEnd + Constants.DEFENSE_SIZE > Width)
                freePositions[ObjectType.Defense].RemoveAll(p => p.X >= xEnd && p.Y >= obstacle.Position.Y && p.Y < yEnd);

            // Si por el lado derecho no hay espacio, eliminamos esas posiciones
            if (yEnd + Constants.DEFENSE_SIZE > Height)
                freePositions[ObjectType.Defense].RemoveAll(p => p.Y >= yEnd && p.X >= obstacle.Position.X && p.X < xEnd);

            #endregion

            #region Update obstacle free positions

            freePositions[ObjectType.Obstacle].RemoveAll(p => p.X == obstacle.Position.X && p.Y == obstacle.Position.Y);

            #endregion

            return true;
        }

        public bool AddDefense(IDefense defense)
        {
            if (!IsValidPosition(defense))
                return false;

            int xEnd = defense.Position.X + defense.Width;
            int yEnd = defense.Position.Y + defense.Height;

            for (int x = defense.Position.X; x < xEnd; x++)
                for (int y = defense.Position.Y; y < yEnd; y++)
                    map[x, y] = 2;

            defenses.Add(defense);

            #region Update defense free positions

            // Eliminamos las posiciones que coinciden con la defensa
            freePositions[ObjectType.Defense].RemoveAll(p => p.X >= defense.Position.X && p.X < xEnd && p.Y >= defense.Position.Y && p.Y < yEnd);

            // Si por debajo no hay espacio, eliminamos esas posiciones
            if (defense.Position.X - Constants.DEFENSE_SIZE < 0)
                freePositions[ObjectType.Defense].RemoveAll(p => p.X <= defense.Position.X && p.Y >= defense.Position.Y && p.Y < yEnd);

            // Si por el lado izquierdo no hay espacio, eliminamos esas posiciones
            if(defense.Position.Y - Constants.DEFENSE_SIZE < 0)
                freePositions[ObjectType.Defense].RemoveAll(p => p.Y <= defense.Position.Y && p.X >= defense.Position.X && p.X < xEnd);

            // Si por arriba no hay espacio, eliminamos esas posiciones
            if(xEnd + Constants.DEFENSE_SIZE > Width)
                freePositions[ObjectType.Defense].RemoveAll(p => p.X >= xEnd && p.Y >= defense.Position.Y && p.Y < yEnd);

            // Si por el lado derecho no hay espacio, eliminamos esas posiciones
            if (yEnd + Constants.DEFENSE_SIZE > Height)
                freePositions[ObjectType.Defense].RemoveAll(p => p.Y >= yEnd && p.X >= defense.Position.X && p.X < xEnd);


            #endregion

            #region Update obstacle free positions

            // Eliminamos las posiciones de Objecto que coinciden con la defensa
            // Realmente esto se podría ahorrar ya que no se van a poner obstáculos después de las defensas.
            freePositions[ObjectType.Obstacle].RemoveAll(p => p.X >= defense.Position.X && p.X < xEnd && p.Y >= defense.Position.Y && p.Y < yEnd);

            #endregion

            return true;
        }

        public bool AddAlien(IAlien alien)
        {
            return false;
        }

        public bool IsValidPosition(IDefense defense)
        {
            return freePositions[ObjectType.Defense].Exists(p => p.X == defense.Position.X && p.Y == defense.Position.Y);
        }

        public bool IsValidPosition(IObstacle obstacle)
        {
            return freePositions[ObjectType.Obstacle].Exists(p => p.X == obstacle.Position.X && p.Y == obstacle.Position.Y);
        }

        public IList<Position> GetFreePositionsForObstacle (int width, int height)
        {
            int maxWidth = Width - width;
            int maxHeight = Height - height;

            List<Position> positions = new List<Position>(freePositions[ObjectType.Obstacle].Where(x => x.X < maxWidth && x.Y < maxHeight));

            return positions;
        }

        public IList<Position> GetFreePositionsForDefense ()
        {
            return freePositions[ObjectType.Defense];
        }

        public IList<Position> GetFreePositionsForAlien ()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Private Methods

        private void InitMap(int width, int height)
        {
            map = new int[width, height];
            int margin = Constants.ALIEN_SIZE + 10;

            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                {
                    if (x == 0 || y == 0 || y == (height - 1) || x == (width - 1))
                        freePositions[ObjectType.Alien].Add(new Position(x, y));

                    map[x, y] = 0;

                    if(x >= margin && y >= margin && x <= (width - margin) && y <= (height - margin))
                        freePositions[ObjectType.Obstacle].Add(new Position(x, y));

                    if (x >= margin && y >= margin && x <= (width - margin) && y <= (height - margin) &&
                       (x + Constants.DEFENSE_SIZE) <= width && (y + Constants.DEFENSE_SIZE) <= height)
                        freePositions[ObjectType.Defense].Add(new Position(x, y));
                }           
        }

        #endregion
    }
}
