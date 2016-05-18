using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public GameObject normalLumberjack; //Normal lumberjack
    public GameObject giantLumberjack;  //Giant lumberjack, slow walk
    public GameObject smallLumberjack;  //Small lumberjack, quick walk

    private int waveNumber = 1;  //Starting on first wave
    private float treeHp = 1000;   //Tree must survive all waves with 1000 starting hp

    private List<EnemyScript> lumberjacks = new List<EnemyScript>();

    //Use these locations/rotations if lumberjack is to be spawned on left side of screen.
    private Vector3 leftSpawnPoint = new Vector3(-25f, 0f, 0f);
    private Quaternion leftSpawnRotation = Quaternion.Euler(0, 90, 0);

    //Use these locations/rotations if lumberjack is to be spawned on right side of screen.
    private Vector3 rightSpawnPoint = new Vector3(25f, 0f, 0f);
    private Quaternion rightSpawnRotation = Quaternion.Euler(0, 270, 0);

    void Start () {
        SpawnLumberjacks();
	}
	
	void Update () {
	    foreach(EnemyScript es in lumberjacks) {
            if (es.CanApplyDamage()) {
                treeHp -= es.damage;
                es.ResetDamageTimer();
            }
        }

        Debug.Log(treeHp);
    }

    void SpawnLumberjacks() {
        lumberjacks.Add(((GameObject) Instantiate(smallLumberjack, leftSpawnPoint, leftSpawnRotation)).GetComponent<EnemyScript>());
    }

}
