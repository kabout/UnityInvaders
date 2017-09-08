using Assets.Scripts.Utils;
using System.Text;
using UnityEngine;

public class UnityDefense : MonoBehaviour, IDefense, ISelectable
{
    #region Fields

    public GameObject Torreta;
    public Transform Target;

    public bool selected;
    public GameObject HealthBar;
    public GameObject DestructionEffect;
    public GameObject Bullet;
    public GameObject BulletPos;

    public int id;
    public float damage;
    public int range;
    public float health;
    public int cost;
    public int dispersion;
    public float shottingInterval = 10f;

    private float shootSpeed = 40;
    private float nextShoot = 0;
    private bool died = false;
    private float maxHealth;
    private float maxBarX;
    private GameObject defenseFloor;
    private AudioSource DiedSound;

    #endregion

    #region Properties

    public int Id { get { return id; } }

    public float Damage { get { return damage; } }

    public int Range { get { return range; } }

    public float Health { get { return health; } }

    public float Radius { get { return transform.localScale.x / 2; } }

    public int Cost { get { return cost; } }

    public int Dispersion { get { return dispersion; } }

    public bool Selected
    {
        get { return selected; }

        set
        {
            selected = value;

            if (defenseFloor)
                defenseFloor.SetActive(value);
        }
    }

    public IPosition Position
    {
        get
        {
            return ConvertPosition.Convert(transform.position);
        }
    }

    #endregion

    #region Unity Methods

    void Awake()
    {
        defenseFloor = this.transform.Find("DefenseFloor").gameObject;
        DiedSound = this.gameObject.GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {
        maxHealth = Health;
        maxBarX = HealthBar.transform.localScale.x;
        selected = false;
        //InvokeRepeating("DecreaseHealth", 1f, 1f);
    }

    void FixedUpdate()
    {
        if (Target)
            Shoot();
        else
            Torreta.transform.localRotation = Quaternion.identity;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Alien"))
            Destroy(collision.gameObject);
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

        GameObject bullet = (GameObject)Instantiate(Bullet, BulletPos.transform.position, BulletPos.transform.rotation);
        var bulletController = bullet.GetComponent<BulletController>();
        bulletController.Damage = Damage;
        bulletController.Dispersion = Dispersion;
        bulletController.Owner = gameObject;
        //Le damos velocidad a la bala 
        bullet.GetComponent<Rigidbody>().velocity = BulletPos.transform.TransformDirection(new Vector3(0, 0, shootSpeed));

        nextShoot = Time.time + shottingInterval;
    }

    public void SetHealthBar(float myHealth)
    {
        HealthBar.transform.localScale = new Vector3((myHealth * maxBarX) / maxHealth,
            HealthBar.transform.localScale.y,
            HealthBar.transform.localScale.z);
    }

    #endregion

    #region Methods

    public void TakeDamage(float damage)
    {
        lock(this)
        {
            if (damage > Health)
            {
                died = true;
                health = 0;
                ParticleSystem particleSystem = ((GameObject)Instantiate(DestructionEffect, transform.position, Quaternion.identity)).GetComponent<ParticleSystem>();
                particleSystem.Play();
                DiedSound.Play();

                Destroy(gameObject, 1);

                GameStatistics gameStatistics = GameObject.FindObjectOfType<GameStatistics>();

                if (gameStatistics != null)
                    gameStatistics.AddDefense(Id, Position.X, Position.Z, Time.time);

                Debug.Log(string.Format("Defense {0} in position ({1:0.00},{2:0.00}) died!", Id, Position.X, Position.Z));
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
        sb.AppendLine(string.Format("Dispersion: {0}", Dispersion));
        sb.AppendLine(string.Format("Range: {0}", Range));
        sb.AppendLine(string.Format("Cost: {0}", Cost));

        return sb.ToString();
    }

    public void ChangePosition(IPosition position)
    {
        transform.position = ConvertPosition.Convert(position);
    }

    #endregion
}
