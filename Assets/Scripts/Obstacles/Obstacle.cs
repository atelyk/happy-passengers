using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    //Position transform;

    void Awake()
    {
        //transform = GetComponent<Position>();
    }

    void Update()
    {
        Move();
    }

    protected virtual void Move()
    {
        transform.Translate(0, -(GameManager.Instance.GameSpeed * Time.deltaTime), 0);
    }

    protected virtual void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "sceneCollider")
            ObstacleManager.Instance.MakeObjectFree(this.gameObject);
    }
}
