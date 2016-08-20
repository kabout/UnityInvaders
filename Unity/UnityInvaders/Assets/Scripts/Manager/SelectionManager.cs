using System;
using UnityEngine;
using UnityInvaders.Interfaces;
using UnityInvaders.Model;

namespace UnityInvaders.Managers
{
    public class SelectionManager : MonoBehaviour
    {
        private ISelectable selectedGameObject;
        public GameObject UIInfoSelectObject;

        // Use this for initialization
        void Start ()
        {
            selectedGameObject = null;
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
                ManageSelection();
        }

        private void ManageSelection()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (!Physics.Raycast(ray, out hit) || hit.collider == null)
            {
                UnSelected();
                return;
            }

            UnityDefense unityDefense = hit.collider.gameObject.GetComponent<UnityDefense>();

            if (unityDefense == null || (UnityDefense)selectedGameObject == unityDefense)
                return;

            if (selectedGameObject != null)
                selectedGameObject.Selected = false;

            Debug.Log("Selected" + unityDefense.Id);
            unityDefense.Selected = true;
            selectedGameObject = unityDefense;
            UnityEngine.UI.Text UIInfoText = UIInfoSelectObject.GetComponentInChildren<UnityEngine.UI.Text>();
            UIInfoText.text = unityDefense.ToString();
            UIInfoSelectObject.SetActive(true);
        }

        void UnSelected()
        {
            if (selectedGameObject == null)
                return;
            
            selectedGameObject.Selected = false;
            selectedGameObject = null;

            UIInfoSelectObject.SetActive(false);
        }
    }
}
