using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public GameObject normalLumberjack; //Normal lumberjack
    public GameObject giantLumberjack;  //Giant lumberjack, slow walk
    public GameObject smallLumberjack;  //Small lumberjack, quick walk

    public Text treeHpText;
    public Text waveText;
    public Text enemiesLeftText;
    public GameObject winningPanel;
    public GameObject losingPanel;

    private int waveNumber = 0;
    private float waveTimer = 0;
    private int lastSpawn = 2;

    private const float maxTreeHp = 1000; //Tree must survive all waves with 1000 starting hp
    private float treeHp = 1000;

    private List<EnemyScript> activeLumberjacks = new List<EnemyScript>();
    private List<GameObject> toSpawn = new List<GameObject>();

    //Use these locations/rotations if lumberjack is to be spawned on left side of screen.
    private Vector3 leftSpawnPoint = new Vector3(-25f, 0f, 0f);
    private Quaternion leftSpawnRotation = Quaternion.Euler(0, 90, 0);

    //Use these locations/rotations if lumberjack is to be spawned on right side of screen.
    private Vector3 rightSpawnPoint = new Vector3(25f, 0f, 0f);
    private Quaternion rightSpawnRotation = Quaternion.Euler(0, 270, 0);

    private Color blueSkyColor = new Color(75 / 255f, 151 / 255f, 204 / 255f);
    private Color brownSkyColor = new Color(83 / 255f, 64 / 255f, 50 / 255f);
    private Color redSkyColor = new Color(255 / 255f, 64 / 255f, 64 / 255f);

    private AudioSource chopSound;
    private AudioSource whackSound;

    void Start () {
        treeHp = maxTreeHp;
        waveNumber = 0;
        winningPanel.SetActive(false);
        losingPanel.SetActive(false);

        chopSound = GetComponents<AudioSource>()[0];
        whackSound = GetComponents<AudioSource>()[1];
    }
	
	void Update () {
		if (Input.GetKey ("escape")) {
			// exit on escape
			SceneManager.UnloadScene (SceneManager.GetActiveScene().name);
			SceneManager.LoadScene ("MainMenu");
		}

        waveTimer += Time.deltaTime; //Adds to total time in the wave

        for (int i=0; i<activeLumberjacks.Count; i++) {
            EnemyScript es = activeLumberjacks[i];

            if (es.dead) {
                whackSound.Play();
                activeLumberjacks.RemoveAt(i);
                i--;
            } else if (es.CanApplyDamage()) {
                chopSound.Play();
                treeHp -= es.damage;
                es.ResetDamageTimer();
            }
        }

        float treeHpPercent = treeHp / maxTreeHp;
        float remainingPercent = 1 - treeHpPercent;
        treeHpText.text = ((int) (treeHpPercent * 100)) + "%";
        RenderSettings.fogDensity = 0.1f * remainingPercent;
        Color skyColor = new Color(blueSkyColor.r + ((redSkyColor.r - blueSkyColor.r) * remainingPercent),
            blueSkyColor.g + ((redSkyColor.g - blueSkyColor.g) * remainingPercent),
            blueSkyColor.b + ((redSkyColor.b - blueSkyColor.b) * remainingPercent));
        Camera.main.backgroundColor = skyColor;

        if (treeHp <= 0) {
            //You lost the game ;(
            DestroyAllLumberjacks();
            losingPanel.SetActive(true);
            treeHp = 0;
        } else if (activeLumberjacks.Count == 0 && toSpawn.Count == 0) {
            if (waveNumber == 10) {
                //You won! :D
                DestroyAllLumberjacks();
                winningPanel.SetActive(true);
            } else {
                //Need to spawn another wave
                PopulateWave(++waveNumber);
            }
        }

        if (toSpawn.Count > 0 && (int) waveTimer > lastSpawn) {
            lastSpawn = (int) waveTimer;
            SpawnLumberjack();
        }

        enemiesLeftText.text = (activeLumberjacks.Count + toSpawn.Count).ToString();
    }

    private void PopulateWave(int waveNumber) {
        waveText.text = waveNumber.ToString() + "/10";
        enemiesLeftText.text = waveNumber.ToString();

        if (waveNumber >= 1) toSpawn.Add(normalLumberjack);
        if (waveNumber >= 2) toSpawn.Add(smallLumberjack);
        if (waveNumber >= 3) toSpawn.Add(giantLumberjack);

        //Each wave >= 3 should have 1 of each lumberjack, the rest will be randomly generated.

        for (int i=toSpawn.Count; i < waveNumber; i++) {
            int numb = Random.Range(1, 11);

            if (numb <= 4) toSpawn.Add(normalLumberjack); //1-4 = 40%
            else if (numb <= 8) toSpawn.Add(smallLumberjack); //5-8 = 40%
            else if (numb <= 10) toSpawn.Add(giantLumberjack); //8-10 = 20%
        }

        //Wave should be populated now.
        waveTimer = 0;
        lastSpawn = 2;
    }

    private void SpawnLumberjack() {
        GameObject lumberjack = toSpawn[0];

        int random = Random.Range(1, 3);

        //50% chance enemy gets spawned on left, 50% chance enemy gets spawned on right
        if (random == 1) lumberjack = (GameObject) Instantiate(lumberjack, leftSpawnPoint, leftSpawnRotation);
        else lumberjack = (GameObject) Instantiate(lumberjack, rightSpawnPoint, rightSpawnRotation);

        activeLumberjacks.Add(lumberjack.GetComponent<EnemyScript>());
        toSpawn.RemoveAt(0);
    }

    private void DestroyAllLumberjacks()
    {
        toSpawn.Clear();
        foreach (EnemyScript es in activeLumberjacks)
        {
            Destroy(es.gameObject);
        }
    }

}
