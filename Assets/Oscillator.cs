using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]

public class Oscillator : MonoBehaviour {

    [SerializeField] Vector3 movementVector = new Vector3(10f,0f,0f);
    [SerializeField] float period = 2f;

    [Range(0,1)] [SerializeField] float movementFactor;

    Vector3 startingPosition;

	// Use this for initialization
	void Start ()
    {
        startingPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // peiod this reduces the rate of the game engine, 
        float cycle = Time.time / period;
        // this give the exact value of a complet circle 6.24 which is a complete circle
        const float tau = Mathf.PI * 2f;
        //thuis value always return btwn -1 and 1
        float rawSineWave =Mathf.Sin( cycle * tau);

        movementFactor = rawSineWave / 2f + 0.5f;
        Vector3 offset = movementVector * movementFactor; // this is a manual way of making oscillation 
        transform.position = offset + startingPosition; // this is also the same
    }
}
