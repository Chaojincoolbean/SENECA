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



	void Start(){
		color.color = new Color (1f, 1f, 1f, 0f);
	}

    private void Update()
    {
		//if (GameManager.instance.sceneName.Contains ("Title")) {

		if (alreadySelected) {
			color.color = new Color (0.5f, 0.5f, 0.5f, 1.0f);
		} else {
			color.color = new Color (1f,1f,1f, 1.0f);
		}
	//	} 
			//color.color = transform.parent.GetChild (0).gameObject.GetComponent<Image> ().color;
	
    }

}
