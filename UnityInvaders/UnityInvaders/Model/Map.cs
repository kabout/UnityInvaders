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
        private int marginBetweenObjects;
        private List<IObstacle> obstacles;
        private List<IDefense> defenses;
        private List<IAlien> aliens;
        private Dictionary<ObjectType, List<Position>> freePositions = new Dictionary<ObjectType, List<Position>>();
        private int defenseSize;

        #endregion

        #region Properties

        public IList<IObstacle> Obstacles { get { return obstacles; } }
        public IList<IDefense> Defenses { get { return defenses; } }
        public int Size { get; private set; }
        public IList<IAlien> Aliens { get { return aliens; } }
        public int Margin { get; private set; }

        #endregion

        #region Constructors

        public Map(int size)
        {
            Size = size;
            obstacles = new List<IObstacle>();
            defenses = new List<IDefense>();
            aliens = new List<IAlien>();
            freePositions.Add(ObjectType.Obstacle, new List<Position>());
            freePositions.Add(ObjectType.Defense, new List<Position>());
            freePositions.Add(ObjectType.Alien, new List<Position>());
            marginBetweenObjects = 2;
            defenseSize = Constants.DEFAULT_DEFENSE_RADIO * 2;

            InitMap();
        }

        #endregion

        #region Methods

        public bool AddObstacle(IObstacle obstacle)
        {
            if (!IsValidPosition(obstacle))
                return false;

            obstacles.Add(obstacle);
            
            int xEnd = obstacle.Position.X + (obstacle.Radius * 2);
            int yEnd = obstacle.Position.Y + (obstacle.Radius * 2);

            for (int x = obstacle.Position.X; x < xEnd; x++)
                for (int y = obstacle.Position.Y; y < yEnd; y++)
                    map[x, y] = 1;

            #region Update defense free positions
            
            xEnd = Math.Min(Size - Margin, xEnd + marginBetweenObjects);
            yEnd = Math.Min(Size - Margin, yEnd + marginBetweenObjects);

            // Eliminamos las posiciones que coinciden con el obstáculo
            // Eliminamos las posiciones por debajo que no se pueda poner defensas          
            // Eliminamos las posiciones de la izquierda que no se pueda poner defensas
            int initY = Math.Max(0, obstacle.Position.Y - defenseSize - marginBetweenObjects);
            int initX = Math.Max(0, obstacle.Position.X - defenseSize - marginBetweenObjects);
            freePositions[ObjectType.Defense].RemoveAll(p => p.Y >= initY && p.Y < yEnd && p.X >= initX && p.X < xEnd);

            // Si por arriba no hay espacio, eliminamos esas posiciones
            if (xEnd + defenseSize > Size - Margin)
                freePositions[ObjectType.Defense].RemoveAll(p => p.X >= xEnd && p.Y >= obstacle.Position.Y && p.Y < yEnd);

            // Si por el lado derecho no hay espacio, eliminamos esas posiciones
            if (yEnd + defenseSize > Size - Margin)
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

            int xEnd = defense.Position.X + defenseSize;
            int yEnd = defense.Position.Y + defenseSize;

            for (int x = defense.Position.X; x < xEnd; x++)
                for (int y = defense.Position.Y; y < yEnd; y++)
                    map[x, y] = 2;

            defenses.Add(defense);

            #region Update defense free positions

            xEnd = Math.Min(Size - Margin, xEnd + marginBetweenObjects);
            yEnd = Math.Min(Size - Margin, yEnd + marginBetweenObjects);

            // Eliminamos las posiciones que coinciden con la defensa
            // Eliminamos las posiciones por debajo que no se pueda poner defensas          
            // Eliminamos las posiciones de la izquierda que no se pueda poner defensas
            int initY = Math.Max(0, defense.Position.Y - defenseSize - marginBetweenObjects);
            int initX = Math.Max(0, defense.Position.X - defenseSize - marginBetweenObjects);
            freePositions[ObjectType.Defense].RemoveAll(p => p.Y >= initY && p.Y < yEnd && p.X >= initX && p.X < xEnd);

            // Si por arriba no hay espacio, eliminamos esas posiciones
            if (xEnd + defenseSize > Size - Margin)
                freePositions[ObjectType.Defense].RemoveAll(p => p.X >= xEnd && p.Y >= defense.Position.Y && p.Y < yEnd);

            // Si por el lado derecho no hay espacio, eliminamos esas posiciones
            if (yEnd + defenseSize > Size - Margin)
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

        public IList<Position> GetFreePositionsForObstacle (int radius)
        {
            int minWidth = Margin + radius;
            int minHeight = Margin + radius;
            int maxWidth = Size - Margin - (radius * 2);
            int maxHeight = Size - Margin - (radius * 2);

            List<Position> positions = new List<Position>(freePositions[ObjectType.Obstacle].Where(p => p.X >= minWidth && p.X < maxWidth && p.Y >= minHeight && p.Y < maxHeight));

            return positions;
        }

        public IList<Position> GetFreePositionsForDefense ()
        {
            return freePositions[ObjectType.Defense];
        }

        public IList<Position> GetFreePositionsForAlien ()
        {
            return freePositions[ObjectType.Alien];
        }

        #endregion

        #region Private Methods

        private void InitMap()
        {
            map = new int[Size, Size];
            Margin = Constants.ALIEN_SIZE + 10;

            for (int x = 0; x < Size; x++)
                for (int y = 0; y < Size; y++)
                {
                    if (x == 0 || y == 0 || y == (Size - 1) || x == (Size - 1))
                        freePositions[ObjectType.Alien].Add(new Position(x, y));

                    map[x, y] = 0;

                    if(x >= Margin && y >= Margin && x <= (Size - Margin) && y <= (Size - Margin))
                        freePositions[ObjectType.Obstacle].Add(new Position(x, y));

                    if (x >= Margin && y >= Margin && x <= (Size - Margin) && y <= (Size - Margin) &&
                       (x + defenseSize) <= Size && (y + defenseSize) <= Size)
                        freePositions[ObjectType.Defense].Add(new Position(x, y));
                }           
        }

        #endregion
    }
}
