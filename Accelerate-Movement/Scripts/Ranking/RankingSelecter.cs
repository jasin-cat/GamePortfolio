using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class RankingSelecter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image _focus;

    void Awake()
    {
        _focus?.gameObject.SetActive(false);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _focus?.gameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _focus?.gameObject.SetActive(false);
    }
}