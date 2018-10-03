using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour {
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    new Rigidbody rigidbody;
     AudioSource audioSource;

	// Use this for initialization
	void Start () {

        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Rotate();
        Thrust();
       
    }
    // thrusting code
    private void Thrust()
    {
        if (Input.GetKey(KeyCode.Space)) // for our thrusting and can thrust while rotating
        {
            rigidbody.AddRelativeForce(Vector3.up *mainThrust);
            if (!audioSource.isPlaying)
            {
                audioSource.Play();

            }

            print("thrusting");
        }
        else
        {
            audioSource.Stop();
        }
    }
    //rotation code
    private void Rotate()
    {
        
        float rotationThisFrame = rcsThrust * Time.deltaTime;
        rigidbody.freezeRotation = true;// take manual control of the rotation
        if (Input.GetKey(KeyCode.A))
        {

            transform.Rotate(Vector3.forward*rotationThisFrame);
            print("Rotating left");
        }
        else if (Input.GetKey(KeyCode.D))

        {

            transform.Rotate(-(Vector3.forward * rotationThisFrame));
            print("Rotating right");

        }
        rigidbody.freezeRotation = false;// unfrez the manual control 
    }

   
}
