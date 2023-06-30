using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentObject : MonoBehaviour
{
    public bool _isTransparent { get; private set; } = false;

    private MeshRenderer[] _renderers;
    private WaitForSeconds delay = new WaitForSeconds(0.001f);
    private WaitForSeconds resetDelay = new WaitForSeconds(0.005f);
    private const float THRESHOLD_ALPHA = 0.25f;
    private const float THRESHOLD_MAX_TIMER = 0.5f;

    private bool _isResting = false;
    private float timer = 0f;
    private Coroutine timeCheckCoroutine;
    private Coroutine resetCoroutine;
    private Coroutine becomeTransparentCoroutine;

    Collider Col;

    private void Awake()
    {
        _renderers = GetComponentsInChildren<MeshRenderer>();
        Col = GetComponent<Collider>();
    }

    public void BecomeTransparent()
    {

        if (_isTransparent)
        {
            timer = 0f;
            return;
        }

        if (resetCoroutine != null && _isResting)
        {
            _isResting = false;
            _isTransparent = false;
            StopCoroutine(resetCoroutine);
        }

        SetMaterialTransparent();
        _isTransparent = true;
        becomeTransparentCoroutine = StartCoroutine(BecomeTransparentCoroutine());
    }

    private void SetMaterialRenderingMode(Material material, float mode, int renderQueue)
    {
        material.SetFloat("_Mode", mode);
        material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        material.SetInt("ZWrite", 0);
        material.DisableKeyword("_ALPHATEST_ON");
        material.EnableKeyword("_ALPHABLEND_ON");
        material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        material.renderQueue = renderQueue;
    }

    private void SetMaterialTransparent()
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            foreach (Material material in _renderers[i].materials)
            {
                SetMaterialRenderingMode(material, 3f, 3000);
            }
        }
    }

    private void SetMaterialOpaque()
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            foreach (Material material in _renderers[i].materials)
            {
                SetMaterialRenderingMode(material, 0f, -1);
            }
        }
    }

    public void ResetOriginalTransparent()
    {
        SetMaterialOpaque();
        resetCoroutine = StartCoroutine(ResetOriginalTransparentCoroutine());
    }

    private IEnumerator BecomeTransparentCoroutine()
    {
        while (true)
        {
            bool isComplete = true;
            for (int i = 0; i < _renderers.Length; i++)
            {
                if (_renderers[i].material.color.a > THRESHOLD_ALPHA)
                {
                    isComplete = false;

                    Color color = _renderers[i].material.color;
                    color.a -= Time.deltaTime;
                    _renderers[i].material.color = color;
                    Col.enabled = false;
                }
            }

            if (isComplete)
            {
                CheckTimer();
                break;
            }

            yield return delay;
        }
    }

    private IEnumerator ResetOriginalTransparentCoroutine()
    {
        _isTransparent = false;

        while (true)
        {
            bool isComplete = true;
            for (int i = 0; i < _renderers.Length; i++)
            {
                if (_renderers[i].material.color.a < 1f)
                { isComplete = false; }

                Color color = _renderers[i].material.color;
                color.a += Time.deltaTime;
                _renderers[i].material.color = color;
                Col.enabled = true;
            }

            if (isComplete)
            {
                _isResting = false;
                break;
            }
            yield return resetDelay;
        }
    }

    public void CheckTimer()
    {
        if (timeCheckCoroutine != null)
        {
            StopCoroutine(timeCheckCoroutine);
        }
        timeCheckCoroutine = StartCoroutine(CheckTimerCoroutine());
    }

    private IEnumerator CheckTimerCoroutine()
    {
        timer = 0;

        while (true)
        {
            timer += Time.deltaTime;
            if (timer > THRESHOLD_MAX_TIMER)
            {
                _isResting = true;
                ResetOriginalTransparent();
                break;
            }
            yield return null;
        }
    }
}
