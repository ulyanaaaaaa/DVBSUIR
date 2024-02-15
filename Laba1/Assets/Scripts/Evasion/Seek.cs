using UnityEngine;

public class Seek : AgentBehaviour
{
    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        steering.Linear = (Target.transform.position - transform.position).normalized; //направление к цели
        steering.Linear *= Agent.MaxAccel; //задаем максимальное ускорение
        return steering;
    }
}
