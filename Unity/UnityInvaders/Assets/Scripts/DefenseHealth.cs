using UnityEngine;
using System.Collections;

public class DefenseHealth : MonoBehaviour
{

    public float maxHealth = 100f;    
    public float Health = 0f;
    public GameObject healthBar;
    float maxBarX;

	// Use this for initialization
	void Start ()
    {
        Health = maxHealth;
        maxBarX = healthBar.transform.localScale.x;
        InvokeRepeating("DecreaseHealth", 1f, 1f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void DecreaseHealth()
    {
        Health -= 2f;
        SetHealthBar(Health);
    }

    public void SetHealthBar(float myHealth)
    {
        healthBar.transform.localScale = new Vector3((myHealth * maxBarX) / maxHealth, 
            healthBar.transform.localScale.y, 
            healthBar.transform.localScale.z);
    }
}
