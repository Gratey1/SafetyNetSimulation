public class PlayClickAction : BaseClickAction
{
    protected override void OnButtonClicked()
    {
        GameManager.Instance.Play();
    }
}
