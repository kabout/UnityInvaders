using UnityEngine;
using UnityInvaders.Interfaces;
using UnityInvaders.Managers;
using UnityStandardAssets.Cameras;

namespace UnityInvaders.Model
{
    public class UnityDefense : MonoBehaviour, IDefense, ISelectable
    {
        #region Fields

        public bool selected;
        public GameObject healthBar;
        public GameObject destructionEffect;
        public SelectionManager selectionManager;

        public int id;
        public float damage;
        public int range;
        public int health;
        public int radius;
        public int cost;
        public int dispersion;
        public float attackPerSecond;
        public int type;

        private bool died = false;
        private float maxHealth;
        private float maxBarX;
        private GameObject defenseFloor;

        #endregion

        #region Properties

        public int Id { get { return id; } }

        public float Damage { get { return damage; } }

        public int Range { get { return range; } }

        public int Health { get { return health; } }

        public int Radius { get { return radius; } }

        public int Cost { get { return cost; } }

        public int Dispersion { get { return dispersion; } }

        public float AttacksPerSecond { get { return attackPerSecond; } }

        public int Type { get { return type; } }

        public bool Selected
        {
            get { return selected; }

            set
            {
                selected = value;
                defenseFloor.SetActive(value);

                if(selected)
                    selectionManager.SelectGameObject(this);
            }
        }

        #endregion

        #region Unity Methods

        void Awake()
        {
            defenseFloor = this.transform.Find("DefenseFloor").gameObject;
        }

        // Use this for initialization
        void Start ()
        {
            maxHealth = Health;
            maxBarX = healthBar.transform.localScale.x;
            selected = false;
            //InvokeRepeating("DecreaseHealth", 1f, 1f);
        }

        void Update ()
        {
            //if (Input.GetMouseButtonDown(0))
            //    Selected = true;
        }

        void OnMouseDown()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
                if (hit.collider != null && hit.collider.gameObject.GetComponent<UnityDefense>().Id == this.Id)
                {
                    Debug.Log("Selected" + this.Id);
                    Selected = true;
                   // Camera.main.GetComponent<TargetFieldOfView>().SetTarget(transform);
                }
        }

        void DecreaseHealth ()
        {
            if(!died)
                TakeDamage(10);
        }

        public void SetHealthBar (float myHealth)
        {
            healthBar.transform.localScale = new Vector3((myHealth * maxBarX) / maxHealth,
                healthBar.transform.localScale.y,
                healthBar.transform.localScale.z);
        }

        #endregion

        #region Methods

        public void TakeDamage (int damage)
        {
            if (damage > Health)
            {
                health = 0;
                died = true;
                ParticleSystem particleSystem = ((GameObject)Instantiate(destructionEffect, transform)).GetComponent<ParticleSystem>();
                particleSystem.Play();
                Destroy(gameObject);
            }
            else
                health -= damage;

            SetHealthBar(Health);
        }

        public bool IsAlive ()
        {
            return !died;
        }

        #endregion
    }
}
