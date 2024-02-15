using UnityEngine;

public class Pursue : Seek
{
    [SerializeField] private float _maxPrediction;
    private GameObject _targetAux;
    private Agent _targetAgent;

    public override void Awake()
    {
        base.Awake(); //вызов базового метода из родительского класса
        _targetAgent = Target.GetComponent<Agent>();
        _targetAux = Target;
        Target = new GameObject();
    }

    public override Steering GetSteering()
    {
        // Вычисляем направление от текущей позиции до позиции новой цели
        Vector3 direction = _targetAux.transform.position - transform.position; 
        // Вычисляем расстояние между текущей позицией и позицией новой цели
        float distance = direction.magnitude;
        // Получаем скорость текущего агента
        float speed = Agent.Velocity.magnitude;
        float prediction;

        // prediction - минимальное расстояние до цели
        if (speed <= distance / _maxPrediction)
            prediction = _maxPrediction;
        else
            prediction = distance / speed;

        Target.transform.position = _targetAux.transform.position;
        Target.transform.position += _targetAgent.Velocity * prediction;
        return base.GetSteering();
    }

    private void OnDestroy()
    {
        Destroy(_targetAux);
    }
}
