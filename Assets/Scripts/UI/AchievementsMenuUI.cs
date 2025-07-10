using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchivementsMenuUI : MonoBehaviour
{
    [SerializeField] private Button insectsBtn;
    [SerializeField] private Button reptilesBtn;
    [SerializeField] private Button birdsBtn;
    [SerializeField] private Button fishBtn;
    [SerializeField] private Button mammalsBtn;
    [SerializeField] private Button dinosaurBtn;
    [SerializeField] private Button specialBtn;

    [SerializeField] private GameObject insectsAchievementsMenu;
    [SerializeField] private GameObject reptilesAchievementsMenu;
    [SerializeField] private GameObject birdsAchievementsMenu;
    [SerializeField] private GameObject fishAchievementsMenu;
    [SerializeField] private GameObject mammalsAchievementsMenu;
    [SerializeField] private GameObject dinosaurAchievementsMenu;
    [SerializeField] private GameObject specialAchievementsMenu;

    private void Awake()
    {
        insectsBtn.onClick.AddListener(() =>
        {
            Hide();
            insectsAchievementsMenu.gameObject.SetActive(true);
        });

        reptilesBtn.onClick.AddListener(() =>
        {
            Hide();
            reptilesAchievementsMenu.gameObject.SetActive(true);
        });

        birdsBtn.onClick.AddListener(() =>
        {
            Hide();
            birdsAchievementsMenu.gameObject.SetActive(true);
        });

        fishBtn.onClick.AddListener(() =>
        {
            Hide();
            fishAchievementsMenu.gameObject.SetActive(true);
        });

        mammalsBtn.onClick.AddListener(() =>
        {
            Hide();
            mammalsAchievementsMenu.gameObject.SetActive(true);
        });

        dinosaurBtn.onClick.AddListener(() =>
        {
            Hide();
            dinosaurAchievementsMenu.gameObject.SetActive(true);
        });

        specialBtn.onClick.AddListener(() =>
        {
            Hide();
            specialAchievementsMenu.gameObject.SetActive(true);
        });
    }

    private void Start()
    {
        Hide();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
