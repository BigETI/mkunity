using UnityEngine;
using System.Collections;

public class SnowmanPlayerController : SnowmanAIController {
    
    public Transform cameraPivot;
    public MouseConfiguration mouse;
    
    new void Start () {
        base.Start();
	}
	
	new void FixedUpdate() {
        base.FixedUpdate();
        float s = Input.GetAxis("Horizontal"), f = Input.GetAxis("Vertical");
        freeCam = Input.GetButton("Fire2");
        rotateAround(cameraPivot, Input.GetAxis("Mouse X") * mouse.horizontalSensibility, -Input.GetAxis("Mouse Y") * mouse.verticalSensibility);
        if ((s >= 0.1f) || (s <= -0.1f) || (f >= 0.1f) || (f <= -0.1f))
            move(f, s);
        else
            stop();
        if (Input.GetButtonDown("Jump"))
            jump();
    }
}
