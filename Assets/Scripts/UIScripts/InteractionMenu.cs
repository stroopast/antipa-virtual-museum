using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;

public class InteractionMenu : MonoBehaviour
{
    public TextMeshProUGUI exhibitName;
    public TextMeshProUGUI speciesName;
    public TextMeshProUGUI exhibitDescr;
    public Image exhibitImg;
    public AudioSource exhibitSound;

    public GameObject additionalInformationPage;

    private ExhibitData currentExhibit;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

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

    public void OpenAdditionalInformation()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        additionalInformationPage.SetActive(true);
        gameObject.SetActive(false);
    }

    public void ExitInteractionMenu()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        gameObject.SetActive(false);
    }
}
