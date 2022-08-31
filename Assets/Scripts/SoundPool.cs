using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPool : MonoBehaviour {
    public static SoundPool current;
    public GameObject poolObject;
    public int poolAmount = 10;
    public bool willGrow = true;

    public List<GameObject> poolList;

    private void Awake() {
        current = this;
    }

    // Start is called before the first frame update
    void Start() {
        poolList = new List<GameObject>();
        for (int i = 0; i < poolAmount; i++) {
            GameObject obj = Instantiate(poolObject);
            obj.SetActive(false);
            poolList.Add(obj);
        }
    }

    public GameObject GetPoolObject() { 
        for (int i = 0; i < poolAmount; i++) {
            if (!poolList[i].activeInHierarchy) return poolList[i];
        }
        if (willGrow) { 
            GameObject obj = Instantiate(poolObject);
            poolList.Add(obj);
            obj.SetActive(false);
            return obj;
        }
        return null;
    }


}
