using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public float speed = 3;    //In units per second. Varies from each lumberjack.
    public int damage  = 10;   //This applies every time a lumberjack hits a tree. Varies from each lumberjack.
    public int hp      = 100;  //Healthpoints. Varies from each lumberjack.

    private Vector3 translation;
    private Animation enemyAnimation;
    private bool moving = true;

    void Start () {
        this.translation = new Vector3(0, 0, this.speed);
        this.enemyAnimation = GetComponent<Animation>();
    }
	
	// Update is called once per frame
	void Update () {
        if (moving)
            this.transform.Translate(translation * Time.deltaTime);
	}

    void OnCollisionEnter (Collision col) {
        if (col.gameObject.CompareTag("Tree")) {
            if (moving) {
                moving = false;
                this.enemyAnimation.CrossFade("Lumbering");
            }
            //Destroy(col.gameObject);
            //Destroy(this.gameObject);
        }
    }

}
