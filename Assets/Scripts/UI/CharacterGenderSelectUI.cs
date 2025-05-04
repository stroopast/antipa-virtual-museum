using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterGenderSelectUI : MonoBehaviour
{
    [SerializeField] private Button ChooseFemaleGenderBtn;
    [SerializeField] private Button ChooseMaleGenderBtn;

    private void Awake()
    {
        ChooseFemaleGenderBtn.onClick.AddListener(() =>
        {
            MultiplayerManager.Instance.ChangePlayerGender(0);
        });
        ChooseMaleGenderBtn.onClick.AddListener(() =>
        {
            MultiplayerManager.Instance.ChangePlayerGender(1);
        });
    }

}
