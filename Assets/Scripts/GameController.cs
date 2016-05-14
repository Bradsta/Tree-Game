using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    private int waveNumber = 1;  //Starting on first wave
    private int treeNumber = 10; //10 trees are in the game initially

    private Vector3 leftSpawnPoint  = new Vector3(-20f, 0.5f, 0f); //Need to rotate lumberjack 90 degrees in y-axis if spawned here.
    private Vector3 rightSpawnPoint = new Vector3(20f, 0.5f, 0f);  //Need to rotate lumberjack 270 degrees in y-axis if spawned here.

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

}
