using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repulsorScript : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
    }

    Rigidbody2D colliderRB;
 
    private void OnTriggerStay2D(Collider2D collision)
    {
        colliderRB = collision.gameObject.GetComponent<Rigidbody2D>();
        colliderRB.velocity = colliderRB.velocity + (Vector2)gameObject.transform.up * speed;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        colliderRB = collision.gameObject.GetComponent<Rigidbody2D>();
        colliderRB.velocity = Vector2.zero;
    }

}
