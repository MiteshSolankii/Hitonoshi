using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private bool isFollowing;
    public float followSpeed = 4f;

    public Transform followTarget = null;

    private Animator anim;

    public GameObject UI_keyImage = null;


    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing)
        {
            anim.enabled = false;
            UI_keyImage.SetActive(true);
            transform.position = Vector3.Lerp(transform.position, followTarget.position, followSpeed * Time.deltaTime);
        }
        else
        {
            //anim.enabled = true;
            UI_keyImage.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!isFollowing)
            {
                Audiomanager.instance.PlaySound("KeyCollectSound");
                GetComponent<SpriteRenderer>().sprite = null;
                PlayerController playerController = FindObjectOfType<PlayerController>();

                followTarget = playerController.keyFollowPoint;

                isFollowing = true;
                playerController.followingKey = this;
            } 
        }
        
    }
}
