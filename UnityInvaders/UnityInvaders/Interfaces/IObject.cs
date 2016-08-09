using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityInvaders.Model;

namespace UnityInvaders.Interfaces
{
    public interface IEntity
    {
        /// <summary>
        /// Identificador del objeto
        /// </summary>
        int Id { get; }
        /// <summary>
        /// Posición del objeto
        /// </summary>
        Position Position { get; }
        /// <summary>
        /// Radio del objeto
        /// </summary>
        int Radius { get; }
    }
}
