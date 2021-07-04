using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSystem : MonoBehaviour
{

    Animator animator;

    public int rewards = 5;
    public int currentRewards = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void GetReward()
    {
        animator.SetBool("BoxShow", true);
        
    }

    public void ShootReward()
    {
        animator.SetBool("BoxShow", false);

        for(int i = rewards; i > rewards; i--)
        {
            Debug.Log(i);
            animator.SetBool("BoxShoot", true);
           
        }

        

       

        
    }

}
