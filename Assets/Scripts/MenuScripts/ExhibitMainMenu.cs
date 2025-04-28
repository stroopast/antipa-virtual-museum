using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class ExhibitMainMenu : MonoBehaviour
{
    public TextMeshProUGUI exhibitName;
    public TextMeshProUGUI speciesName;
    public TextMeshProUGUI exhibitDescr;
    public Image exhibitImg;
    public AudioSource exhibitSound;

    private ExhibitData currentExhibit;

    public void LoadExhibitData(ExhibitData data)
    {
        currentExhibit = data;

        exhibitName.text = data.exhibitName;
        speciesName.text = data.species;
        exhibitDescr.text = data.description;
        exhibitImg.sprite = data.image;
    }

    public void PlayExhibitSound()
    {
        if (currentExhibit != null && currentExhibit.sound != null)
        {
            exhibitSound.clip = currentExhibit.sound;
            exhibitSound.Play();
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ExitExhibitMainMenu()
    {
        HelperFunctions.LockCursor();
        gameObject.SetActive(false);
    }

    public void OpenExhibitMainMenu()
    {
        HelperFunctions.UnlockCursor();
        gameObject.SetActive(true);
    }
}
