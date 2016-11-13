using UnityEngine;
using System.Collections;

public class SnowballController : SnowmanAIController {

    public Transform cameraPivot;
    public MouseConfiguration mouse;

    private Transform t;

    new void Start()
    {
        base.Start();
        t = GetComponent<Transform>();
        is_ball = true;
    }

    new void FixedUpdate()
    {
        base.FixedUpdate();
        float s = Input.GetAxis("Horizontal"), f = Input.GetAxis("Vertical");
        //freeCam = Input.GetButton("Fire2");
        rotateAround(cameraPivot, Input.GetAxis("Mouse X") * mouse.horizontalSensibility, -Input.GetAxis("Mouse Y") * mouse.verticalSensibility);
        if ((s >= 0.1f) || (s <= -0.1f) || (f >= 0.1f) || (f <= -0.1f))
            push(cameraPivot, f, s);
        if (Input.GetButtonDown("Jump"))
            jump();
    }

    void Update()
    {
        cameraPivot.localPosition = new Vector3(t.localPosition.x, t.localPosition.y, t.localPosition.z);
    }
}
