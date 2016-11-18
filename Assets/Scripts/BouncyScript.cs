using UnityEngine;
using System.Collections;

public class BouncyScript : MonoBehaviour {

    private AudioSource a;

    public float bounciness = 10.0f;

	// Use this for initialization
	void Start () {
        a = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider collider)
    {
        SnowmanAIController saic = collider.GetComponent<SnowmanAIController>();
        if (saic != null)
        {
            saic.bounceUp(bounciness);
            a.Play();
        }
    }
}
