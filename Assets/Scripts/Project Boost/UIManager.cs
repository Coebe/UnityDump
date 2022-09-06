using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject textMeshPro;

    string defaultValue = "";
    TextMeshProUGUI levelIndex;
    // Start is called before the first frame update
    void Start()
    {
        levelIndex = textMeshPro.GetComponent<TextMeshProUGUI>();
        defaultValue = levelIndex.text;
    }

    // Update is called once per frame
    void Update()
    {
        int curLevelIndex = SceneManager.GetActiveScene().buildIndex;
        levelIndex.text = curLevelIndex.ToString();
    }
}
