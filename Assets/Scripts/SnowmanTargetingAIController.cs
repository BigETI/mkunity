using UnityEngine;
using System.Collections;

public class SnowmanTargetingAIController : SnowmanAIController {

    private NavMeshAgent nma;

    public Transform target;

    new void Start () {
        base.Start();
        nma = GetComponent<NavMeshAgent>();
    }
	
	new void FixedUpdate() {
        base.FixedUpdate();
        nma.SetDestination(target.position);
    }
}
