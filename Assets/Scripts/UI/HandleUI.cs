using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleUI : MonoBehaviour
{
    [SerializeField] private GameObject startUI;
    [SerializeField] private GameObject startSingleplayerUI;
    [SerializeField] private GameObject singleplayerNewGameUI;
    [SerializeField] private GameObject singleplayerLoadUI;
    [SerializeField] private GameObject deleteSaveUI;
    [SerializeField] private GameObject character;

    private void Start()
    {
        startUI.gameObject.SetActive(true);
        startSingleplayerUI.gameObject.SetActive(false);
        singleplayerNewGameUI.gameObject.SetActive(false);
        singleplayerLoadUI.gameObject.SetActive(false);
        deleteSaveUI.gameObject.SetActive(false);
        character.gameObject.SetActive(false);
    }
}
