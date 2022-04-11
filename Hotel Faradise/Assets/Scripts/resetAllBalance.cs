using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetAllBalance : MonoBehaviour
{
    private int colL = 0;
    private int colR = 0;
    private int colU = 0;
    public GameObject platL;
    public GameObject platR;
    private int deepest = 0;
    private Transform tranL;
    private Transform tranR;
    private Rigidbody2D bodyL;
    private Rigidbody2D bodyR;
    private float difference;
    public float correctionTime = 0.075f;
    public float correctionForce = 1.5f;
    public float correctionSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        tranL = platL.transform;
        tranR = platR.transform;
        bodyL = platL.GetComponent<Rigidbody2D>();
        bodyR = platR.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(colR == 0 && colL == 0 && colU == 0)
        {

            difference = Mathf.Abs(tranL.position.y - tranR.position.y);
            //counter++;
            if(difference > correctionTime)
            {
                if (tranL.position.y < tranR.position.y)
                {
                    bodyL.AddForce(Vector2.up * Time.deltaTime * difference * correctionSpeed, ForceMode2D.Impulse);
                    deepest = 0;
                }
                else
                {
                    bodyR.AddForce(Vector2.up * Time.deltaTime * difference * correctionSpeed, ForceMode2D.Impulse);
                    deepest = 1;
                }
            } else
            {
                if (deepest == 0)
                {
                    bodyR.AddForce(bodyL.velocity * correctionForce, ForceMode2D.Impulse);
                    
                }
                else
                {
                    bodyL.AddForce(bodyR.velocity * correctionForce, ForceMode2D.Impulse);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        colU++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        colU--;
    }

    public void increaseCol(int id)
    {
        if (id == 0) colL++;
        else colR++;
    }

    public void decreaseCol(int id)
    {
        if (id == 0) colL--;
        else colR--;
    }
}
