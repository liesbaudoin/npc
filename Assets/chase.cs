using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chase : MonoBehaviour
{
    public Transform player;
    static Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();//altijd nodig, anders is anim leeg
    }


    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(player.position, this.transform.position);
        if (distance < 10)//vergelijking van de positie van de speler met de positie van de skeleton
        {
            Debug.Log("Within range");
            Vector3 direction = player.position - this.transform.position;//int het verschil tussen player en skeleton toekennen aan direction
            direction.y = 0;

            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f); //slerp itt snap is heel langzaam

            anim.SetBool("isIdle", false);
            if (distance >= 5)
            {
                this.transform.Translate(0, 0, 0.05f);
                anim.SetBool("isWalking", true);
                anim.SetBool("isAtacking", false);
            }
            else
            {
                anim.SetBool("isAtacking", true);
                anim.SetBool("isWalking", false);
            }
        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isAtacking", false);
            anim.SetBool("isWalking", false);
        }
    }
}
