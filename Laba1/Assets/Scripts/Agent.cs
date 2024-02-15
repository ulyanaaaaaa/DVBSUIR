using UnityEngine;

public class Agent : MonoBehaviour
{
    public float MaxSpeed;
    public Vector3 Velocity;
    public float MaxAccel;
    private Steering _steering;
    private float _orientation;
    private float _rotation;
    
    
    private void Start()
    {
        Velocity = Vector3.zero;
        _steering = new Steering();
    }

    public virtual void Update()
    {
        Vector3 diplacement = Velocity * Time.deltaTime; //смещение
        _orientation += _rotation * Time.deltaTime; //поворот

        if (_orientation < 0.0f)
            _orientation += 360.0f;
        
        else if (_orientation > 360.0f)
            _orientation -= 360.0f;
        
        transform.Translate(diplacement, Space.World); //перемещение
        transform.rotation = new Quaternion(); //обнуление
        transform.Rotate(Vector3.up, _orientation);
    }

    public virtual void LateUpdate()
    {
        Velocity += (_steering.Linear * Time.deltaTime).normalized;
        _rotation += _steering.Angular * Time.deltaTime;

        if (Velocity.magnitude > MaxSpeed)
            Velocity *= MaxSpeed;

        if (_steering.Angular == 0.0f)
            _rotation = 0.0f;

        if (_steering.Linear.sqrMagnitude == 0.0f)
            Velocity = Vector3.zero;

        _steering = new Steering();
    }

    public void SetSteering(Steering steering)
    {
        _steering = steering;
    }
}
