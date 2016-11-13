using UnityEngine;
using System.Collections;

public class BouncyScript : MonoBehaviour {

    private AudioSource a;

	// Use this for initialization
	void Start () {
        a = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collider)
    {
        SnowmanAIController saic = collider.GetComponent<SnowmanAIController>();
        if (saic != null)
        {
            saic.bounceUp(10.0f);
            a.Play();
        }
    }
}
