﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip dead;
    [SerializeField] float levelLoadDelay = 2f;

    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem deadParticle;

    new Rigidbody rigidbody;
    AudioSource audioSource;
    ParticleSystem particle;

    enum State { Alive , Dying , Transcending}

    State state = State.Alive;
	// Use this for initialization
	void Start ()
    {

        rigidbody = GetComponent<Rigidbody>();
        particle = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (state == State.Alive)
        {
            RespondToRotateInput();
            RespondToThrustInput();
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
                StartSuccessSequence();
                break;
            default:
                StartDeadSequence();
                break;
        }
          
    }
    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticle.Play();
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    private void StartDeadSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(dead);
        deadParticle.Play();
        Invoke("LoadFirstLevel", levelLoadDelay);
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
    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space)) // for our thrusting and can thrust while rotating
        {
            ApplyingThrust();

            print("thrusting");
        }
        else
        {
            audioSource.Stop();
        }
    }

    private void ApplyingThrust()
    {
        rigidbody.AddRelativeForce(Vector3.up * mainThrust);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);

        }
        mainEngineParticle.Play();
    }

    //rotation code
    private void RespondToRotateInput()
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
