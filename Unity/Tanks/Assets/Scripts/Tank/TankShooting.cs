using UnityEngine;
using UnityEngine.UI;

public class TankShooting : MonoBehaviour
{
    public int PlayerNumber = 1;       
    public Rigidbody Shell;            
    public Transform FireTransform;    
    public Slider AimSlider;           
    public AudioSource ShootingAudio;  
    public AudioClip ChargingClip;     
    public AudioClip FireClip;         
    public float MinLaunchForce = 15f; 
    public float MaxLaunchForce = 30f; 
    public float MaxChargeTime = 0.75f;

    
    private string FireButton;         
    private float CurrentLaunchForce;  
    private float ChargeSpeed;         
    private bool Fired;                


    private void OnEnable()
    {
        CurrentLaunchForce = MinLaunchForce;
        AimSlider.value = MinLaunchForce;
    }


    private void Start()
    {
        FireButton = "Fire" + PlayerNumber;

        ChargeSpeed = (MaxLaunchForce - MinLaunchForce) / MaxChargeTime;
    }
    

    private void Update()
    {
        // Track the current state of the fire button and make decisions based on the current launch force.
        AimSlider.value = MinLaunchForce;

        if(CurrentLaunchForce >= MaxLaunchForce && !Fired)
        {
            // Hemos llegado a la energía máxima
            CurrentLaunchForce = MaxLaunchForce;
            Fire();
        }
        else if(Input.GetButtonDown(FireButton))
        {
            // Hemos empezado a pulsar el disparo
            Fired = false;
            CurrentLaunchForce = MinLaunchForce;
            if(ShootingAudio.clip != ChargingClip)
                ShootingAudio.clip = ChargingClip;
            ShootingAudio.Play();
        }
        else if(Input.GetButton(FireButton) && !Fired)
        {
            // Seguimos pulsando el botón del disparo
            CurrentLaunchForce += ChargeSpeed * Time.deltaTime;
            AimSlider.value = CurrentLaunchForce;
        }
        else if(Input.GetButtonUp(FireButton) && !Fired)
        {
            // Hemos soltado el botón del disparo
            Fire();
        }
    }


    private void Fire()
    {
        // Instantiate and launch the shell.
        Fired = true;
        Rigidbody shellInstance = Instantiate(Shell, 
            FireTransform.position, FireTransform.rotation) as Rigidbody;

        shellInstance.velocity = FireTransform.forward * CurrentLaunchForce;
        ShootingAudio.clip = FireClip;
        ShootingAudio.Play();

        CurrentLaunchForce = MinLaunchForce;
    }
}