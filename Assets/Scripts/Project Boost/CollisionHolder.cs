using UnityEngine;
using UnityEngine.SceneManagement;

public delegate void SequenceDelegate();

public class CollisionHolder : MonoBehaviour
{
    static public CollisionHolder instance;
    [SerializeField] float loadDelay = 1f;
    [SerializeField] AudioClip crash;
    [SerializeField] AudioClip finish;
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem finishParticle;

    AudioSource audioSource;

    bool isTransationing = false;
    bool collisionEnabled = true;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        instance = this;
    }

    private void Update()
    {
        RespondDebugKeys();
    }

    void RespondDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            CollisionHolder.instance.NextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionEnabled = !collisionEnabled;
        }
    }

    static void DoSequence(bool tag, SequenceDelegate sequenceDelegate)
    {
        if (tag) sequenceDelegate();
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransationing || !collisionEnabled) return;
        audioSource.Stop();
        // 将方法绑定(+=)到委托, 等到委托触发时会一次调用绑定的方法
        // 将方法解除绑定(-=)
        // SequenceDelegate delegate1 = new SequenceDelegate(StartCrashSequence);
        // delegate1 += StartSuccessSequence;
        DoSequence(other.gameObject.CompareTag("Respawn"), StartCrashSequence);
        DoSequence(other.gameObject.CompareTag("Finish"), StartSuccessSequence);
        // 使用委托(Delegate)替代 switch
        // switch (other.gameObject.tag)
        // {
        //     case "Friendly":
        //         Debug.Log("you have touched the friendly tag object~~");
        //         break;
        //     case "Fuel":
        //         Debug.Log("come on!");
        //         break;
        //     case "Respawn":
        //         StartCrashSequence();
        //         break;
        //     case "Finish":
        //         StartSuccessSequence();
        //         break;
        //     default:
        //         Debug.Log("here is~~ other thing");
        //         break;
        // }
    }

    void StartSuccessSequence()
    {
        Debug.Log("StartSuccessSequence");
        isTransationing = true;
        audioSource.PlayOneShot(finish);
        // TODO: add partical upon crash BUT build and run no problem
        finishParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", loadDelay);
    }

    void StartCrashSequence()
    {
        Debug.Log("StartCrashSequence");
        isTransationing = true;
        audioSource.PlayOneShot(crash);
        crashParticle.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", loadDelay);
    }
    public void ReloadLevel()
    {
        // SceneManager.LoadScene(levelName);
        // this way of statement is more easily to read
        int currentActiveLevel = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentActiveLevel);
    }

    public void NextLevel()
    {
        int nextLevel = SceneManager.GetActiveScene().buildIndex + 1;
        nextLevel = nextLevel < SceneManager.sceneCountInBuildSettings ? nextLevel : 0;
        SceneManager.LoadScene(nextLevel);
    }
}
