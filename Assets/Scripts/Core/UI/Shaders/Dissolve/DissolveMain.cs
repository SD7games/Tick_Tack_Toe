using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIDissolve : MonoBehaviour
{
    [SerializeField] private float _dissolveTime = 0.75f;

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

    public void PlayDissolve()
    {
        if (_dissolveRoutine != null)
            StopCoroutine(_dissolveRoutine);

        _dissolveRoutine = StartCoroutine(DissolveRoutine());
    }

    private IEnumerator DissolveRoutine()
    {
        float elapsed = 0f;
        _material.SetFloat(_dissolveAmount, 1.1f);

        while (elapsed < _dissolveTime)
        {
            elapsed += Time.deltaTime;
            float value = Mathf.Lerp(1.1f, 0f, elapsed / _dissolveTime);
            _material.SetFloat(_dissolveAmount, value);
            yield return null;
        }

        _material.SetFloat(_dissolveAmount, 0f);
    }
}
