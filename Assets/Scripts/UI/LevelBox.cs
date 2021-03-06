using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelBox : MonoBehaviour
{
    public TextMeshProUGUI Header;
    public TextMeshProUGUI Label;
    public TextMeshProUGUI LevelLabel;



    public void PopulateLevel()
    {
        Header.text = ("Level Up!".ToUpper());
        Label.text = "Skill and perk points available".ToUpper();
        LevelLabel.text = (PlayerScene.instance.MainCharacter.level).ToString();
    }



    public IEnumerator CloseLevelBox()
    {
        yield return new WaitForSeconds(4f);
        Close();
    }

    void Close()
    {
        this.gameObject.SetActive(false);
    }
}
