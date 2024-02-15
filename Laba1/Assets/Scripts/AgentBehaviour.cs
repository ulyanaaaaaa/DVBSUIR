using UnityEngine;

[RequireComponent(typeof(Agent))]

public class AgentBehaviour : MonoBehaviour
{
    public GameObject Target;
    protected Agent Agent;

    public virtual void Awake()
    {
        Agent = GetComponent<Agent>();
    }

    public virtual void Update()
    {
        Agent.SetSteering(GetSteering());
    }

    public virtual Steering GetSteering()
    {
        return new Steering();
    }
}
