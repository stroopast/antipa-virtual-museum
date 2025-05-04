using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVisual : MonoBehaviour
{
    [SerializeField] private GameObject FemaleCharacter;
    [SerializeField] private GameObject MaleCharacter;

    private void Hide(GameObject characterGender)
    {
        characterGender.SetActive(false);
    }

    private void Show(GameObject characterGender)
    {
        characterGender.SetActive(true);
    }

    public void SetPlayerGender(int genderId)
    {
        if(genderId == 0)
        {
            Hide(MaleCharacter);
            Show(FemaleCharacter);
        }
        else if(genderId == 1)
        {
            Hide(FemaleCharacter);
            Show(MaleCharacter);
        }
    }
}
