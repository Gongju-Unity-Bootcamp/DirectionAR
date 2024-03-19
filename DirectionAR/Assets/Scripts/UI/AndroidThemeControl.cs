using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AndroidThemeControl : MonoBehaviour
{
    
    public void Awake()
    {
        StatusBarControl(true);
        StatusBarColorControl(0xffff3300);
    }

    public void StatusBarControl(bool _isVisible)
    {
        ApplicationChrome.statusBarState = _isVisible ? ApplicationChrome.States.Visible : ApplicationChrome.States.Hidden;
    }

    public void StatusBarColorControl(uint _colorValue)
    {
        ApplicationChrome.statusBarColor = _colorValue;
    }

    public void NavigationBarControl(bool _isVisible)
    {
        ApplicationChrome.navigationBarState = _isVisible ? ApplicationChrome.States.Visible : ApplicationChrome.States.Hidden;
    }

    public void NavigationBarColorControl(uint _colorValue)
    {
        ApplicationChrome.navigationBarColor = _colorValue;
    }
}
