public class PauseClickAction : BaseClickAction
{
    protected override void OnButtonClicked()
    {
        GameManager.Instance.Pause();
    }
}
