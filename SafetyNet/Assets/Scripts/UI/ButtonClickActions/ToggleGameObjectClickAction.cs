using UnityEngine;

public class ToggleGameObjectClickAction : BaseClickAction
{
    [SerializeField]
    private GameObject gameObj;

    [SerializeField]
    private bool allowActivation = true;

    [SerializeField]
    private bool allowDeactivation = true;

    protected override void OnButtonClicked()
    {
        if (gameObj != null)
        {
            bool _shouldActivate = (allowActivation && !gameObj.activeSelf);
            bool _shouldDeactivate = (allowDeactivation && gameObj.activeSelf);
            if(_shouldActivate || _shouldDeactivate)
            {
                gameObj.SetActive(!gameObj.activeSelf);
            }
        }
    }
}
