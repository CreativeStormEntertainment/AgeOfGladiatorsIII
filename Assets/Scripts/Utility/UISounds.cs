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



    public void PlayRegularButton()
    {
        Audio.PlayOneShot(Regular[0]);
    }

    public void PlayLargeButton()
    {
        Audio.PlayOneShot(Large[0]);
    }

    public void PlaySmallButton()
    {
        Audio.PlayOneShot(Small[0]);
    }

    public void PlayTabButton()
    {
        Audio.PlayOneShot(Tab[0]);
    }

    public void PlayCloseButton()
    {
        Audio.PlayOneShot(Close[0]);
    }

    public void PlayPipButton()
    {
        Audio.PlayOneShot(Pip[0]);
    }

    public void PlayMouseOn(int _index)
    {
        Audio.PlayOneShot(Hover[_index]);
    }

    public void PlayMouseOnNode()
    {
        Audio.PlayOneShot(MouseOnNode[0]);
    }

    public void PlayWindowOpen()
    {
        Audio.PlayOneShot(WindowOpen[0]);
    }

    public void PlaySkillSucceed()
    {
        Audio.PlayOneShot(SkillSucceed[0]);
    }

    public void PlaySkillFail()
    {
        Audio.PlayOneShot(SkillFail[0]);
    }

    public void PlayActionNotPossible()
    {
        Audio.PlayOneShot(ActionNotPossible[0]);
    }

    public void PlayActionTravel()
    {
        Audio.PlayOneShot(TravelSound[0]);
    }
}
