using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewExhibitData", menuName = "Exhibit/Exhibit Data")]
public class ExhibitData : ScriptableObject
{
    [Header("Main Information")]
    public string exhibitName;
    public string species;
    [TextArea] public string description;
    public Sprite image;
    public AudioClip sound;

    [Header("Additional Information - Left Page")]
    [TextArea] public string geographicRange;
    [TextArea] public string habitat;
    [TextArea] public string lifespan;
    [TextArea] public string food;

    [Header("Additional Information - Right Page")]
    public string habitatTitle;
    public string lifespanTitle;
    public string dietTitle;
    public Sprite map;
}
