using UnityEngine;

public class Leave : AgentBehaviour
{
    public float EscapeRadius;
    public float DangerRadius;
    public float TimeToTarget = 0.1f;
    
    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        Vector3 direction = transform.position - Target.transform.position;
        float distance = direction.magnitude;

        if (distance > DangerRadius)
            return steering;
        
        float reduce;

        if (distance > DangerRadius)
            reduce = 0f;
        else
            reduce = distance / DangerRadius * Agent.MaxSpeed;
        
        float targetSpeed = Agent.MaxSpeed - reduce;
        
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
