using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTP : MonoBehaviour
{
    public GameObject respawnPoint;
    public string tagName = "Player";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(tagName))
        {
            collision.transform.position = new Vector3(respawnPoint.transform.position.x, respawnPoint.transform.position.y, respawnPoint.transform.position.z);
        }
        {

        }
    }
}
