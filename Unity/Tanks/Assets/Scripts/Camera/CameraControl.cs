using UnityEngine;

public class CameraControl : MonoBehaviour
{
    /// <summary>
    /// Tiempo que queremos que pase para que se hagan los cambios de posición o zoom
    /// </summary>
    public float DampTime = 0.2f;    
    /// Número de unidades de margen que vamos a tener para calcular el zoom de la camara      
    /// Para que los tanques no aparezcan cortados.
    public float ScreenEdgeBuffer = 4f;          
    /// Tamaño mínimo de la camara ortográfica 
    public float MinSize = 6.5f;                  
    [HideInInspector] public Transform[] Targets; 


    private Camera Camera;                        
    private float ZoomSpeed;                      
    private Vector3 MoveVelocity;      
    /// Media de las posiciones de los tanques activos           
    private Vector3 DesiredPosition;              


    private void Awake()
    {
        Camera = GetComponentInChildren<Camera>();
    }


    private void FixedUpdate()
    {
        Move();
        Zoom();
    }


    private void Move()
    {
        FindAveragePosition();
        
        /// Devuelve la posición transform.position avanzada un poco (DesiredPosition)
        transform.position = Vector3.SmoothDamp(transform.position, DesiredPosition, ref MoveVelocity, DampTime);
    }


    private void FindAveragePosition()
    {
        Vector3 averagePos = new Vector3();
        int numTargets = 0;

        foreach(Transform transformTarget in Targets)
        {
            if (!transformTarget.gameObject.activeSelf)
                continue;

            averagePos += transformTarget.position;
            numTargets++;
        }

        if (numTargets > 0)
            averagePos /= numTargets;

        averagePos.y = transform.position.y;

        DesiredPosition = averagePos;
    }


    private void Zoom()
    {
        float requiredSize = FindRequiredSize();
        Camera.orthographicSize = Mathf.SmoothDamp(Camera.orthographicSize, requiredSize, ref ZoomSpeed, DampTime);
    }


    private float FindRequiredSize()
    {
        Vector3 desiredLocalPos = transform.InverseTransformPoint(DesiredPosition);

        float size = 0f;

        foreach (Transform transformTarget in Targets)
        {
            if (!transformTarget.gameObject.activeSelf)
                continue;

            Vector3 targetLocalPos = transform.InverseTransformPoint(transformTarget.position);

            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

            size = Mathf.Max (size, Mathf.Abs (desiredPosToTarget.y));

            size = Mathf.Max (size, Mathf.Abs (desiredPosToTarget.x) / Camera.aspect);
        }
        
        size += ScreenEdgeBuffer;

        size = Mathf.Max(size, MinSize);

        return size;
    }


    public void SetStartPositionAndSize()
    {
        FindAveragePosition();

        transform.position = DesiredPosition;

        Camera.orthographicSize = FindRequiredSize();
    }
}