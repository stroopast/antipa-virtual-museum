using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WelcomeMenuUI : MonoBehaviour
{

    [SerializeField] private Button exitBtn;

    private void Update()
    {
        HelperFunctions.UnlockCursor();
    }
    private void Awake()
    {
        exitBtn.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
            HelperFunctions.LockCursor();
        });
    }
}
