using UnityEngine;
using TFPlay.UI;

public class UnparentEmojiRenderCamera : MonoBehaviour
{
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private Transform cam;

    private BaseUIBehaviour uIBehaviour;

    private void Awake()
    {
        uIBehaviour = GetComponentInParent<BaseUIBehaviour>();
    }

    private void OnEnable()
    {
        uIBehaviour.OnShow += UIBehaviour_OnShow;
        uIBehaviour.OnHide += UIBehaviour_OnHide;
    }

    private void UIBehaviour_OnShow()
    {
        transform.SetParent(GameC.Instance.transform);
        transform.localScale = Vector3.one;
        transform.position = offset;
        cam.SetActive();
    }

    private void UIBehaviour_OnHide()
    {
        cam.SetInactive();
    }
}
