using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crime : MonoBehaviour
{
    public Crimes CrimeType;
    [HideInInspector]
    public NPCConversantNames CriminalConversant;
    [HideInInspector]
    public CrimeSentence CrimePunishment;

    [HideInInspector]
    public NPC Criminal;
    [HideInInspector]
    public string criminalName;
    [HideInInspector]
    public string crimeName;
    [HideInInspector]
    public string crimeDescription;

    public bool crimeResolved;
    [HideInInspector]
    public bool crimeSuccess;
    [HideInInspector]
    public bool crimeReported;
    [HideInInspector]
    public int crimeReputationGain;
    [HideInInspector]
    public int crimeXPGain;



    public void Start()
    {
        BuildCrime();
        Criminal = GetComponent<NPC>();
        criminalName = Criminal.characterName;
    }



    // build crime
    void BuildCrime()
    {
        switch (CrimeType)
        {
            case Crimes.Littering:
                crimeName = "<color=#ffc149>Littering</color>";
                crimeDescription = "Penal Code <color=#ffc149>#1333</color>\nSection 5";
                crimeReputationGain = 2;
                crimeXPGain = 400;
                break;
            case Crimes.ProhibitedSubstance:
                crimeName = "<color=#ffc149>Prohibited Substance</color>";
                crimeDescription = "Penal Code <color=#ffc149>#2393</color>\nSection 2";
                crimeReputationGain = 3;
                crimeXPGain = 400;
                break;
            case Crimes.HostageTaking:
                crimeName = "<color=#ffc149>Hostage Taking</color>";
                crimeDescription = "Penal Code <color=#ffc149>#6127</color>\nSection 8a";
                crimeReputationGain = 10;
                crimeXPGain = 0;
                break;
            case Crimes.IllegalBroadcast:
                crimeName = "<color=#ffc149>Illegal Broadcast</color>";
                crimeDescription = "Penal Code <color=#ffc149>#1811</color>\nSection 6";
                crimeReputationGain = 5;
                crimeXPGain = 400;
                break;
            case Crimes.Tobacco:
                crimeName = "<color=#ffc149>Tobacco</color>";
                crimeDescription = "Penal Code <color=#ffc149>#9132</color>\nSection 4";
                crimeReputationGain = 3;
                crimeXPGain = 400;
                break;
        }
    }



    // roll crime conviction
    public void RollCrimeConviction(string _sentence)
    {
        // ui
        //UI.instance.OpenCrimeSolved(this);

        // report
        UI.instance.ReportedCrime = this;
        UI.instance.reportCrimeOnConversationEnd = true;

        // resolve sentencing
        crimeResolved = true;
        CrimePunishment = (CrimeSentence)System.Enum.Parse(typeof(CrimeSentence), _sentence);

        int _chance = 0;

        switch (CrimePunishment)
        {
            case CrimeSentence.SoftFine:
                _chance = 80;
                break;
            case CrimeSentence.MediumFine:
                _chance = 60;
                break;
            case CrimeSentence.HardFine:
                _chance = 40;
                break;
            case CrimeSentence.BrutalFine:
                _chance = 20;
                break;
            case CrimeSentence.SoftArrest:
                _chance = 80;
                break;
            case CrimeSentence.MediumArrest:
                _chance = 60;
                break;
            case CrimeSentence.HardArrest:
                _chance = 40;
                break;
            case CrimeSentence.BrutalArrest:
                _chance = 20;
                break;
        }

        // roll sentence
        if (Random.Range(0, 100) <= _chance || CrimePunishment == CrimeSentence.Execution)
            crimeSuccess = true;
        else
            crimeSuccess = false;

        // add crime to database
        CrimeDatabase.CrimeList.Add(this);

        // add to stat
        AddToStat();

        // add xp
        AddXP();
    }

    // add to stat
    void AddToStat()
    {
        PlayerCharacter _Player = PlayerScene.instance.MainCharacter;

        switch (CrimePunishment)
        {
            case CrimeSentence.SoftFine:
                _Player.fines++;
                break;
            case CrimeSentence.MediumFine:
                _Player.fines++;
                break;
            case CrimeSentence.HardFine:
                _Player.fines++;
                break;
            case CrimeSentence.BrutalFine:
                _Player.fines++;
                break;
            case CrimeSentence.SoftArrest:
                _Player.arrests++;
                break;
            case CrimeSentence.MediumArrest:
                _Player.arrests++;
                break;
            case CrimeSentence.HardArrest:
                _Player.arrests++;
                break;
            case CrimeSentence.BrutalArrest:
                _Player.arrests++;
                break;
        }
    }

    // add to stat
    public string GetPunishmentType()
    {
        string _type = "";

        switch (CrimePunishment)
        {
            case CrimeSentence.SoftFine:
                _type = "Fine (Soft)";
                break;
            case CrimeSentence.MediumFine:
                _type = "Fine (Medium)";
                break;
            case CrimeSentence.HardFine:
                _type = "Fine (Hard)";
                break;
            case CrimeSentence.BrutalFine:
                _type = "Fine (Brutal)";
                break;
            case CrimeSentence.SoftArrest:
                _type = "Arrest (Short)";
                break;
            case CrimeSentence.MediumArrest:
                _type = "Arrest (Medium)";
                break;
            case CrimeSentence.HardArrest:
                _type = "Arrest (Long)";
                break;
            case CrimeSentence.BrutalArrest:
                _type = "Arrest (Life)";
                break;
            case CrimeSentence.Execution:
                _type = "Executed";
                break;
        }

        return _type;
    }

    // add xp
    void AddXP()
    {
        PlayerCharacter _Player = PlayerScene.instance.MainCharacter;

        _Player.GainExperience(crimeXPGain);
    }
}
