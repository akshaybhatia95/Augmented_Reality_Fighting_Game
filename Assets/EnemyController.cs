using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {
    public Transform playerTransform;
    private Vector3 direction;
    static Animator anim2;
    public int enemyHealth = 100;
    public static EnemyController instance;
    public Slider enemyHB;
    public BoxCollider[] c;
    public AudioClip[] audioClip;
    AudioSource audioSource;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Use this for initialization
    void Start () {
        anim2 = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        setAllBoxColliders(false);
	}
    private void playAudio(int clip)
    {
        audioSource.clip = audioClip[clip];
        audioSource.Play();
    }
    private void setAllBoxColliders(bool state)
    {
        c[0].enabled = state;
        c[1].enabled = state;
    }





    // Update is called once per frame
    void Update () {
        if (anim2.GetCurrentAnimatorStateInfo(0).IsName("fight_idleCopy"))
        {
            direction = playerTransform.position - this.transform.position;
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.3f);
            setAllBoxColliders(false);
            audioSource.Stop();
        }

        if (direction.magnitude > 1.5f && GameController.allowMovement)
        {
            anim2.SetTrigger("walkFWD");
            setAllBoxColliders(false);
            audioSource.Stop();
        }
        else {
            anim2.ResetTrigger("walkFWD");
        }
        if (direction.magnitude < 1.5f && direction.magnitude > 0.75f && GameController.allowMovement) {
            setAllBoxColliders(true);
            if (!audioSource.isPlaying && !anim2.GetCurrentAnimatorStateInfo(0).IsName("roundhouse_kick-mine"))
            {
                playAudio(1);
                anim2.SetTrigger("kick");
            }

        }
        else
        {
            anim2.ResetTrigger("kick");
        }
        if (direction.magnitude <= 0.75f && direction.magnitude > 0.2f && GameController.allowMovement)
        {
            setAllBoxColliders(true);
            if (!audioSource.isPlaying && !anim2.GetCurrentAnimatorStateInfo(0).IsName("cross_punch"))
            {
                playAudio(0);
                anim2.SetTrigger("punch");
            }

        }
        else
        {
            anim2.ResetTrigger("punch");
        }
        if (direction.magnitude <= 0.2f && GameController.allowMovement)
        {
            setAllBoxColliders(false);
            anim2.SetTrigger("walkBack");
            audioSource.Stop();

        }
        else
        {
            anim2.ResetTrigger("walkBack");
        }
    }

    public void enemyReact()
    {
        enemyHealth = enemyHealth - 10;
        enemyHB.value = enemyHealth;
        if(enemyHealth < 10)
        {
            enemyKnockout();
            playAudio(3);
;        }
        else
        {
            anim2.ResetTrigger("idle");
            playAudio(2);
            anim2.SetTrigger("react");
        }
      
    }

    public void enemyKnockout()
    {
        anim2.SetTrigger("knockout");
        GameController.instance.scorePlayer();
        GameController.instance.OnScreenPoints();
        GameController.instance.rounds();
    }
}
