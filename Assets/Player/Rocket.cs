using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{

    [Range(0, 500)] [SerializeField] float rcsThrust = 100f;
    [Range(0, 2000)] [SerializeField] float mainThrust = 1000f;
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    bool timeUp = false;
    LevelTime levelTime;
    Rigidbody rb;
    AudioSource audioSource;

    public enum State { Alive, Transcending, Dying }
    public State state = State.Alive;
    bool collisionsDisabled = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        levelTime = FindObjectOfType<LevelTime>();
    }


    void Update()
    {
        if (state == State.Alive && !TimeUp())
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }

        if (TimeUp()) StartTimeUpSequence();

        if (Debug.isDebugBuild) RespondToDebugKeys();
    }

    
    private void RespondToDebugKeys() 
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        } else if (Input.GetKeyDown(KeyCode.C)) 
        {
            collisionsDisabled = !collisionsDisabled;
        }
    }

    private bool TimeUp()
    {
        if (levelTime.GetSecondsLeft() <= 0) return true;
        else return false;
    }
    
    private void RespondToRotateInput()
    {
        rb.freezeRotation = true;
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rb.freezeRotation = false;
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
            audioSource.PlayOneShot(mainEngine);

        mainEngineParticles.Play();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive || collisionsDisabled) return; // just one hit then die ignoring collisions

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        DisappearWhenDead();
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        Invoke("LoadCurrentLevel", death.length);
    }

    private void StartTimeUpSequence()
    {
        if(state != State.Alive) return;
        state = State.Dying;
        DisappearWhenDead();
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        deathParticles.Play();
        Invoke("LoadCurrentLevel", death.length);
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay);
    }

    void DisappearWhenDead()
    {
        DisableRenderers();
        DisableLights();
    }

    private void DisableLights()
    {
        Light[] lights = GetComponentsInChildren<Light>();

        foreach (var light in lights)
        {
            light.enabled = false;
        }
    }

    private void DisableRenderers()
    {
        MeshRenderer[] renderers = GetComponentsInChildren<MeshRenderer>();

        foreach (MeshRenderer renderer in renderers)
        {
            renderer.enabled = false;
        }
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            // TODO ADD FINAL SCENE
        }

    }

    private void LoadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}