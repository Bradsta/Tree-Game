using UnityEngine;
using System.Collections;

public class EnemyScript : MonoBehaviour {

    public int xPerSecond = 1;

    private Vector3 translation;

    void Start ()
    {
        this.translation = new Vector3(0, 0, this.xPerSecond);
    }
	
	// Update is called once per frame
	void Update ()
    {
        this.transform.Translate(translation * Time.deltaTime);
	}

    void OnCollisionEnter (Collision col)
    {
        if (col.gameObject.CompareTag("Tree"))
        {
            Destroy(col.gameObject);
            Destroy(this.gameObject);
        }
    }

}
