﻿// Popup list with multi-instance support created by Xiaohang Miao. (xmiao2@ncsu.edu)

using UnityEngine;
public class Popup
{

    // Represents the selected index of the popup list, the default selected index is 0, or the first item
    private int selectedItemIndex = 0;

    // Represents whether the popup selections are visible (active)
    private bool isVisible = false;

    // Represents whether the popup button is clicked once to expand the popup selections
    private bool isClicked = false;

    // If multiple Popup objects exist, this static variable represents the active instance, or a Popup object whose selection is currently expanded
    private static Popup current;

    // This function is ran inside of OnGUI()
    // For usage, see http://wiki.unity3d.com/index.php/PopupList#Javascript_-_PopupListUsageExample.js
    public int List(Rect box, GUIContent[] items)
    {
        bool isChanged = false;

        // If the instance's popup selection is visible
        if (isVisible)
        {

            // Draw a Box
            Rect listRect = new Rect(box.x, box.y + box.height, box.width, box.height * items.Length);
            GUI.Box(listRect, "");

            // Draw a SelectionGrid and listen for user selection
            var index = GUI.SelectionGrid(listRect, selectedItemIndex, items, 1);
            if(index != selectedItemIndex)
            {
                selectedItemIndex = index;
                isChanged = true;
            }

            // If the user makes a selection, make the popup list disappear
            if (GUI.changed)
            {
                current = null;
            }
        }

        // Get the control ID
        int controlID = GUIUtility.GetControlID(FocusType.Passive);

        // Listen for controls
        switch (Event.current.GetTypeForControl(controlID))
        {
            // If mouse button is clicked, set all Popup selections to be retracted
            case EventType.mouseUp:
                {
                    current = null;
                    break;
                }
        }

        // Draw a button. If the button is clicked
        if (GUI.Button(new Rect(box.x, box.y, box.width, box.height), items[selectedItemIndex]))
        {
            // If the button was not clicked before, set the current instance to be the active instance
            if (!isClicked)
            {
                current = this;
                isClicked = true;
            }
            // If the button was clicked before (it was the active instance), reset the isClicked boolean
            else
            {
                isClicked = false;
            }
        }

        // If the instance is the active instance, set its popup selections to be visible
        if (current == this)
        {
            isVisible = true;
        }

        // These resets are here to do some cleanup work for OnGUI() updates
        else
        {
            isVisible = false;
            isClicked = false;
        }

        // Return the selected item's index
        return isChanged ? selectedItemIndex : -1;
    }

    // Get the instance variable outside of OnGUI()
    public int GetSelectedItemIndex()
    {
        return selectedItemIndex;
    }
}