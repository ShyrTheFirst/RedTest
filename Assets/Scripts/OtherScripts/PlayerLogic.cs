using UnityEngine;

public class PlayerLogic : MonoBehaviour
{
    float move_speed = 0.01f;
    int attack_move = 0;
    bool CanAttack = true;
    bool Special_ready = false;
    private int hitRange = 1;
    private Rigidbody rb;
    private Animator anim;
    private AnimatorStateInfo anim_info;
    private AudioSource Audio;
    public AudioClip Attack1Sound, Attack2Sound, Attack3Sound, SpecialSound;
    private double timer_limit = 1.0;
    private double timer_actual = 0.0;
    void Start()
    {
      rb = GetComponent<Rigidbody>();
      anim = GetComponent<Animator>();
      Audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.CanPlay())
        {
            return;
        }
        if (timer_actual >= timer_limit && GameManager.Instance.ReturnSpecialData() < 100)
        {
            GameManager.Instance.AddSpecialPoints(10);
            timer_actual = 0.0;
        }
        else
        {
            timer_actual += Time.deltaTime;
        }

        if (GameManager.Instance.ReturnSpecialData() >= 100)
        {
            GameManager.Instance.TriggerEvent("TriggerParticle");
            Special_ready = true;
        }


        anim_info = anim.GetCurrentAnimatorStateInfo(0);
        if (anim_info.IsName("Anim_Player_Idle"))
        {
            CanAttack = true;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (horizontal != 0 || vertical != 0 && Input.GetAxis("Fire1") <= 0)
        {
            anim.SetBool("Walking", true);
        }
        
        else
        {
            anim.SetBool("Walking", false);
        }

        if (Input.GetButtonDown("Fire1") && CanAttack)
        {
            anim.SetTrigger("Attacking");
            anim.SetInteger("Attack", attack_move);
            DetectHit();
            switch (attack_move)
            {
                case 0:
                PlaySFX(Attack1Sound);
                break;
                case 1:
                PlaySFX(Attack2Sound);
                break;
                case 2:
                PlaySFX(Attack3Sound);
                break;

            }
            attack_move += 1;
            if (attack_move > 2)
            {
                attack_move = 0;
            }
        }
        
        
        if (Input.GetButtonUp("Fire1") && CanAttack)
        {
            anim.ResetTrigger("Attacking");
            CanAttack = false;
        }
         
        if (Input.GetButtonDown("Fire2") && Special_ready && anim_info.IsName("Anim_Player_Idle"))
        {
            anim.SetTrigger("Special");
            DetectHit();
            PlaySFX(SpecialSound);
            GameManager.Instance.AddSpecialPoints(-100);
            Special_ready = false;
        }
        
        if (Input.GetButtonUp("Fire2") && !Special_ready && anim_info.IsName("Anim_Player_Idle"))
        {
            anim.ResetTrigger("Special");
        }
        
        Vector3 movement = new Vector3(horizontal,0,vertical) * move_speed;
        rb.position += movement;

    }

    void DetectHit()
    {
        RaycastHit hit;
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 origin = transform.position;

        if(Physics.Raycast(origin: origin,direction: forward, maxDistance: hitRange, hitInfo: out hit))
        {
            if(hit.transform.gameObject.tag == "Enemy")
            {
                hit.transform.gameObject.SendMessage("TakeDamage");
            }
        }
    }

    void PlaySFX(AudioClip value)
    {
        //Audio.AudioClip = value;
        Audio.clip = value;
        Audio.Play();
    }

}
