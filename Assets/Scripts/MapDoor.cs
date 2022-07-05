using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDoor : MonoBehaviour
{
    public bool doorOpen;

    public Animation AttachedAnimation;



    private void Awake()
    {
        // needs to be here - and also in trigger skill roll
        if (GetComponent<TriggerSkillRoll>() != null && GetComponent<TriggerSkillRoll>().SkillRequired == Skill.None)
            GetComponent<TriggerSkillRoll>().itemLocked = false;
    }



    // open door
    public void OpenDoor()
    {
        // do not proceed if door locked
        if (GetComponent<TriggerSkillRoll>().itemLocked || doorOpen)
            return;

        doorOpen = true;

        // audio
        if (GetComponent<AudioCue>() != null)
            GetComponent<AudioCue>().PlayCue();

        // animation
        PlayAnimation();

        StartCoroutine(HideDoor());

        Cursor.SetCursor(null, Vector3.zero, CursorMode.Auto);
    }

    // play animation
    public void PlayAnimation()
    {
        if (AttachedAnimation != null)
            AttachedAnimation.Play();
    }

    // hide doors
    public IEnumerator HideDoor()
    {
        yield return new WaitForSeconds(AttachedAnimation.clip.length);
        this.gameObject.SetActive(false);
    }
}
