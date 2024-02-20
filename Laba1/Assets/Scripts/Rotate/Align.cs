using UnityEngine;

public class Align : AgentBehaviour
{
    public float TargetRadius; //радиус цели
    public float SlowRadius; //радиус замедлиения
    public float TimeToTarget = 0.1f;

    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        float targetOrientation = Target.GetComponent<Agent>().Orientation; //угол
        float rotation = targetOrientation - Agent.Orientation; //разница в углах
        rotation = Agent.MapToRange(rotation); // преобразование разницы в угол в пределах от -180 до 180 градусов
        float rotationSize = Mathf.Abs(rotation); // вычисление абсолютноого значение угла поворота

        if (rotationSize < TargetRadius)
            return steering;
        
        float targetRotation;
        
        if (rotationSize > SlowRadius)
            targetRotation = Agent.MaxRotation;
        else 
            targetRotation = Agent.MaxRotation * rotationSize / SlowRadius;

        targetRotation *= rotation / rotationSize; // Распределяем угловой поворот в соответствии с направлением
        steering.Angular = targetRotation - Agent.Rotation; // Вычисляем требуемое угловое ускорение для достижения целевого поворота
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
