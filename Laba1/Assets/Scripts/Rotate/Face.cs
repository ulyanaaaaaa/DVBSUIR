using UnityEngine;

public class Face : Align
{
    protected GameObject targetAux;

    public override void Awake()
    {
        base.Awake();
        targetAux = Target;
        Target = new GameObject();
        Target.AddComponent<Agent>();
    }

    public override Steering GetSteering()
    {
        Vector3 direction = targetAux.transform.position - transform.position;

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
