using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager instance;
    public Transform playerTransform;
    public GameObject player;
    public Transform mainCanvas;
    public int score = 0;
    public int killNumMax;
    public int curKill;
    public int item = 0;
    public AudioClip[] audioList;
    public GameObject[] particleList;

    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update() {

    }

    public void PlaySoundAtLoc(AudioClip clip, Vector3 loc) {
        GameObject obj = SoundPool.current.GetPoolObject();
        AudioSource audio = obj.GetComponent<AudioSource>();

        obj.transform.position = loc;
        obj.SetActive(true);
        audio.PlayOneShot(clip);
        StartCoroutine(FadeSound(audio));
    }

    IEnumerator FadeSound(AudioSource audio) {
        while (audio.isPlaying)
            yield return new WaitForSeconds(0.5f);
        audio.gameObject.SetActive(false);
    }
}
