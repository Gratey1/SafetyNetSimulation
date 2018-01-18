using UnityEngine;
using System;
using System.Collections;

public class MenuObject : MonoBehaviour 
{
    //-------------------------------------------------------------------------------

    [Serializable]
    public class NavigationOption
    {
        [SerializeField]
        private MenuObject option;
        
        [SerializeField]
        private bool useAutomaticDirection = true;
        
        [SerializeField]
        private Vector2 manualDirection;

        [SerializeField]
        private float breakAngle = 45.0f;

        //-------------------------------------------------------------------------------

        public MenuObject Option
        {
            get { return option; }
        }

        public bool UseAutomaticDirection
        {
            get { return useAutomaticDirection; }
        }

        public Vector2 ManualDirection
        {
            get { return manualDirection; }
        }

        public float BreakAngle
        {
            get { return breakAngle; }
        }

        //-------------------------------------------------------------------------------

        public NavigationOption(MenuObject _option, float _breakAngle)
        {
            option = _option;
            useAutomaticDirection = true;
            manualDirection = Vector2.zero;
            breakAngle = _breakAngle;
        }

        public NavigationOption(MenuObject _option, Vector2 _manualDirection, float _breakAngle)
        {
            option = _option;
            useAutomaticDirection = false;
            manualDirection = _manualDirection;
            breakAngle = _breakAngle;
        }
    }

    //-------------------------------------------------------------------------------

    public delegate void ActionReceivedHandler(MenuAction _action);
    public event ActionReceivedHandler ActionReceived;
    public void TriggerActionReceived(MenuAction _action) { if (ActionReceived != null) ActionReceived(_action); }

    //-------------------------------------------------------------------------------

    [SerializeField]
    private NavigationOption[] navOptions;
    private bool isFocused = false;
    private bool isLocked = false;

    //-------------------------------------------------------------------------------

    public bool IsFocused { get { return isFocused; } }
    public bool IsLocked 
    { 
        get { return isLocked || !isActiveAndEnabled; }
        set { isLocked = value; }
    }

    //-------------------------------------------------------------------------------

	void OnEnable () 
    {
        ActionReceived += OnActionReceived;
	}

    void OnDisable()
    {
        ActionReceived -= OnActionReceived;
    }

    void OnActionReceived(MenuAction _action)
    {
        switch (_action)
        {
            case MenuAction.Focus:
                {
                    isFocused = true;
                    break;
                }
            case MenuAction.Unfocus:
                {
                    isFocused = false;
                    break;
                }
            /*
        case MenuAction.Submit:
            {
                break;
            }
        case MenuAction.Cancel:
            {
                break;
            }
            */
            default:
                {
                    break;
                }
        }
    }

    public NavigationOption GetNavigationOption(Vector2 _dir)
    {
        Vector2 _normalizedDir = _dir.normalized;
        NavigationOption _bestOption = null;
        float _minAngle = float.MaxValue;
        foreach(NavigationOption no in navOptions)
        {
            if (no == null || no.Option == null) continue;

            Vector2 _dir2;
            if(no.UseAutomaticDirection)
            {
                _dir2 = (no.Option.transform.position - transform.position).normalized;
            }
            else
            {
                _dir2 = no.ManualDirection.normalized;
            }

            float _curAngle = Vector2.Angle(_normalizedDir, _dir2);
            if(_curAngle < _minAngle && _curAngle <= no.BreakAngle)
            {
                _minAngle = _curAngle;
                _bestOption = no;
            }
        }

        return _bestOption;
    }

    //-------------------------------------------------------------------------------
}
