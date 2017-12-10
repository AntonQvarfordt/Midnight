using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class CursorEffects : MonoBehaviour {

    public Image InnerSprite;
    public Image OuterSprite;

    Color InnerSpriteStartColor;
    Color OuterSpriteStartColor;

    private bool _playing;

    private void Awake()
    {
        InnerSpriteStartColor = InnerSprite.color;
        OuterSpriteStartColor = OuterSprite.color;
    }

    public void Bop (Transform controller)
    {
        if (_playing)
            return;

        _playing = true;
        controller.localScale = Vector3.one * 4;
        controller.DOScale(1, 0.5f).SetEase(Ease.InBack);

        OuterSprite.color = Color.magenta;
        OuterSprite.DOColor(OuterSpriteStartColor, 1f);

        InnerSprite.color = Color.cyan;
        InnerSprite.DOColor(InnerSpriteStartColor, 1f).OnComplete(() => _playing = false);
    }
}
