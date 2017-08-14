using System;
using UnityEngine;

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

            if (!Physics.Raycast(ray, out hit, LayerMask.GetMask("Defense")) || hit.collider == null)
            {
                UnSelected();
                UIInfoSelectObject.SetActive(false);
                return;
            }

            UIInfoSelectObject.SetActive(true);

            UnityDefense unityDefense = hit.collider.gameObject.GetComponent<UnityDefense>();

            if (unityDefense == null || (UnityDefense)selectedGameObject == unityDefense)
                return;

            if (selectedGameObject != null)
                selectedGameObject.Selected = false;

            Debug.Log("Selected" + unityDefense.Id);
            unityDefense.Selected = true;
            selectedGameObject = unityDefense;
            //UnityEngine.UI.Text UIInfoText = UIInfoSelectObject.GetComponentInChildren<UnityEngine.UI.Text>();
            //UIInfoText.text = unityDefense.ToString();
            //UIInfoSelectObject.SetActive(true);
            //foreach(GameObject alien in GameObject.FindGameObjectsWithTag("Alien"))
            //    alien.GetComponent<MoveAlien>().ChangeTarget(hit.collider.gameObject.transform);
        }

        void UnSelected()
        {
            if (selectedGameObject == null)
                return;
            
            selectedGameObject.Selected = false;
            selectedGameObject = null;

            //UIInfoSelectObject.SetActive(false);
        }
    }
}
