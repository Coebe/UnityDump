using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float rotateValue = 1f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem particleLeft;
    [SerializeField] ParticleSystem particleMain;
    [SerializeField] ParticleSystem particleRight;
    public float thrustSpeed = 1f;

    Rigidbody rg;
    AudioSource audioSource;
    BoxCollider collision;

    // Start is called before the first frame update
    void Start()
    {
        rg = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        collision = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotate();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else if (audioSource.isPlaying)
        {
            StopThrusting();
        }
    }

    void ProcessRotate()
    {
        Vector3 direction = Vector3.zero;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            StartRotateForward(ref direction);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            StartRotateBack(ref direction);
        }
        else
        {
            StopRotate();
        }

    }

    void StartThrusting()
    {
        // TODO: 火箭尾部粒子特效经常没有
        particleMain.Play();
        rg.AddRelativeForce(Vector3.up * Time.deltaTime * thrustSpeed);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
    }

    void StopThrusting()
    {
        audioSource.Stop();
        particleMain.Stop();
    }

    void StartRotateForward(ref Vector3 direction)
    {
        direction = Vector3.forward;
        ApplyRotate(ref direction);
        particleRight.Play();
    }

    void StartRotateBack(ref Vector3 direction)
    {
        direction = Vector3.back;
        ApplyRotate(ref direction);
        particleLeft.Play();
    }

    void StopRotate()
    {
        particleRight.Stop();
        particleLeft.Stop();
    }

    void ApplyRotate(ref Vector3 direction)
    {
        rg.freezeRotation = true;   // freeze rotation when key pressed 'A' or 'left arrow'
        transform.Rotate(direction * rotateValue * Time.deltaTime);
        rg.freezeRotation = false;  // unfressze rotation when key release 
    }

    public void Respawn()
    {

    }
}
