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
    private List<string> noSoundAnimals = new List<string> { "Tarantula mexicană", "Scorpion imperial", "Viespea gigant asiatică",
                                                             "Gândacul verde", "Fluturele Goliath", "Văduva neagră", "Călugărița",
                                                             "Crab Dungeness", "Furnica gigant", "Lăcustă cu dungi verzi", "Anaconda verde",
                                                             "Vipera cu corn", "Mamba negru", "Piton verde de copac", "Cobra", "Aligator", "Dragonul de Komodo",
                                                             "Delfin", "Caracatiță", "Orca", "Pește balon", "Somon",
                                                             "Marele alb", "Pisică de mare", "Sturion", "Pește spadă", "Ton"};

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
            ExhibitQuizMenuUI.GetComponent<ExhibitQuizMenuUI>().StartQuiz();
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

        exhibitSoundButton.gameObject.SetActive(true);

        // If can't play a animal sound beacuse it produce no sound e.g. spider -> remove play sound button and rearrange the other 2 buttons
        Vector3 currentQuizBtnPosition = exhibitQuizButton.GetComponent<RectTransform>().anchoredPosition;
        Vector3 currentInfoBtnPosition = exhibitInformationButton.GetComponent<RectTransform>().anchoredPosition;
        foreach (string name in noSoundAnimals)
        {
            if (name == exhibitName.text)
            {
                exhibitSoundButton.gameObject.SetActive(false);
                currentQuizBtnPosition.y = -5;
                currentInfoBtnPosition.y = 135;
                exhibitQuizButton.GetComponent<RectTransform>().anchoredPosition = currentQuizBtnPosition;
                exhibitInformationButton.GetComponent<RectTransform>().anchoredPosition = currentInfoBtnPosition;
                return;
            }
        }

        currentQuizBtnPosition.y = -55;
        currentInfoBtnPosition.y = 185;
        exhibitQuizButton.GetComponent<RectTransform>().anchoredPosition = currentQuizBtnPosition;
        exhibitInformationButton.GetComponent<RectTransform>().anchoredPosition = currentInfoBtnPosition;
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

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
