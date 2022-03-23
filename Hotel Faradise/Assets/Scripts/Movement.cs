using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 2f; 
    public float jumpForce = 5.0f;

    private Rigidbody2D _body;
    private Animator _anim;
    private BoxCollider2D _box;

    //private float iceDirection;
    //public float iceSpeed;
    //private bool onIce;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _box = GetComponent<BoxCollider2D>();
        //iceDirection = 0;
        //iceSpeed = 20;
        //onIce = false;
    }
    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed ;
        _anim.SetFloat("speed", Mathf.Abs(deltaX));
        if (!Mathf.Approximately(deltaX, 0f))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX), 1f, 1f);
        }

        Vector2 movement = new Vector2(deltaX, _body.velocity.y);
         _body.velocity = movement;        

        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            _anim.SetTrigger("jumping");
            _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        /*if(Input.GetAxis("Horizontal") != 0){
            iceDirection = Input.GetAxis("Horizontal");
        }

        //aqui tendria que mirar si esta sobre ice, en vez de pulsar la p
        // yo haria que los suelos de hielotengan un tag iceGround
        //si esta sobre hielo, desactivar el movimiento normal
        if ((Input.GetKeyDown("p"))) 
        { 
            if(onIce) onIce = false;
            else onIce = true;
        }
        
        if(grounded && onIce){
             transform.Translate(new Vector3(iceDirection * iceSpeed, 0, 0) * Time.deltaTime);
        }*/
       
        
    }

    private (Vector2, Vector2) getGroundCheckCorners()
    {
        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        return (corner1, corner2);
    }
    public bool grounded
    {
        get
        {
            var (corner1, corner2) = getGroundCheckCorners();
            Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
            return (hit != null);
        }
    }
    
}
