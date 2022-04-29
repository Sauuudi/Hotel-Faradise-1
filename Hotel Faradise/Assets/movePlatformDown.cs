using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movePlatformDown : MonoBehaviour
{
    //[SerializeField] private Transform start;
    [SerializeField] private Transform end;
    private float originalY;
    //[SerializeField]
    private float weight = 0;
    public float upperLimit = 2.5f;
    public float lowerLimit = -2.5f;
    public float speed = 1.0f;
    private float realWeight = 0f;
    //private float extraWeight = 0;
    private bool onCoroutine = false;
    //private Rigidbody2D body;
    //public int id;
    private void Start()
    {
        originalY = transform.position.y;
        //body = GetComponent<Rigidbody2D>();
        //balanzaParent = GetComponentInParent<balanza>().gameObject;
        //transform.position = start.position;
        //end = start;
    }

    void Update()
    {
        if (weight > upperLimit)
        {
            //extraWeight += weight - upperLimit;
            weight = upperLimit;

        }
        else if (weight < lowerLimit)
        {
            //extraWeight += weight - lowerLimit;
            weight = lowerLimit;
        }
        else
        {
            weight = realWeight;
        }
        //end.position = new Vector3(start.position.x, start.position.y + weight, start.position.z);
        //transform.position = Vector2.Lerp(transform.position, end.position, 0.2f);
        //transform.position = Vector2.Lerp(transform.position, end.position, 0.01f);
    }

    //Para los objetos que se pongan encima
    public void changeWeight(float new_weight)
    {
        /*
        if(new_weight < 0)
        {
            new_weight = Mathf.Max(new_weight, lowerLimit);
        }
        else
        {
            new_weight = Mathf.Min(new_weight, upperLimit);
        } */
        weight -= new_weight;
        realWeight -= new_weight;
        //balanzaParent.GetComponent<balanza>().changeMoreWeight(id, new_weight);
        end.position = new Vector3(transform.position.x, originalY + weight, transform.position.z);
        if (!onCoroutine)
        {
            end.position = new Vector3(transform.position.x, originalY + weight, transform.position.z);
            StartCoroutine(MovePlatform());
        }

    }

    //Para comunicarse entre las plataformas
    public void modifyWeight(float new_weight)
    {
        weight += new_weight;
        realWeight += new_weight;
        end.position = new Vector3(transform.position.x, originalY + weight, transform.position.z);
        if (!onCoroutine)
        {
            end.position = new Vector3(transform.position.x, originalY + weight, transform.position.z);
            StartCoroutine(MovePlatform());
        }
    }

    IEnumerator MovePlatform()
    {
        onCoroutine = true;
        while (Mathf.Abs(transform.position.y - end.position.y) > 0.01f)
        {
            if (end.position.y > transform.position.y)
            {
                //body.MovePosition(new Vector2(transform.position.x, transform.position.y + Time.deltaTime * 1.5f));
                transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * speed, transform.position.z);
            }
            else
            {
                //body.MovePosition(new Vector2(transform.position.x, transform.position.y - Time.deltaTime * 1.5f));
                transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * speed, transform.position.z);
            }
            end.position = new Vector3(transform.position.x, originalY + weight, transform.position.z);
            //Debug.Log(Time.deltaTime.ToString());
            //transform.position = Vector2.Lerp(transform.position, end.position, 0.025f);
            yield return null;
            //yield return null;
        }
        onCoroutine = false;
        //Debug.Log("END " + i.ToString());
    }
}