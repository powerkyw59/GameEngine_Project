﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour,IDamagable
{
    [SerializeField] private GameObject player = null;
    [SerializeField] private Animator animator = null;
    [SerializeField] private GameObject particle = null;
    [SerializeField] private CapsuleCollider2D collider2D = null;

    // Start is called before the first frame update.
    public float HitPoint { get; private set; }
    public float AttackTime  { get; private set; }

    public bool isMove { get; private set; }
    public bool isNearPlayer(ref float dir)
    {
        float deltaX = (player.transform.position.x - transform.position.x);
        float deltaY = (player.transform.position.y - transform.position.y);

        dir = deltaX > 0 ? 1 : -1;
        deltaX = Mathf.Abs(deltaX);
        deltaY = Mathf.Abs(deltaY);

        if (deltaX < 1 && deltaX > 0.1 && deltaY < 0.3)
            return true;
        else
            return false;
    }
    public void Damage(float damageAmount)
    {
        HitPoint -= damageAmount;

        if(HitPoint <= 0)
        {
            particle.SetActive(true);
            particle.transform.position = new Vector3(transform.position.x, transform.position.y-0.18f, transform.position.z);
            //gameObject.SetActive(false);
            pSystem sys = particle.gameObject.GetComponent<pSystem>();

            sys.playTime = 0;

            animator.SetBool("Dead", true);
            collider2D.enabled = false;
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        }
    }

    void Start()
    {
        HitPoint = 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (HitPoint <= 0)
            return;
        float dir = 0;
        if (isNearPlayer(ref dir))
        {
            transform.position = new Vector3(transform.position.x + dir*0.5f *Time.deltaTime, transform.position.y, transform.position.z);

            transform.localRotation = dir < 0 ?
                    Quaternion.Euler(0, 180, 0) :
                    Quaternion.identity;
            animator.SetBool("Move",true);
        }
        else
        {
            if(animator.GetBool("Move"))
            {
               animator.SetBool("Move", false);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 14)
        {
            Damage(5);
            //transform.Translate(-0.1f, 0, 0);
        }
    }
 

}
