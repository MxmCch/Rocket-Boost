using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Tagger : MonoBehaviour
{
    //next level/restart delay
    float delayDuration = 2f;
    [SerializeField] AudioClip crashSound;
    [SerializeField] AudioClip finishSound;
    [SerializeField] ParticleSystem finishParticles;
    [SerializeField] ParticleSystem crashParticles;
    public TMP_Text scoreText;
    bool isTransitioning = false;
    bool cheatCollider = false;

    AudioSource audioSource;
    bool boxCollider = false;
    public int points = 0;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
        boxCollider = GetComponent<Collider>();
        scoreText = scoreText.GetComponent<TMP_Text>();
        points = 0;
        scoreText.text = points.ToString();
    }

    private void Update()
    {
        KeyDownCheats();
    }

    private void KeyDownCheats()
    {
        //next level cheat
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadLevel();
        }
        //invincible cheat
        if (Input.GetKeyDown(KeyCode.C))
        {
            cheatCollider = !cheatCollider;
            Debug.Log(cheatCollider);
        }
    }

    void OnCollisionEnter(Collision other) {
        if (isTransitioning || cheatCollider){return;}
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You just hit friendly object");
                break;
            case "Finish":
                Debug.Log("You just hit finish object");
                NextLevelSequence(delayDuration);
                break;
            case "Obstacle":
                Debug.Log("You just hit obstacle object");
                StartCrashSequence(delayDuration);
                break;
            default:
                Debug.Log("You just hit something wtf is that");
                break;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Point")
        {
            scoreText.text = points.ToString();
        }
    }

    void VFXenter(AudioClip soundClip, ParticleSystem effectName)
    {
        audioSource.Stop();
        audioSource.PlayOneShot(soundClip);
        effectName.Play();
    }

    void StartCrashSequence(float invokeDelay)
    {
        VFXenter(crashSound, crashParticles);
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel",2);
    }
    void NextLevelSequence(float invokeDelay)
    {
        Debug.Log("FD");
        VFXenter(finishSound, finishParticles);
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        Invoke("LoadLevel",2);
    }
    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    void LoadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex+1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
}
