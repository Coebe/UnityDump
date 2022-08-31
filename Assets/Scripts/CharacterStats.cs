using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterStats : MonoBehaviour {
    [SerializeField]
    public float maxHp = 100;
    public float hp { get; private set; }
    public float atk = 10;

    int killScore = 0;

    //public int y {
    //    get {
    //        return x;
    //    }
    //    set {
    //        x = value;
    //    }
    //}

    private int x;


    private void Start() {
        hp = maxHp;
    }

    public void ChangeHp(float value) {
        hp = Mathf.Clamp(value + hp, 0, maxHp);
        var hpValue = hp / maxHp;
        if (transform.CompareTag("Enemy")) {
            // get the child component "HP"
            transform.Find("Canvas").GetChild(1).GetComponent<Image>().fillAmount = hpValue;
        } else if (transform.CompareTag("Player")) {
            var playerHealth = LevelManager.instance.mainCanvas.Find("PlayerHealth");
            playerHealth.Find("HP_Health").GetComponent<Image>().fillAmount = hp / maxHp;
            playerHealth.Find("HP_Health_Text").GetComponent<TextMeshProUGUI>().text = string.Format("{0:0.##} %", hpValue * 100);
        }

        Debug.Log(transform.CompareTag("Player") + ": " + hp + "/" + maxHp);
        if (hp <= 0) {
            Die();
        }
    }

    void Die() {
        if (transform.CompareTag("Player")) {
            LevelManager.instance.mainCanvas.Find("GameOverText").gameObject.SetActive(true);
            LevelManager.instance.player.GetComponent<PlayerControllerCS>().isDied = true;
            //Debug.Log("Player Die.");
        } else if (transform.CompareTag("Enemy")) {
            LevelManager.instance.score += killScore;
            if (++LevelManager.instance.curKill >= LevelManager.instance.killNumMax) 
                LevelManager.instance.mainCanvas.Find("WinText").gameObject.SetActive(true);
            Instantiate(LevelManager.instance.particleList[0], transform.position, transform.rotation);
            Destroy(transform.gameObject);
        }
    }
}
