using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialIcon : MonoBehaviour 
{
	public bool alreadySelected;
	public string title;
	public Image color;
	public Image icon;
	public RadialMenu myMenu;

    private void Update()
    {
        if(alreadySelected)
        {
            color.color = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        }
    }

}
