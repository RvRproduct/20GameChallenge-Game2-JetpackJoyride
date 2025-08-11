using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    [SerializeField] private Sprite unMuted;
    [SerializeField] private Sprite muted;
    private Button button;

    private Image audioImage;

    private void Awake()
    {
        audioImage = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(MuteAndUnMuteAudio);
    }

    private void Start()
    {
        if (GameManager.Instance.GetIsSoundEffectsMuted()
            && GameManager.Instance.GetIsMusicMuted())
        {
            audioImage.sprite = muted;
        }
        else
        {
            audioImage.sprite = unMuted;
        }
    }

    public void MuteAndUnMuteAudio()
    {
        if (GameManager.Instance.GetIsSoundEffectsMuted()
            && GameManager.Instance.GetIsMusicMuted())
        {
            GameManager.Instance.SetIsMusicMuted(false);
            GameManager.Instance.SetIsSoundEffectsMuted(false);
            audioImage.sprite = unMuted;
        }
        else
        {
            GameManager.Instance.SetIsMusicMuted(true);
            GameManager.Instance.SetIsSoundEffectsMuted(true);
            audioImage.sprite = muted;
        }

        EventSystem.current.SetSelectedGameObject(null);
    }
}
