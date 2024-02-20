using UnityEngine;

public class Agent : MonoBehaviour //ответственный за реализацию моделей интеллектуального перемещения
{
    public float MaxSpeed;
    public Vector3 Velocity;
    public float MaxAccel;
    private Steering _steering;
    public float Orientation;
    public float Rotation;
    public float MaxRotation;
    public float MaxAngularAccel;

    private void Start()
    {
        Velocity = Vector3.zero;
        _steering = new Steering();
    }

    public virtual void Update()
    {
        Vector3 diplacement = Velocity * Time.deltaTime; //смещение
        Orientation += Rotation * Time.deltaTime; //поворот

        if (Orientation < 0.0f)   //ограничение
            Orientation += 360.0f;
        
        else if (Orientation > 360.0f)
            Orientation -= 360.0f;
        
        transform.Translate(diplacement, Space.World); //перемещение
        transform.rotation = new Quaternion(); //обнуление
        transform.Rotate(Vector3.up, Orientation);
    }

    public virtual void LateUpdate() //подготавливает значения для следующего кадра
    {
        Velocity += (_steering.Linear * Time.deltaTime).normalized;
        Rotation += _steering.Angular * Time.deltaTime;

        if (Velocity.magnitude > MaxSpeed)
            Velocity *= MaxSpeed;

        if (_steering.Angular == 0.0f)
            Rotation = 0.0f;

        if (_steering.Linear.sqrMagnitude == 0.0f)
            Velocity = Vector3.zero;

        _steering = new Steering();
    }
    
    public float MapToRange(float rotation) //определяет направление вращения посредством вычисления разности двух направлений
    {
        rotation %= 360.0f;

        if (Mathf.Abs(rotation) > 180.0f)
        {
            if (rotation < 0.0f)
                rotation += 360.0f;
            else
                rotation -= 360.0f;
        }

        return rotation;
    }

    public void SetSteering(Steering steering)
    {
        _steering = steering;
    }
}
