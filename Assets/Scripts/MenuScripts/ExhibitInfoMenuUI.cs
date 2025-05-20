using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ExhibitInfoMenuUI : MonoBehaviour
{
    [SerializeField] private Button exitBtn;
    [SerializeField] private Button backBtn;

    [SerializeField] private GameObject ExhibitMainMenuUI;

    [Header("Left Page")]
    [SerializeField] private TextMeshProUGUI geographicRange;
    [SerializeField] private TextMeshProUGUI habitat;
    [SerializeField] private TextMeshProUGUI lifespan;
    [SerializeField] private TextMeshProUGUI food;

    [Header("Right Page")]
    [SerializeField] private Image map;
    [SerializeField] private TextMeshProUGUI echibitName;
    [SerializeField] private TextMeshProUGUI echibitSpecies;
    [SerializeField] private TextMeshProUGUI echibitHabitat;
    [SerializeField] private TextMeshProUGUI echibitDiet;
    [SerializeField] private TextMeshProUGUI echibitLifespan;

    private void Awake()
    {
        exitBtn.onClick.AddListener(() =>
        {
            HelperFunctions.LockCursor();
            Hide();
        });

        backBtn.onClick.AddListener(() =>
        {
            ExhibitMainMenuUI.gameObject.SetActive(true);
            Hide();
        });
    }

    private void Start()
    {
        Hide();
    }

    public void LoadExhibitAdditionalInfo(ExhibitData data)
    {

        // left page load
        geographicRange.text = data.geographicRange;
        habitat.text = data.habitat;
        lifespan.text = data.lifespan;
        food.text = data.food;

        // right page load
        echibitName.text = data.exhibitName;
        echibitSpecies.text = data.species;
        echibitHabitat.text = data.habitatTitle;
        echibitDiet.text = data.dietTitle;
        echibitLifespan.text = data.lifespanTitle;
        map.sprite = data.map;
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
