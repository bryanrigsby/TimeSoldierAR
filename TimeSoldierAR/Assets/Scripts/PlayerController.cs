using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D theRB;
    public FixedJoystick fixedJoystick;
    public float moveSpeed;

    public Animator myAnim; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //theRB.velocity = new Vector2(fixedJoystick.Horizontal, fixedJoystick.Vertical) * moveSpeed;

        //myAnim.SetFloat("moveX", theRB.velocity.x);
        //myAnim.SetFloat("moveY", theRB.velocity.y);



        //Debug.Log(fixedJoystick.Horizontal + ", " + fixedJoystick.Vertical);


        //if (fixedJoystick.Horizontal > 0.5 || fixedJoystick.Horizontal < -0.5 || fixedJoystick.Vertical > 0.5 || fixedJoystick.Vertical < -0.5)
        //{
        //    myAnim.SetFloat("lastMoveX", fixedJoystick.Horizontal);
        //    myAnim.SetFloat("lastMoveY", fixedJoystick.Vertical);
        //}


        theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * moveSpeed;

        myAnim.SetFloat("moveX", theRB.velocity.x);
        myAnim.SetFloat("moveY", theRB.velocity.y);

        if (Input.GetAxisRaw("Horizontal") == 1 || Input.GetAxisRaw("Horizontal") == -1 || Input.GetAxisRaw("Vertical") == 1 || Input.GetAxisRaw("Vertical") == -1)
        {
            myAnim.SetFloat("lastMoveX", Input.GetAxisRaw("Horizontal"));
            myAnim.SetFloat("lastMoveY", Input.GetAxisRaw("Vertical"));
        }
    }
}
