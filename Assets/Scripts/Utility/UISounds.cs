using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISounds : MonoBehaviour
{
    public static UISounds instance;

    public static AudioSource Audio;

    [Header("Button")]
    public AudioClip[] Regular;
    public AudioClip[] Large;
    public AudioClip[] Small;
    public AudioClip[] Tab;
    public AudioClip[] Close;
    public AudioClip[] Open;
    public AudioClip[] Pip;

    [Header("Other")]
    public AudioClip[] Hover;
    public AudioClip[] MouseOnNode;
    public AudioClip[] SkillSucceed;
    public AudioClip[] SkillFail;
    public AudioClip[] ActionNotPossible;
    public AudioClip[] TravelSound;
    public AudioClip[] WindowOpen;



    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    private void Start()
    {
        Audio = GetComponent<AudioSource>();
    }



    // regular
    public void PlayRegularButton()
    {
        Audio.PlayOneShot(Regular[0]);
    }

    // large
    public void PlayLargeButton()
    {
        Audio.PlayOneShot(Large[0]);
    }

    // small
    public void PlaySmallButton()
    {
        Audio.PlayOneShot(Small[0]);
    }

    // tab
    public void PlayTabButton()
    {
        Audio.PlayOneShot(Tab[0]);
    }

    // close
    public void PlayCloseButton()
    {
        Audio.PlayOneShot(Close[0]);
    }

    // pip
    public void PlayPipButton()
    {
        Audio.PlayOneShot(Pip[0]);
    }

    // mouse on
    public void PlayMouseOn(int _index)
    {
        Audio.PlayOneShot(Hover[_index]);
    }

    // mouse on (travel node)
    public void PlayMouseOnNode()
    {
        Audio.PlayOneShot(MouseOnNode[0]);
    }

    // mouse on
    public void PlayWindowOpen()
    {
        Audio.PlayOneShot(WindowOpen[0]);
    }

    // skill succeeed
    public void PlaySkillSucceed()
    {
        Audio.PlayOneShot(SkillSucceed[0]);
    }

    // skill fail
    public void PlaySkillFail()
    {
        Audio.PlayOneShot(SkillFail[0]);
    }

    // skill fail
    public void PlayActionNotPossible()
    {
        Audio.PlayOneShot(ActionNotPossible[0]);
    }

    // travel sound
    public void PlayActionTravel()
    {
        Audio.PlayOneShot(TravelSound[0]);
    }
}
