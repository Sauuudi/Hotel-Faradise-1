using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movement : MonoBehaviour
{
    public float speed = 2f; 
    public float jumpForce = 5.0f;

    private Rigidbody2D _body;
    private Animator _anim;
    private BoxCollider2D _box;

    [SerializeField] private LayerMask m_WhatIsGround;							// A mask determining what is ground to the character
    [SerializeField] private Transform m_GroundCheck;		// A position marking where to check if the player is grounded.
    const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;   // Whether or not the player is grounded.

    public float iceSpeed;
    private bool onIce;
    public UnityEvent OnLandEvent;

    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _box = GetComponent<BoxCollider2D>();
        iceSpeed = 20;
        onIce = false;
    }

    private void Awake() {
        if (OnLandEvent == null)
			OnLandEvent = new UnityEvent();
    }
    void Update()
    {   
        if(!onIce){
            float deltaX = Input.GetAxis("Horizontal") * speed ;
            _anim.SetFloat("speed", Mathf.Abs(deltaX));
            if (!Mathf.Approximately(deltaX, 0f))
            {
                transform.localScale = new Vector3(Mathf.Sign(deltaX), 1f, 1f);
            }
            Vector2 movement = new Vector2(deltaX, _body.velocity.y);
            _body.velocity = movement;  
        }

        if (m_Grounded && !onIce && Input.GetKeyDown(KeyCode.Space))
        {
            _anim.SetTrigger("jumping");
            _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    
        //aqui tendria que mirar si esta sobre ice, en vez de pulsar la p
        // yo haria que los suelos de hielotengan un tag iceGround
        //si esta sobre hielo, desactivar el movimiento normal
        if ((Input.GetKeyDown("p"))) 
        { 
            if(onIce) onIce = false;
            else onIce = true;
        }
        
        //cuando este sobre hielo aplicar un movimiento continuo sobre el personaje
        
    }

    private void FixedUpdate() {
        bool wasGrounded = m_Grounded;
		m_Grounded = false;

        // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
                Debug.Log("es grounded");
				m_Grounded = true;
				if (!wasGrounded)
					OnLandEvent.Invoke();
			}
		}
    }

    private (Vector2, Vector2) getGroundCheckCorners()
    {
        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        return (corner1, corner2);
    }
    /*public bool grounded
    {
        get
        {
            var (corner1, corner2) = getGroundCheckCorners();
            Collider2D hit = Physics2D.OverlapArea(corner1, corner2);
            return (hit != null);
        }
    }*/
    
}
