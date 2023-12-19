using DG.Tweening;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapTransition : MonoBehaviour
{
    private Image _mapTransitionImage;

    private void Awake()
    {
        _mapTransitionImage = GetComponent<Image>();
    }
    private void Start()
    {
        _mapTransitionImage.DOFade(0, 2);
    }
    public void LoadingMap()
    {
        _mapTransitionImage.DOFade(255, 2);
    }
    public void LoadedMap()
    {
        _mapTransitionImage.DOFade(0, 2);
    }
}
