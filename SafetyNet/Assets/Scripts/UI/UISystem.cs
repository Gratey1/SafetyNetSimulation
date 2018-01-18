using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum ScreenType
{
    None = 0,
    
    BattleHud = 1,
    CharacterSelectScreen = 2,
    TeamSelectScreen = 3,
    ContextSelectionScreen = 4,
    TextInputScreen = 5,

    PlaceholderScreen = 999,
}

public class UISystem : MonoBehaviour 
{
    private static UISystem instance;

    [SerializeField]
    private BaseScreen[] screenPrefabs;
    private Dictionary<ScreenType, BaseScreen> allScreens;
    private List<BaseScreen> activeScreens;

    public static UISystem Instance { get { return instance; } }

	void Awake () 
    {
        if(UISystem.instance != null)
        {
            Debug.LogError("Can't create multiple instances of singleton UISystem!");
            return;
        }

        UISystem.instance = this;

        allScreens = new Dictionary<ScreenType, BaseScreen>();
        activeScreens = new List<BaseScreen>();
	    foreach(BaseScreen bs in screenPrefabs)
        {
            BaseScreen screen = Instantiate(bs);
            screen.transform.SetParent(transform);
            allScreens.Add(bs.Type, screen);
            screen.Hide();
            //bs.Hide();
           // allScreens.Add(bs.Type, bs);
        }
	}

    public BaseScreen PushScreen(ScreenType _type)
    {
        if(allScreens.ContainsKey(_type))
        {
            BaseScreen bs = allScreens[_type];
            bs.Show();
            activeScreens.Insert(0, bs);
            return bs;
        }

        return null;
    }

    public BaseScreen PopScreen(ScreenType _type, bool _popAllScreensAbove = false)
    {
        bool _isFound = false;
        BaseScreen _returnScreen = null;
        int i = 0;
        for (i = 0; i < activeScreens.Count; i++)
        {
            BaseScreen bs = activeScreens[i];
            if(_type == bs.Type)
            {
                _isFound = true;
                _returnScreen = bs;
                break;
            }
        }

        if(_isFound)
        {
            if(_popAllScreensAbove)
            {
                for (; i >= 0; i--)
                {
                    BaseScreen bs = activeScreens[i];
                    bs.Hide();
                    bs.Shutdown();
                    activeScreens.RemoveAt(i);
                }
            }
            else
            {
                BaseScreen bs = activeScreens[i];
                bs.Hide();
                bs.Shutdown();
                activeScreens.RemoveAt(i);
            }
        }

        return _returnScreen;
    }

    public BaseScreen GetScreen(ScreenType _type)
    {
        if(allScreens.ContainsKey(_type))
        {
            return allScreens[_type];
        }
        return null;
    }

    public BaseScreen GetActiveScreen(ScreenType _type)
    {
        foreach(BaseScreen bs in activeScreens)
        {
            if(bs.Type == _type)
            {
                return bs;
            }
        }
        return null;
    }
}



