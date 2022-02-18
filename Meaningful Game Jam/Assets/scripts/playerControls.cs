using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControls : MonoBehaviour
{
    [SerializeField] float          speed = 1;
    private Rigidbody2D             body;
    private Vector3                 velocity = Vector2.zero;
    public bool                     inRange = false;
    public Dog                      inLeash;
    public menus                    menu;
    public Dog                      dogInRange;
    private SpriteRenderer          gfx;
    private Animator                animator;


    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        inLeash = new Dog();
        inLeash.dogName = "";
        dogInRange = new Dog();
        gfx = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();


    }

    private void FixedUpdate()
    {
        InputPlayer();            
    }

    private void InputPlayer()
    {

        velocity = Input.GetAxis("Horizontal") * Vector2.right*speed + Input.GetAxis("Vertical") * Vector2.up * speed;
        if (velocity.x > 0.1f)
            gfx.flipX = false;
        else if(velocity.x < -0.1f)
            gfx.flipX = true;
        body.velocity = velocity;
        animator.SetFloat("Speed", Mathf.Abs(velocity.x)+Mathf.Abs(velocity.y));
    }

    private void Update()
    {
        if (inRange && dogInRange.exist && dogInRange.owned)
            menu.OpenDogMenu(dogInRange);
        else if ((!inRange || !dogInRange.exist || !!dogInRange.owned)&& menu.dogMenu.activeSelf)
            menu.CloseDogMenu();
        else if(inRange && Input.GetButtonDown("Submit"))
        {
            if (!dogInRange.exist)
            {
                dogInRange.CopyValues(inLeash);
                inLeash = new Dog();
                inLeash.dogName = "";
                dogInRange.exist = true;
                dogInRange.owned = true;
            }  
            else if(dogInRange.exist && !dogInRange.owned)
            {
                if (inLeash.dogName == "")
                {
                    
                    inLeash.CopyValues(dogInRange);
                    Destroy(dogInRange.gameObject);
                    
                }
                else
                {
                    Dog temp = new Dog();
                    temp.CopyValues(inLeash);
                    inLeash.CopyValues(dogInRange);
                    dogInRange.CopyValues(temp);
                }
            }
        }
    }

    public void SetRangeDog(Dog doginrange)
    {
        dogInRange = doginrange;
        inRange = true;
    }
}
