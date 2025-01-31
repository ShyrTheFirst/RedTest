using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyLogic : MonoBehaviour
{
    private Animator anim;
    private AnimatorStateInfo anim_info;
    private int Health = 10;
    private AudioSource Audio;
    public AudioClip hitsound, deathsound;

    public GameObject self;
    void Start()
    {
        anim = GetComponent<Animator>();
        Audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        anim_info = anim.GetCurrentAnimatorStateInfo(0);
        if (anim_info.IsName("Mesh_Player@Stunned"))
        {
            anim.ResetTrigger("Attacked");
        }
    }
    void TakeDamage()
    {
        Health -= 1;
        PlaySFX(hitsound);
        if (Health <= 0)
        {
            anim.SetBool("Dead", true);
            PlaySFX(deathsound);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            anim.SetBool("Dead", false);
        }
        anim.SetTrigger("Attacked");
    }

    void PlaySFX(AudioClip value)
    {
        Audio.clip = value;
        Audio.Play();
    }
}
