using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewExhibitData", menuName = "Exhibit/Exhibit Data")]
public class ExhibitData : ScriptableObject
{
    public string exhibitName;
    public string species;
    [TextArea] public string description;
    public Sprite image;
    public AudioClip sound;
}
