using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ExhibitInfoMenu : MonoBehaviour
{
    [Header("Left Page")]
    public TextMeshProUGUI geographicRange;
    public TextMeshProUGUI habitat;
    public TextMeshProUGUI lifespan;
    public TextMeshProUGUI food;

    [Header("Right Page")]
    public Image map;
    public TextMeshProUGUI echibitName;
    public TextMeshProUGUI echibitSpecies;
    public TextMeshProUGUI echibitHabitat;
    public TextMeshProUGUI echibitDiet;
    public TextMeshProUGUI echibitLifespan;

    public void ExitExhibitInfoMenu()
    {
        HelperFunctions.LockCursor();
        gameObject.SetActive(false);
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

    public void OpenExhibitInfoMenu()
    {
        HelperFunctions.UnlockCursor();
        gameObject.SetActive(true);
    }
}
