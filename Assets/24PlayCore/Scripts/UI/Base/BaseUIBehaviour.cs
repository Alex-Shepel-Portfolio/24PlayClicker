using UnityEngine;
using UnityEngine.UI;

namespace TFPlay.UI
{
    public class BaseUIBehaviour : MonoBehaviour
    {
        [SerializeField] private bool hideOnInit;
        [SerializeField] private GameObject content;

        public event System.Action OnShow;
        public event System.Action OnHide;

        protected virtual void OnValidate()
        {

        }

        protected virtual void Awake()
        {

        }

        protected virtual void Start()
        {
            GameC.Instance.OnInitCompleted += Init;
        }

        protected virtual void Init()
        {
            if (hideOnInit)
            {
                HideInstant();
            }
        }

        public virtual void Show()
        {
            content.SetActive();
            OnShow?.Invoke();
        }

        public virtual void Hide()
        {
            HideInstant();
        }

        public virtual void HideInstant()
        {
            content.SetInactive();
            OnHide?.Invoke();
        }
    }
}