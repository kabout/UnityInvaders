using UnityEngine;
using System.Collections;

namespace UnityInvaders.Interfaces
{
    public interface IEntity
    {
        /// <summary>
        /// Identificador del objeto
        /// </summary>
        int Id { get; }
        /// <summary>
        /// Radio del objeto
        /// </summary>
        int Radius { get; }
    }
}
