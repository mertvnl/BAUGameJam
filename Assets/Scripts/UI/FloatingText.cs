using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FloatingText : MonoBehaviour
{
    private TextMeshPro _text;
    public TextMeshPro Text => _text ??= GetComponentInChildren<TextMeshPro>();

    public void Initialize(string text, Color color)
    {
        Text.DOFade(1, 0);
        Text.SetText(text);
        Text.color = color;
        Animation();
    }

    private void Animation()
    {
        transform.DOMoveY(transform.position.y + 0.5f, 0.3f).OnComplete(()=> PoolingSystem.Instance.DestroyPoolObject(gameObject));
        Text.DOFade(0, 0.15f).SetDelay(0.15f);
    }
}
