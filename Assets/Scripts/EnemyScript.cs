using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public float speed = 3;    //In units per second. Varies from each lumberjack.
    public float damage  = 10;   //This applies every time a lumberjack hits a tree. Varies from each lumberjack.
    public float hp      = 100;  //Healthpoints. Varies from each lumberjack.

    public bool dead = false; //Will be deleted from GameController if true.

    private Vector3 translation;
    private Animation enemyAnimation;
    private AudioSource whackSound;

    private bool moving = true;

    private bool applyDamage = false; //When true, the GameController will apply damage to the tree.
    private float lastDamage = 0f;

    void Start () {
        translation = new Vector3(0, 0, this.speed);
        enemyAnimation = GetComponent<Animation>();
        whackSound = GetComponent<AudioSource>();
    }
	
	void Update () {
        if (moving) {
            this.transform.Translate(translation * Time.deltaTime);
        } else { //We are hitting the tree
            lastDamage += Time.deltaTime;

            if (lastDamage >= 1f && !applyDamage) { //Apply damage every second
                applyDamage = true; //Damage can now be applied to the tree.
            }
        }
	}

    void OnCollisionEnter (Collision col) {
        if (col.gameObject.CompareTag("Tree")) {
            if (moving) {
                moving = false;
                lastDamage = 0.5f; //Reset damage timer, compensate for cross fade
                enemyAnimation.CrossFade("Lumbering");
            }
        } else if (col.gameObject.CompareTag("Bullet")) {
            BulletScript bs = col.gameObject.GetComponent<BulletScript>();

            if (!bs.hit) {
                whackSound.Play(); //Hit sound
                hp -= bs.damage; //Take damage from the bullet.
                bs.hit = true;
            }

            Destroy(col.gameObject);

            if (hp <= 0) { //Lumberjack has no more hp.
                Destroy(this.gameObject);
                dead = true;
            }
        }
    }

    public bool CanApplyDamage() {
        return applyDamage;
    }

    public void ResetDamageTimer() {
        lastDamage = 0f;
        applyDamage = false;
    }

}
