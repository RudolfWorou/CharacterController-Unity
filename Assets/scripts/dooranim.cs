using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dooranim : MonoBehaviour
{
    // Start is called before the first frame update
    public float mindist = 3;
    public Animator animator;
    public Transform theplayer;
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        theplayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Vector3.Distance(transform.position, theplayer.position) <= mindist)
        {
            animator.SetBool("character_nearby",true);
        }
        else
        {
            animator.SetBool("character_nearby", false  );
        }
    }

/*
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag == "Player")
        {
            
        }
    }
    */
}
