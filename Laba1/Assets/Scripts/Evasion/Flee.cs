using UnityEngine;

public class Flee : AgentBehaviour
{
    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        steering.Linear = (transform.position - Target.transform.position).normalized; //направление от цели
        steering.Linear *= Agent.MaxAccel;
        return steering;
    }
}
