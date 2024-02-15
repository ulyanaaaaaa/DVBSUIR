using UnityEngine;

public class Arrive : AgentBehaviour
{
    public float TargetRadius; //радиус остановки
    public float SlowRadius; //радиус замедления
    public float TimeToTarget = 0.1f;

    public override Steering GetSteering()
    {
        //вычисляем скорость в зависимости от расстояния до цели и радиуса замедления
        Steering steering = new Steering();
        Vector3 direction = Target.transform.position - transform.position;
        float distance = direction.magnitude;
        float targetSpeed;

        if (distance < TargetRadius)
            return steering;
        if (distance > SlowRadius)
            targetSpeed = Agent.MaxSpeed;
        else
            targetSpeed = Agent.MaxSpeed * distance / SlowRadius;
        
        //устанавливаются управляющие значения, а скорость ограничивается максимальным значением
        Vector3 desiredVelocity = direction.normalized;
        desiredVelocity *= targetSpeed;
        steering.Linear = desiredVelocity - Agent.Velocity;
        steering.Linear /= TimeToTarget;

        if (steering.Linear.magnitude > Agent.MaxAccel)
        {
            steering.Linear.Normalize();
            steering.Linear *= Agent.MaxAccel;
        }

        return steering;
    }
}
