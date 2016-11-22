using System;
using System.Text;
using UnityEngine;
using UnityInvaders.Interfaces;

namespace UnityInvaders.Model
{
    public class UnityDefense : MonoBehaviour, IDefense, ISelectable
    {
        #region Fields

        public GameObject Torreta;
        public Transform Target;

        public bool selected;
        public GameObject healthBar;
        public GameObject destructionEffect;
        public GameObject Bullet;
        public GameObject BulletPos;

        public int id;
        public float damage;
        public int range;
        public float health;
        public int cost;
        public int dispersion;
        public int type;
        public float shottingInterval = 1f;

        private float shootSpeed = 50;
        private float nextShoot = 0;
        private bool died = false;
        private float maxHealth;
        private float maxBarX;
        private GameObject defenseFloor;

        #endregion

        #region Properties

        public int Id { get { return id; } }

        public float Damage { get { return damage; } }

        public int Range { get { return range; } }

        public float Health { get { return health; } }

        public float Radius { get { return transform.localScale.x / 2; } }

        public int Cost { get { return cost; } }

        public int Dispersion { get { return dispersion; } }

        public int Type { get { return type; } }

        public bool Selected
        {
            get { return selected; }

            set
            {
                selected = value;

                if(defenseFloor)
                    defenseFloor.SetActive(value);
            }
        }

        public Vector3 Position
        {
            get
            {
                return transform.position;
            }
        }

        #endregion

        #region Unity Methods

        void Awake()
        {
            defenseFloor = this.transform.Find("DefenseFloor").gameObject;
        }

        // Use this for initialization
        void Start()
        {
            maxHealth = Health;
            maxBarX = healthBar.transform.localScale.x;
            selected = false;
            //InvokeRepeating("DecreaseHealth", 1f, 1f);
        }

        void Update()
        {
            if (Target)
                Shoot();
            else
                Torreta.transform.localRotation = Quaternion.identity;
        }

        void DecreaseHealth()
        {
            if (!died)
                TakeDamage(10);
        }

        public void Shoot()
        {
            Torreta.transform.LookAt(Target);

            if (nextShoot >= Time.time)
                return;

            nextShoot = Time.time + shottingInterval;

            GameObject bullet = (GameObject)Instantiate(Bullet, BulletPos.transform.position, BulletPos.transform.rotation);
            var bulletController = bullet.GetComponent<BulletController>();
            bulletController.Damage = Damage;
            bulletController.Dispersion = Dispersion;
            //Le damos velocidad a la bala 
            bullet.GetComponent<Rigidbody>().velocity = BulletPos.transform.TransformDirection(new Vector3(0, 0, shootSpeed));
        }

        public void SetHealthBar(float myHealth)
        {
            healthBar.transform.localScale = new Vector3((myHealth * maxBarX) / maxHealth,
                healthBar.transform.localScale.y,
                healthBar.transform.localScale.z);
        }

        #endregion

        #region Methods

        public void TakeDamage(float damage)
        {
            if (damage > Health)
            {
                health = 0;
                died = true;
                ParticleSystem particleSystem = ((GameObject)Instantiate(destructionEffect, transform.position, Quaternion.identity)).GetComponent<ParticleSystem>();
                particleSystem.Play();
                Destroy(gameObject);
            }
            else
                health -= damage;

            SetHealthBar(Health);
        }

        public bool IsAlive()
        {
            return !died;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine(string.Format("Identifier: {0}", Id));
            sb.AppendLine(string.Format("Health: {0}", Health));
            sb.AppendLine(string.Format("Damage: {0}", Damage));
            sb.AppendLine(string.Format("Dispersion: {0}", Dispersion));
            sb.AppendLine(string.Format("Range: {0}", Range));
            sb.AppendLine(string.Format("Cost: {0}", Cost));

            return sb.ToString();
        }

        public void ChangePosition(Vector3 position)
        {
            transform.position = position;
        }

        #endregion
    }
}
