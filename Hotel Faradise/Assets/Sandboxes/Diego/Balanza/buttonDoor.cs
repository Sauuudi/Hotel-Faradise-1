using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonDoor : MonoBehaviour
{

    public GameObject door;
    public string tagName = "Clavo";
    public float speed = 1.0f;
    public float upPosition = 1.5f;
    public float timeToMove = 0.3f;
    private bool onCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tagName) && !onCoroutine)
        {
            StartCoroutine("MoveDoor");
        }
    }

    IEnumerator MoveDoor()
    {
        yield return new WaitForSeconds(timeToMove);
        onCoroutine = true;
        float count = 0.0f;
        while (count < upPosition)
        {
            door.transform.position = new Vector3(door.transform.position.x, door.transform.position.y + 0.01f * speed, door.transform.position.z);
            count += 0.01f * speed;
            yield return null;
        }
    }

    public void activatedButton()
    {
        if(tagName == "Button" && !onCoroutine)
        {
            StartCoroutine("MoveDoor");
        }
        
    }
}
