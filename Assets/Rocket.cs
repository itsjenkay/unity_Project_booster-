using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;

    new Rigidbody rigidbody;
    AudioSource audioSource;
    enum State { Alive , Dying , Transcending}

    State state = State.Alive;
	// Use this for initialization
	void Start () {

        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if (state == State.Alive)
        {
            Rotate();
            Thrust();
        }
       
       
    }
    void OnCollisionEnter (Collision collision)
    {
        if (state != State.Alive) { return; }// it ignore collision

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // this will not do anything
                break;
            case "Finish":
                // dude u made it congrats man

                state = State.Alive;
                Invoke("LoadNextLevel",1f);
                break;
            default:
                state = State.Dying;
                Invoke("LoadFirstLevel",1f);
                break;
        }
          
    }

    private  void LoadNextLevel()
    {
        SceneManager.LoadScene(1);
    }
    private  void LoadFirstLevel()
    {
        SceneManager.LoadScene(0);
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
