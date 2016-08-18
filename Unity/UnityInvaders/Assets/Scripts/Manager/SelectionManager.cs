using UnityEngine;
using UnityInvaders.Interfaces;

namespace UnityInvaders.Managers
{
    public class SelectionManager : MonoBehaviour
    {
        private ISelectable selectedGameObject;

        // Use this for initialization
        void Start ()
        {
            selectedGameObject = null;
        }

        public void SelectGameObject (ISelectable gameObject)
        {
            if (selectedGameObject == gameObject)
                return;

            if (selectedGameObject != null)
                selectedGameObject.Selected = false;

            selectedGameObject = gameObject;
        }
    }
}
