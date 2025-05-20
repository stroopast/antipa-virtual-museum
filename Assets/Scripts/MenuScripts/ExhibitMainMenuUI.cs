using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using System;
using System.Runtime.CompilerServices;

public class ExhibitMainMenuUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI exhibitName;
    [SerializeField] private TextMeshProUGUI speciesName;
    [SerializeField] private TextMeshProUGUI exhibitDescription;
    [SerializeField] private Image exhibitImage;
    [SerializeField] private AudioSource exhibitSound;

    [SerializeField] private Button exhibitSoundButton;
    [SerializeField] private Button exhibitInformationButton;
    [SerializeField] private Button exhibitQuizButton;
    [SerializeField] private Button exhibitExitButton;

    [SerializeField] private GameObject ExhibitInfoMenuUI;
    [SerializeField] private GameObject ExhibitQuizMenuUI;

    private ExhibitData currentExhibit;

    private void Awake()
    {
        exhibitSoundButton.onClick.AddListener(() =>
        {
            PlayExhibitSound();
        });

        exhibitInformationButton.onClick.AddListener(() =>
        {
            ExhibitInfoMenuUI.gameObject.SetActive(true);
            Hide();
        });

        exhibitQuizButton.onClick.AddListener(() =>
        {
            ExhibitQuizMenuUI.gameObject.SetActive(true);
            ExhibitQuizMenuUI.GetComponent<ExhibitQuizMenu>().StartQuiz();
            Hide();
        });

        exhibitExitButton.onClick.AddListener(() =>
        {
            HelperFunctions.LockCursor();
            Hide();
        });
    }

    private void Start()
    {
        Hide();
    }

    public void LoadExhibitData(ExhibitData data)
    {
        currentExhibit = data;

        exhibitName.text = data.exhibitName;
        speciesName.text = data.species;
        exhibitDescription.text = data.description;
        exhibitImage.sprite = data.image;
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

    // remove
    public void ExitExhibitMainMenu()
    {
        HelperFunctions.LockCursor();
        Hide();
    }

    public void OpenExhibitMainMenu()
    {
        HelperFunctions.UnlockCursor();
        Show();
    }


    /// 


    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
