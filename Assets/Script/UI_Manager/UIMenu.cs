using DG.Tweening;
using UnityEngine;

public class UIMenu : MonoBehaviour
{

    [SerializeField] private CanvasGroup _canvasGroup = null;

    void Awake()
    {
        if (_canvasGroup == null)
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            if (_canvasGroup == null)
            {
                Debug.LogError("CanvasGroup is not assigned and could not be found on the GameObject.");
            }
        }
    }

    public virtual void Show()
    {
        Debug.Log("Show");

        if (_canvasGroup != null)
        {
            gameObject.SetActive(true);
            _canvasGroup.DOFade(1.0f, 0.3f).SetEase(Ease.OutSine);
        }
        else
        {
            Debug.LogError("CanvasGroup is null. Cannot show menu.");
        }
    }

    public virtual void Hide()
    {
        Debug.Log("HIDE");
        if (_canvasGroup != null)
        {
            _canvasGroup.DOFade(0.0f, 0.3f).SetEase(Ease.OutSine).OnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
        else
        {
            Debug.LogError("CanvasGroup is null. Cannot hide menu.");
        }
    }
}
