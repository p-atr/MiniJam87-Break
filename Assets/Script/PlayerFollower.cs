using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private float maxX;
    [SerializeField]
    private float maxY;
    [SerializeField]
    private float speed;

    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(player.transform.position.x, player.transform.position.y, -10), speed);

        float xDif = transform.position.x - player.transform.position.x;
        float yDif = transform.position.y - player.transform.position.y;


        if (Mathf.Abs(xDif) > maxX)
        {
            if(xDif > 0)
            {
                transform.position = new Vector3(player.transform.position.x + maxX, transform.position.y, -10);
            }
            else
            {
                transform.position = new Vector3(player.transform.position.x - maxX, transform.position.y, -10);
            }
        }
        if(Mathf.Abs(yDif) > maxY)
        {
            if (yDif > 0)
            {
                transform.position = new Vector3(transform.position.x, player.transform.position.y + maxY, -10);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, player.transform.position.y - maxY, -10);
            }
        }
    }

    public void SetPlayer(GameObject plr)
    {
        player = plr;
    }
}
