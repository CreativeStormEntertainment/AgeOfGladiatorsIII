using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class PortraitSelector
{
    // find portrait
    public static Sprite FindPortrait(Character _Character, int _size)
    {
        // default image
        Sprite _ReturnImage = null;

        int _index = _Character.portraitNumber;

        // conversants
        switch (_Character.PortraitType)
        {
            case CharacterPortraitType.JudgeMale:
                if (_size == 0)
                    _ReturnImage = PortraitImages.instance.JudgeMalePortraitsSmall[_index];

                if (_size == 1)
                    _ReturnImage = PortraitImages.instance.JudgeMalePortraitsMedium[_index];

                if (_size == 2)
                    _ReturnImage = PortraitImages.instance.JudgeMalePortraits[_index];
                break;

            case CharacterPortraitType.JudgeFemale:
                if (_size == 0)
                    _ReturnImage = PortraitImages.instance.JudgeFemalePortraitsSmall[_index];

                if (_size == 1)
                    _ReturnImage = PortraitImages.instance.JudgeFemalePortraitsMedium[_index];

                if (_size == 2)
                    _ReturnImage = PortraitImages.instance.JudgeFemalePortraits[_index];
                break;

            case CharacterPortraitType.CitizenMale:
                if (_size == 0)
                    _ReturnImage = PortraitImages.instance.CitizenMalePortraitsSmall[_index];

                if (_size == 1)
                    _ReturnImage = PortraitImages.instance.CitizenMalePortraitsMedium[_index];

                if (_size == 2)
                    _ReturnImage = PortraitImages.instance.CitizenMalePortraits[_index];
                break;

            case CharacterPortraitType.CitizenFemale:
                if (_size == 0)
                    _ReturnImage = PortraitImages.instance.CitizenFemalePortraitsSmall[_index];

                if (_size == 1)
                    _ReturnImage = PortraitImages.instance.CitizenFemalePortraitsMedium[_index];

                if (_size == 2)
                    _ReturnImage = PortraitImages.instance.CitizenFemalePortraits[_index];
                break;

            case CharacterPortraitType.Item:
                if (_size == 0)
                    _ReturnImage = PortraitImages.instance.ItemPortraitsSmall[_index];

                if (_size == 1)
                    _ReturnImage = PortraitImages.instance.ItemPortraitsMedium[_index];

                if (_size == 2)
                    _ReturnImage = PortraitImages.instance.ItemPortraits[_index];
                break;

            case CharacterPortraitType.Character:
                if (_size == 0)
                    _ReturnImage = PortraitImages.instance.CharacterPortraitsSmall[_index];

                if (_size == 1)
                    _ReturnImage = PortraitImages.instance.CharacterPortraitsMedium[_index];

                if (_size == 2)
                    _ReturnImage = PortraitImages.instance.CharacterPortraits[_index];
                break;
        }

        return _ReturnImage;
    }


    // find conversant portrait (special dialogue)
    public static Sprite FindPortrait(NPCSpecialNames _Name)
    {
        // default image
        Sprite _ReturnImage = null;

        // conversants
        switch (_Name)
        {
            case NPCSpecialNames.LindaDooley:
                _ReturnImage = PortraitImages.instance.CitizenFemalePortraits[0];
                break;
            case NPCSpecialNames.Sheldon:
                _ReturnImage = PortraitImages.instance.CitizenMalePortraits[0];
                break;
            case NPCSpecialNames.Penny:
                _ReturnImage = PortraitImages.instance.CitizenFemalePortraits[0];
                break;
            case NPCSpecialNames.BabyBobNicely: // for television
                _ReturnImage = PortraitImages.instance.CharacterPortraits[1];
                break;
        }

        return _ReturnImage;
    }
}
