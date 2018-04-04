using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    public static GameControl Instance { get; private set; }
    public GameObject pfBall;
    public float timeSpawn = 5;
    public Text txtBegin;
    public Text txtEnd;
    public Text txtDistance;
    void Start () {
        Instance = this;
        SpawnBall();
    }
	
	void SpawnBall()
    {
        GameObject _obj = Instantiate(pfBall, transform);
        _obj.SetActive(true);
	}
    public void OnThown()
    {
        Invoke("SpawnBall", timeSpawn);
    }
}
