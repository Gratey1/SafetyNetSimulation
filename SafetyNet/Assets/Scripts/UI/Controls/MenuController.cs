using UnityEngine;
using System.Collections;

public enum MenuAction
{
    None = 0,
    Focus = 1,
    Unfocus = 2,
    Submit = 3,
    Cancel = 4,
}

public class MenuController : MonoBehaviour 
{
    [SerializeField]
    private float deadZone = 0.25f;

    [SerializeField]
    private float scrollDelay = 0.25f;
    private float nextNavTime = -1.0f;

    private MenuObject curMenuObj;
    private MenuObject prevMenuObj;
    
    // Input
    private bool isSubmitting = false;
    private bool isCanceling = false;
    private bool isNavigating = false;
    private Vector2 navDirection;
  
    public Vector2 NavDirection { get { return navDirection; } }
    public MenuObject CurMenuObj { get { return curMenuObj; } }

	void LateUpdate () 
    {
        if (curMenuObj == null) return;

        CheckInput();

        if(isSubmitting)
        {
            DoSubmit();
            return;
        }
        
        if(isCanceling)
        {
            DoCancel();
            return;
        }
        
        if(isNavigating)
        {
            DoNavigation();
            return;
        }
	}

    void CheckInput()
    {
        isSubmitting = Input.GetKeyDown(KeyCode.Return);
        isCanceling = Input.GetKeyDown(KeyCode.RightShift);

        navDirection = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            navDirection += Vector2.up;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            navDirection -= Vector2.up;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            navDirection -= Vector2.right;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            navDirection += Vector2.right;
        }

        if(navDirection.magnitude < deadZone)
        {
            navDirection = Vector2.zero;
            isNavigating = false;
        }
        else if (Time.time >= nextNavTime)
        {
            nextNavTime += scrollDelay;
            isNavigating = true;
        }
    }

    void DoSubmit()
    {
        curMenuObj.TriggerActionReceived(MenuAction.Submit);
    }

    void DoCancel()
    {
        curMenuObj.TriggerActionReceived(MenuAction.Cancel);
    }

    void DoNavigation()
    {
        SetFocusedMenuObj(GetBestNavigationOption(curMenuObj));
    }

    MenuObject GetBestNavigationOption(MenuObject _menuObj)
    {
        if (_menuObj == null) return null;

        MenuObject.NavigationOption no = _menuObj.GetNavigationOption(navDirection);
        if (no == null || no.Option == null) return null;
        if (!no.Option.IsLocked) return no.Option;

        return GetBestNavigationOption(no.Option);
    }

    public void SetFocusedMenuObj(MenuObject _nextMenuObj)
    {
        if(curMenuObj != null)
        {
            curMenuObj.TriggerActionReceived(MenuAction.Unfocus);
        }

        if(_nextMenuObj != null)
        {
            _nextMenuObj.TriggerActionReceived(MenuAction.Focus);
        }

        prevMenuObj = curMenuObj;
        curMenuObj = _nextMenuObj;
    }
}
