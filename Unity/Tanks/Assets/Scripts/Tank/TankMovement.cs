using UnityEngine;

public class TankMovement : MonoBehaviour
{
    public int PlayerNumber = 1;
    /// Velocidad en unidades por segundo
    public float Speed = 12f;
    /// Grado de giro por segundo         
    public float TurnSpeed = 180f;
    /// <summary>
    /// Audio Source que controla el sonido del motor
    /// </summary>
    public AudioSource MovementAudio;
    /// Audio clips posibles 
    public AudioClip EngineIdling;       
    public AudioClip EngineDriving;
    /// Rango en el que varia el sonido del motor   
    public float PitchRange = 0.2f;

    /// <summary>
    /// El nombre del eje que controla el movimiento del tanque
    /// Se configuran en InputManager -> Axes
    /// </summary>
    private string MovementAxisName;
    /// <summary>
    /// El nombre del eje de giro del tanque
    /// Se configuran en InputManager -> Axes
    /// </summary>
    private string TurnAxisName; 
    /// <summary>
    /// RigidBody del tanque
    /// Se utiliza para mover el tanque sobre el terreno.
    /// </summary>
    private Rigidbody Rigidbody;
    /// <summary>
    /// El valor que se esté pulsando en ese eje para mover (-1, 0, 1)
    /// </summary>    
    private float MovementInputValue;
    /// <summary>
    /// El valor que se esté pulsando en ese eje para girar (-1, 0, 1)
    /// </summary>    
    private float TurnInputValue;
    /// <summary>
    /// Copia del tono inicial del AudioSource
    /// </summary>           
    private float OriginalPitch;         

    /// <summary>
    /// Referencia a los componentes
    /// </summary>
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Se ejecuta cuando el objeto se activa en la escena
    /// </summary>
    private void OnEnable ()
    {
        // Esto implica que puede ser afectado por colisiones, fuerza y demas. Activa el RigidBody
        Rigidbody.isKinematic = false;
        // Valores de movimiento a cero
        MovementInputValue = 0f;
        TurnInputValue = 0f;
    }

    /// <summary>
    /// Se ejecuta cuando el objeto se desactiva en la escena
    /// </summary>
    private void OnDisable ()
    {
        // Desactiva el RidigBody
        Rigidbody.isKinematic = true;
    }

    /// <summary>
    /// Inicializamos los valores de axis y el tono inicial
    /// </summary>
    private void Start()
    {
        MovementAxisName = "Vertical" + PlayerNumber;
        TurnAxisName = "Horizontal" + PlayerNumber;

        OriginalPitch = MovementAudio.pitch;
    }
    

    private void Update()
    {
        // Store the player's input and make sure the audio for the engine is playing.
        MovementInputValue = Input.GetAxis(MovementAxisName);
        TurnInputValue = Input.GetAxis(TurnAxisName);

        EngineAudio();
    }


    private void EngineAudio()
    {
        // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.
        // Si no estoy pulsando ninguna tecla
        if(Mathf.Abs(MovementInputValue) < 0.1f && Mathf.Abs(TurnInputValue) < 0.1f)            
            ChangeAudio(EngineIdling);
        else
            ChangeAudio(EngineDriving);
    }

    // Esto es para que en cada fotograma no cambie el sonido
    private void ChangeAudio(AudioClip audioClip)
    {
        if (MovementAudio.clip != audioClip)
        {
            MovementAudio.clip = audioClip;
            MovementAudio.pitch = Random.Range(OriginalPitch - PitchRange, OriginalPitch + PitchRange);
            MovementAudio.Play();
        }
    }

    /// <summary>
    /// Se ejecuta en cada paso del motor de fisico
    /// </summary>
    private void FixedUpdate()
    {
        // Move and turn the tank.
        Move();
        Turn();
    }


    private void Move()
    {
        // Adjust the position of the tank based on the player's input.
        Vector3 movement = transform.forward * MovementInputValue * Speed * Time.deltaTime;
        Rigidbody.MovePosition(Rigidbody.position + movement);
    }


    private void Turn()
    {
        // Adjust the rotation of the tank based on the player's input.
        float turn = TurnInputValue * TurnSpeed * Time.deltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        Rigidbody.MoveRotation(Rigidbody.rotation * turnRotation);
    }
}