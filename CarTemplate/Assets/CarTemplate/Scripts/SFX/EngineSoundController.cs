using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CarTemplate;

public class EngineSoundController : MonoBehaviour
{

    public Car car;
    public float minPitch = 1.0f;
    public float maxPitch = 3.0f;
    private AudioSource audioSource;

    private float maxRPM;

    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        maxRPM = car.engine.data.maxRpm;
    }

    // Update is called once per frame
    void Update()
    {

        float engineRatio = car.engine.EngineRpm / maxRPM;
        audioSource.pitch = Mathf.Lerp(minPitch, maxPitch, engineRatio);

    }
}
