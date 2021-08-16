using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private PlayerController playerController;

    private Animator anim;

    public bool waitingToOpenDoor, doorOpen;

    public SpriteRenderer theSR;
    public Sprite doorOpenSprite;

    public GameObject doorInputText;
    public int sceneNumber;

    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waitingToOpenDoor)
        {
            anim.GetComponent<Animator>().enabled = true;
            
            if (Vector3.Distance(playerController.followingKey.transform.position,transform.position)<= 0.1f)
            {
                
                waitingToOpenDoor = false;

                doorOpen = true;
                Key key = FindObjectOfType<Key>();
                key.UI_keyImage.SetActive(false);
                
                
                anim.GetComponent<Animator>().enabled = false;
                theSR.sprite = doorOpenSprite;

                playerController.followingKey.gameObject.SetActive(false);
                playerController.followingKey = null;
            }
        }

        if (doorOpen && Vector3.Distance(playerController.transform.position, transform.position) < 2f )
        {
            doorInputText.SetActive(true);
           
        }
        else
        {
            doorInputText.SetActive(false);
        }
    }

    public void EndLevel()
    {
        SceneManager.LoadScene(sceneNumber);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if(playerController.followingKey != null)
            {
                playerController.followingKey.followTarget = transform;
                waitingToOpenDoor = true;
                Audiomanager.instance.PlaySound("DoorUnlockSound");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            doorInputText.SetActive(false);
        }
    }
}
