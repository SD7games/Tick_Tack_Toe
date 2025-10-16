using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Dissolve : MonoBehaviour
{
    [SerializeField]
    private float _dissolveTime = 0.75f;
    [SerializeField]
    private ContentScrollController _contentScrollController;

    private Image _image;
    private Material _material;
    private int _dissolveAmount = Shader.PropertyToID("_DissolveAmount");
    private Coroutine _dissolveRoutine;

    private void Awake()
    {
        _image = GetComponent<Image>();

        _material = Instantiate(_image.material);
        _image.material = _material;
    }

    private void OnEnable()
    {
        if (_contentScrollController != null)
            _contentScrollController.OnEmojiSelected += OnEmojiSelected;

        PlayDissolve();
    }

    private void OnDisable()
    {
        if (_contentScrollController != null)
            _contentScrollController.OnEmojiSelected -= OnEmojiSelected;

        StopAllCoroutines();
    }

    private void OnEmojiSelected(Sprite sprite)
    {
        _image.sprite = sprite;
        PlayDissolve();
    }

    public void PlayDissolve()
    {
        if (_dissolveRoutine != null)
            StopCoroutine(_dissolveRoutine);

        _dissolveRoutine = StartCoroutine(Dissolved());
    }

    private IEnumerator Dissolved()
    {
        float elapsedTime = 0f;

        _material.SetFloat(_dissolveAmount, 1.1f);

        while (elapsedTime < _dissolveTime)
        {
            elapsedTime += Time.deltaTime;
            float lerpedDissolve = Mathf.Lerp(1.1f, 0f, elapsedTime / _dissolveTime);
            _material.SetFloat(_dissolveAmount, lerpedDissolve);
            yield return null;
        }

        _material.SetFloat(_dissolveAmount, 0f);
    }
}
