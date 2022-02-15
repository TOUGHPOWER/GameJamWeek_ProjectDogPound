using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControls : MonoBehaviour
{
    [SerializeField] float  speed = 1;
    private Rigidbody2D     body;
    private Vector3         velocity = Vector2.zero;
    private float timer = 0;
    private int lastButton = 0;
    private int currentButton = 0;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //InputPlayer();            
    }

    private void InputPlayer()
    {

        //velocity = Input.GetAxis("Horizontal") * Vector2.right*speed + Input.GetAxis("Vertical") * Vector2.up * speed;
        //body.velocity = velocity;
        keydown();

        if (Input.GetKeyUp(KeyCode.A))
        {
            if (currentButton == 1)
                currentButton = lastButton;
            keydown();
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (currentButton == 2)
                currentButton = lastButton; 
            keydown();
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            if (currentButton == 3)
                currentButton = lastButton;
            keydown();
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            if(currentButton == 4)
                currentButton = lastButton;
            keydown();
        }
        switch (currentButton)
        {
            case 1:
                if ((Input.GetKey(KeyCode.A) && timer >= speed))
                {
                    timer = 0;
                    body.MovePosition(transform.position + Vector3.left);
                }
                
                break;
            case 2:
                if ((Input.GetKey(KeyCode.D) && timer >= speed))
                {
                    timer = 0;
                    body.MovePosition(transform.position + Vector3.right);
                }

                break;
            case 3:
                if ((Input.GetKey(KeyCode.W) && timer >= speed))
                {
                    timer = 0;
                    body.MovePosition(transform.position + Vector3.up);
                }
                break;
            case 4:
                if ((Input.GetKey(KeyCode.S) && timer >= speed))
                {
                    timer = 0;
                    body.MovePosition(transform.position + Vector3.down);
                }
                break;
        }
    }

    private void keydown()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            lastButton = currentButton;
            currentButton = 1;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            lastButton = currentButton;
            currentButton = 2;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            lastButton = currentButton;
            currentButton = 3;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            lastButton = currentButton;
            currentButton = 4;
        }
    }

    private void Update()
    {
        InputPlayer();
        timer += Time.deltaTime;
    }
}
