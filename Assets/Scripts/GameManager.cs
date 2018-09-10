using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager _Instance = null;

    private void Awake()
    {
        _Instance = this;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GenerateSoldier(string name,Vector3 spwanPos)
    {
        string resPath = "Heros/" + name;
        GameObject prefab = Resources.Load<GameObject>(resPath);
        GameObject soldier = Instantiate<GameObject>(prefab);
        soldier.transform.position = spwanPos;
    }

    public void GenerateZombie(string name, Vector3 spwanPos)
    {

    }
}
