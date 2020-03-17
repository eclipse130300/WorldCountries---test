using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleTween : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        LeanTween.scale(gameObject, Vector3.zero, 0.1f);
    }

    private void OnEnable()
    {
        LeanTween.scale(gameObject, Vector3.one, 0.3f);
    }
    private void OnDisable()
    {
        LeanTween.scale(gameObject, Vector3.zero, 0.1f);
    }
}
