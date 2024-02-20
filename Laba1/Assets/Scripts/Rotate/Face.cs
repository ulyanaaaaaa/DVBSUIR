using UnityEngine;

public class Face : Align
{
    // Дополнительная цель, используемая для вычисления ориентации 
    protected GameObject targetAux;

    public override void Awake()
    {
        base.Awake();
        // Присваиваем целевой дополнительной переменной и создаем новый GameObject вместо оригинальной цели
        targetAux = Target;
        Target = new GameObject();
        Target.AddComponent<Agent>();
    }

    public override Steering GetSteering()
    {
        // Вычисляем направление к дополнительной цели
        Vector3 direction = targetAux.transform.position - transform.position;
        // Если направление ненулевое, вычисляем угол ориентации и присваиваем его целевой цели
        if (direction.magnitude > 0.0f)
        {
            float targetRotation = Mathf.Atan2(direction.x, direction.z);
            targetRotation *= Mathf.Rad2Deg;
            Target.GetComponent<Agent>().Orientation = targetRotation;
        }

        return base.GetSteering();
    }

    private void OnDestroy()
    {
        Destroy(Target);
    }
}
