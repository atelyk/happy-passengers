using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour {
    Transform transform;

	// Use this for initialization
	void Start () {
        transform = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        Move();
	}

    protected virtual void Move()
    {
        transform.Translate(0, -(GameManager.Instance.Speed * Time.deltaTime), 0);
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
            coll.gameObject.SendMessage("ApplyDamage", 10);

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "sceneCollider")
            ObstacleManager.Instance.MakeObjectFree(this.gameObject);
    }
}
