using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectSinglePlayer : MonoBehaviour
{
    [SerializeField] private GameObject maleCharacter;
    [SerializeField] private GameObject femaleCharacter;

    public string currentGender = "female";

    private void Start()
    {
        femaleCharacter.SetActive(false);
    }

    public void SetCharacterToFemale()
    {
        femaleCharacter.SetActive(true);
        maleCharacter.SetActive(false);
        currentGender = "female";
    }

    public void SetCharacterToMale()
    {
        femaleCharacter.SetActive(false);
        maleCharacter.SetActive(true);
        currentGender = "male";
    }
}
