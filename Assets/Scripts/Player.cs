using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public enum Direction
    {
        Left,
        Right
    }

    [SerializeField]
    private float speed = 1f;


    private PlayerInput input;
    private Direction direction;
    private Animator anim;

 
    public void ChangeDirection(Direction newDirection) {
        switch (newDirection)
        {
            case Direction.Left:
                anim.SetTrigger("moveLeft");
                break;
            case Direction.Right:
                anim.SetTrigger("moveRight");
                break;
            default:
                break;
        }
        direction = newDirection;
    }

    // Use this for initialization
    void Start () {
        input = new PlayerInput(this);
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        input.Update();
        //anim.ResetTrigger("moveLeft");
        //anim.ResetTrigger("moveRight");

        if (direction == Direction.Left)
        {
            transform.Translate( - (speed * Time.deltaTime), 0, 0);
        }
        else
        {
            transform.Translate(speed * Time.deltaTime, 0, 0);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
            coll.gameObject.SendMessage("ApplyDamage", 10);

    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.tag == "Enemy")
            coll.gameObject.SendMessage("ApplyDamage", 10);

    }
}
