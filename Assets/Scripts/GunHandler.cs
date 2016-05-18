﻿using UnityEngine;
using System.Collections;

public class GunHandler : MonoBehaviour {

    public GameObject bullet;    //Bullet objects being sent
    public Transform bulletExit; //Exit location of the bullet, attached to the tip of the gun.
    public float bulletSpeed = 2000f;

    private float rotationPerSecond        = 30f;
    private float horizonalRotationalDepth = 50f;
    private float verticalRotationalDepth  = 20f;

    public float shotsPerSecond = 1f;

    private float maxLeftRotation  = 360;
    private float maxRightRotation = 0;
    private float maxUpRotation    = 360;
    private float maxDownRotation  = 0;

    private float shotDelay;
    private float lastShot; //Time since last shot

    void Start() {
        this.maxLeftRotation  -= horizonalRotationalDepth;
        this.maxRightRotation += horizonalRotationalDepth;
        this.maxUpRotation    -= verticalRotationalDepth;
        this.maxDownRotation  += verticalRotationalDepth;

        this.shotDelay = 1f / this.shotsPerSecond; //Miliseconds to wait before each shot
    }
	
	void Update () {
        lastShot += Time.deltaTime;

        //+1 is added so that rotation in the opposite direction is not locked.
        if (Input.GetKey(KeyCode.LeftArrow) && (transform.eulerAngles.y > maxLeftRotation || transform.eulerAngles.y < (maxRightRotation + 1)))
            transform.Rotate(-Vector3.up * rotationPerSecond * Time.deltaTime);
        else if (Input.GetKey(KeyCode.RightArrow) && (transform.eulerAngles.y > (maxLeftRotation - 1) || transform.eulerAngles.y < maxRightRotation))
            transform.Rotate(Vector3.up * rotationPerSecond * Time.deltaTime);
        //Break in if statements so players can move gun rotation diagonally.
        if (Input.GetKey(KeyCode.UpArrow) && (transform.eulerAngles.x > maxUpRotation || transform.eulerAngles.x < (maxDownRotation + 1))) {
            transform.Rotate(Vector3.left * rotationPerSecond * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.DownArrow) && (transform.eulerAngles.x > (maxUpRotation - 1) || transform.eulerAngles.x < maxDownRotation)) {
            transform.Rotate(-Vector3.left * rotationPerSecond * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.Space) && lastShot >= shotDelay) {
            GameObject clonedBullet = (GameObject) Instantiate(bullet, bulletExit.position, transform.rotation);
            Vector3 forceVector = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y, 0) * Vector3.forward;
            clonedBullet.GetComponent<Rigidbody>().AddForce(forceVector * bulletSpeed);
            lastShot = 0f;

            Destroy(clonedBullet, 3.0f);
        }
    }
    
}
