using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameControl : MonoBehaviour {

    public static GameControl Instance { get; private set; }
    public GameObject pfBall;
    public float timeSpawn = 5;
    void Start () {
        Instance = this;
        score = 0;
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

    int _score;
    int score {
        get {
            return _score;
        }
        set {
            _score = value;
            txtScore.text = "Score " + _score;
        }
    }
    public void OnScore()
    {
        score = score + 1;
    }
    public Text txtScore;
    public void ResetGame()
    {
        score = 0;
    }
}
