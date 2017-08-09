using UnityEngine;
using System.Text;
using Assets.Scripts.Utils;

public class UnityAlien : MonoBehaviour, IAlien
{
    #region Fields

    public bool selected;
    public Transform Target;
    public GameObject HealthBar;
    public GameObject DestructionEffect;
    public GameObject Bullet;
    public GameObject BulletPos;
    public float damage;
    public int range;
    public float health;
    public int cost;
    public int id;
    public float shottingInterval = 1000f;
    private float shootSpeed = 40;
    private float nextShoot = 0;

    private bool died = false;
    private float maxHealth;
    private float maxBarX;

    #endregion

    #region Properties

    public float Damage { get { return damage; } }

    public int Range { get { return range; } }

    public float Health { get { return health; } }    

    public int Width
    {
        get { return (int)transform.localScale.x; }
    }

    public int Height
    {
        get { return (int)transform.localScale.z; }
    }

    public IPosition Position
    {
        get { return ConvertPosition.Convert(transform.position); }
    }

    public int Cost
    {
        get
        {
            return cost;
        }
    }

    public int Id
    {
        get
        {
            return id;
        }
    }

    public float Radius
    {
        get
        {
            return transform.localScale.x / 2;
        }
    }

    #endregion

    #region Methods



    void Awake()
    {
    }

    // Use this for initialization
    void Start()
    {
        maxHealth = Health;
        maxBarX = HealthBar.transform.localScale.x;
    }

    public void SetHealthBar(float myHealth)
    {
        HealthBar.transform.localScale = new Vector3((myHealth * maxBarX) / maxHealth,
            HealthBar.transform.localScale.y,
            HealthBar.transform.localScale.z);
    }

    void FixedUpdate()
    {
        if (Target)
            Shoot();
    }

    public void Shoot()
    {
        if (nextShoot >= Time.time)
            return;

        GameObject bullet = (GameObject)Instantiate(Bullet, BulletPos.transform.position, BulletPos.transform.rotation);
        var bulletController = bullet.GetComponent<BulletController>();
        bulletController.Damage = Damage;
        bulletController.Dispersion = 0;
        bulletController.Owner = gameObject;
        //Le damos velocidad a la bala 
        bullet.GetComponent<Rigidbody>().velocity = BulletPos.transform.TransformDirection(new Vector3(0, 0, shootSpeed));

        nextShoot = Time.time + shottingInterval;
    }

    void DecreaseHealth()
    {
        if (!died)
            TakeDamage(10);
    }

    #endregion

    #region Methods

    public void TakeDamage(float damage)
    {
        lock(this)
        {
            if (damage > Health)
            {
                health = 0;
                died = true;
                ParticleSystem particleSystem = ((GameObject)Instantiate(DestructionEffect, transform.position, Quaternion.identity)).GetComponent<ParticleSystem>();
                particleSystem.Play();
                Destroy(gameObject);

                GameStatistics gameStatistics = GameObject.FindObjectOfType<GameStatistics>();

                if(gameStatistics != null)
                    gameStatistics.AddAlien(Id, Position.X, Position.Y, Time.time);

                Debug.Log(string.Format("Alien {0} in position ({1},{2}) died!", Id, Position.X, Position.Z));
            }
            else
                health -= damage;

            SetHealthBar(Health);
        }
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
        sb.AppendLine(string.Format("Range: {0}", Range));
        sb.AppendLine(string.Format("Cost: {0}", Cost));

        return sb.ToString();
    }

    public void ChangePosition(IPosition position)
    {
        transform.localPosition = ConvertPosition.Convert(position);
    }

    #endregion
}
