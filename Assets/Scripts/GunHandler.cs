using UnityEngine;
using System.Collections;

public class GunHandler : MonoBehaviour {

    public GameObject bullet;

    public float rotationPerSecond = 30f;
    public float rotationalDepth   = 60f;

    private float maxLeftRotation  = 360;
    private float maxRightRotation = 0;

    void Start() {
        this.maxLeftRotation  -= rotationalDepth;
        this.maxRightRotation += rotationalDepth;
    }
	
	void Update () {
        //+1 is added so that rotation in the opposite direction is not locked.
        if (Input.GetKey(KeyCode.LeftArrow) && (transform.eulerAngles.y > maxLeftRotation || transform.eulerAngles.y < (maxRightRotation + 1)))
            transform.Rotate(-Vector3.up * rotationPerSecond * Time.deltaTime);
        else if (Input.GetKey(KeyCode.RightArrow) && (transform.eulerAngles.y > (maxLeftRotation - 1) || transform.eulerAngles.y < maxRightRotation))
            transform.Rotate(Vector3.up * rotationPerSecond * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space)) {
            GameObject clonedBullet = Instantiate(this.bullet);
            Rigidbody rb = clonedBullet.GetComponent<Rigidbody>();
        }
    }
    
}
