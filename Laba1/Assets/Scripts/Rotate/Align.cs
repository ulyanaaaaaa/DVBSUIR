using UnityEngine;

public class Align : AgentBehaviour
{
    public float TargetRadius;
    public float SlowRadius;
    public float TimeToTarget = 0.1f;

    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        float targetOrientation = Target.GetComponent<Agent>().Orientation;
        float rotation = targetOrientation - Agent.Orientation;
        rotation = Agent.MapToRange(rotation);
        float rotationSize = Mathf.Abs(rotation);

        if (rotationSize < TargetRadius)
            return steering;
        
        float targetRotation;
        
        if (rotationSize > SlowRadius)
            targetRotation = Agent.MaxRotation;
        else 
            targetRotation = Agent.MaxRotation * rotationSize / SlowRadius;

        targetRotation *= rotation / rotationSize;
        steering.Angular = targetRotation - Agent.Rotation;
        steering.Angular /= TimeToTarget;
        float angularAccel = Mathf.Abs(steering.Angular);

        if (angularAccel > Agent.MaxAngularAccel)
        {
            steering.Angular /= angularAccel;
            steering.Angular *= Agent.MaxAngularAccel;
        }

        return steering;
    }
}
