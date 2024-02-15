using UnityEngine;

public class Evade : Flee
{
    [SerializeField] private float _maxPrediction;
    private GameObject _targetAux;
    private Agent _targetAgent;

    public override void Awake()
    {
        base.Awake();
        _targetAgent = Target.GetComponent<Agent>();
        _targetAux = Target;
        Target = new GameObject();
    }

    public override Steering GetSteering()
    {
        Vector3 direction = _targetAux.transform.position - transform.position;
        float distance = direction.magnitude;
        float speed = Agent.Velocity.magnitude;
        float prediction;

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