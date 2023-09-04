using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonAudio : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] AudioManager.SoundEffects sfxHighlighted = AudioManager.SoundEffects.buttonHighlight;
    [SerializeField] AudioManager.SoundEffects sfxSelected;
    [SerializeField] bool playHighlightedAudio = true;
    [SerializeField] bool playSelectedAudio = true;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => ClickButton(sfxSelected));
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (playHighlightedAudio)
        {
            AudioManager.instance.PlaySFX(sfxHighlighted);
        }
    }

    public void ClickButton(AudioManager.SoundEffects source)
    {
        if (playSelectedAudio)
        {
            AudioManager.instance.PlaySFX(source);
        }
    }
}
