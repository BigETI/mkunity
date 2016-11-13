using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SnowmanAIController : MonoBehaviour {

    private Transform t;
    private Rigidbody rb;
    private Vector3 mp;
    private bool stopped = true;
    private Dictionary<int, GameObject> ground_stack = new Dictionary<int, GameObject>();
    private float polar;
    private float elevation;
    private bool free_cam = false;
    private float free_elevation = 0.0f;
    private float free_polar = 0.0f;

    protected bool is_ball = false;
    
    public float speed = 10.0f;
    public float jumpForce = 5.0f;

    public bool freeCam
    {
        get
        {
            return free_cam;
        }
        set
        {
            if (free_cam != value)
            {
                free_cam = value;
                free_elevation = 0.0f;
                free_polar = 180.0f;
            }
        }
    }

    public bool isOnGround
    {
        get
        {
            return (ground_stack.Count > 0);
        }
    }

    public void move(float f, float s)
    {
        mp = (t.forward * f) + (t.right * s);
        stopped = false;
        t.localPosition += mp.normalized * (speed * Time.fixedDeltaTime);
    }

    public void stop()
    {
        if (!stopped)
        {
            rb.velocity = new Vector3(0.0f, rb.velocity.y, 0.0f);
            stopped = true;
        }
    }

    public void push(Transform trans, float f, float s)
    {
        mp = (trans.forward * f) + (trans.right * s);
        stopped = false;
        rb.AddForce(mp.normalized * (speed * Time.fixedDeltaTime), ForceMode.VelocityChange);
    }

    public void rotateAround(Transform cam_pivot, float polar, float elevation)
    {
        if (free_cam)
        {
            free_polar += polar;
            free_elevation += elevation;
            if (free_elevation < -90.0f)
                free_elevation = -90.0f;
            else if (free_elevation > 90.0f)
                free_elevation = 90.0f;
            while (free_polar < 0.0f)
                free_polar += 360.0f;
            while (free_polar > 360.0f)
                free_polar -= 360.0f;
            cam_pivot.localRotation = Quaternion.AngleAxis(free_polar, Vector3.up);
        }
        else
        {
            this.polar += polar;
            this.elevation += elevation;
            if (this.elevation < -90.0f)
                this.elevation = -90.0f;
            else if (this.elevation > 90.0f)
                this.elevation = 90.0f;
            while (this.polar < 0.0f)
                this.polar += 360.0f;
            while (this.polar > 360.0f)
                this.polar -= 360.0f;
            Quaternion r = Quaternion.AngleAxis(this.polar, Vector3.up);
            if (!is_ball)
                t.localRotation = r;
            r *= Quaternion.AngleAxis(this.elevation, Vector3.right);
            cam_pivot.rotation = r;
        }
    }

    public void jump()
    {
        if (isOnGround)
            rb.AddForce(0.0f, jumpForce, 0.0f, ForceMode.VelocityChange);
    }

    public void bounceUp(float force)
    {
        rb.AddForce(0.0f, force, 0.0f, ForceMode.Impulse);
    }
    
    public void Start () {
        t = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }
	
	public void FixedUpdate() {
	    
	}

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag.Equals("Ground"))
        {
            GameObject go = collision.collider.gameObject;
            if (go != null)
                ground_stack.Add(go.GetInstanceID(), go);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag.Equals("Ground"))
        {
            GameObject go = collision.collider.gameObject;
            if (go != null)
                ground_stack.Remove(go.GetInstanceID());
        }
    }
}
