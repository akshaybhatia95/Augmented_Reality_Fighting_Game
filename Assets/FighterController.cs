using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FighterController : MonoBehaviour {
    public Transform enemyTarget;

    static Animator anim;
    public static bool mvBack = false;
    public static bool mvFwd = false;
    public static FighterController instance;
    public static bool isAttacking = false;
    private Vector3 direction;
    public int health = 100;
    public Slider playerHB;
    public BoxCollider[] c;
    public AudioClip[] audioClip;
    AudioSource audioSource;
    private Vector3 playerPosition;

    void Awake()
    {
        if (instance == null) {
            instance = this;
        }
    }
    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        setAllBoxColliders(false);
        audioSource = GetComponent<AudioSource>();
        playerPosition = transform.position;
    }
    private void setAllBoxColliders(bool state)
    {
        c[0].enabled = state;
        c[1].enabled = state;
    }
    private void playAudio(int clip)
    {
        audioSource.clip = audioClip[clip];
        audioSource.Play();
    }


    // Update is called once per frame
    void Update () {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("fight_idle"))
        {
            direction = enemyTarget.position - this.transform.position;
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.3f);
        }
            
       if (anim.GetCurrentAnimatorStateInfo(0).IsName("fight_idle")){
            isAttacking = false;
            setAllBoxColliders(false);
        }

        if (isAttacking == false)
        {
            setAllBoxColliders(false);
            if (mvBack == true)
            {
                anim.SetBool("wkBack0", true);


            }
            else
            {

                anim.SetBool("wkBack0", false);
            }
            if (mvFwd == true)
            {
                anim.SetBool("wkFwd0", true);



            }
            else
            {

                anim.SetBool("wkFwd0", false);

            }
        }
        else {
            setAllBoxColliders(true);
        }

    }
   
    public void punch()
    {
       
        isAttacking = true;
        
        anim.SetTrigger("punch");
        playAudio(0);
    }

    public void kick()
    {
        
        isAttacking = true;

        anim.SetTrigger("kick");
        playAudio(1);
    }

    public void react()
    {
        isAttacking = true;
       
        anim.SetTrigger("reaction");

        health = health - 10;
        if (health < 10)
        {
            knockout();
            playAudio(3);
        }
        else {
            playAudio(2);
            anim.ResetTrigger("idle");
            anim.SetTrigger("react");
        }

        playerHB.value = health;
    }
    public void knockout() {
        anim.SetTrigger("knockout");
        health = 100;
        playerHB.value = 100;
        GameController.instance.scoreEnemy();
        GameController.instance.OnScreenPoints();
        GameController.instance.rounds(); GameController.allowMovement = false;

        if (GameController.enemyScore == 2)
        {
            GameController.instance.doReset();

        }
        else
        {
            StartCoroutine(resetCharacters());
        }

    }
    IEnumerator resetCharacters()
    {
        yield return new WaitForSeconds(4);
        GameController.allowMovement = true;
    }
}
