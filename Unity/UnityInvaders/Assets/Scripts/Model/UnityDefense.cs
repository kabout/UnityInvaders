using UnityEngine;
using System.Collections;
using UnityInvaders.Interfaces;
using UnityInvaders.Model;

public class UnityDefense : MonoBehaviour
{
    #region Fields

    public IDefense defense;
    private float maxHealth = 100f;
    public GameObject healthBar;
    float maxBarX;

    #endregion

    #region Properties

    public int Damage { get { return defense.Damage; } }

    public int Range { get { return defense.Range; } }

    public int Health { get { return defense.Health; } }

    public int Radius
    {
        get { return defense.Radius; }
    }

    public Position Position
    {
        get { return new Position((int)gameObject.transform.localPosition.x, (int)gameObject.transform.localPosition.z); }
    }

    public int Cost { get { return defense.Cost; } }

    public int Dispersion { get { return defense.Dispersion; } }

    public float AttacksPerSecond { get { return defense.AttacksPerSecond; } }

    public int Type { get { return defense.Type; } }

    #endregion

    #region Unity Methods

    void Awake ()
    {
        GUIElement[] elements = GetComponentsInChildren<GUIElement>();

        foreach (GUIElement element in elements)
        {
            if (element.name == "Bar")
            {
                healthBar = element.gameObject;
                break;
            }
        }
    }

    // Use this for initialization
    void Start ()
    {
        maxHealth = Health;
        maxBarX = healthBar.transform.localScale.x;
        InvokeRepeating("DecreaseHealth", 1f, 1f);
    }
    void DecreaseHealth ()
    {
        defense.TakeDamage(2);
        SetHealthBar(Health);
    }

    public void SetHealthBar (float myHealth)
    {
        healthBar.transform.localScale = new Vector3((myHealth * maxBarX) / maxHealth,
            healthBar.transform.localScale.y,
            healthBar.transform.localScale.z);
    }

    #endregion

    #region Methods

    public void ChangePosition (Position position)
    {
        gameObject.transform.localScale += new Vector3(position.X, 0, position.Y);
    }

    public void TakeDamage (int damage)
    {
        defense.TakeDamage(damage);
    }

    public bool IsAlive ()
    {
        return defense.IsAlive();
    }

    #endregion
}
