using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource footstepsSound;
    [SerializeField] private AudioSource winTrophySound;

    [SerializeField] private GameObject handleInputManager;

    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {
        bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        if (isMoving && !handleInputManager.GetComponent<HandleInputScript>().AreMenusActive())
        {
            if (!footstepsSound.isPlaying)
                footstepsSound.Play();

            if (Input.GetKey(KeyCode.LeftShift))
            {
                footstepsSound.pitch = 1.5f;
            }
            else
            {
                footstepsSound.pitch = 1f;
            }
        }
        else
        {
            if (footstepsSound.isPlaying)
                footstepsSound.Stop();
        }
    }

    public void PlayWinTrophySound()
    {
        winTrophySound.Play();
    }
}
