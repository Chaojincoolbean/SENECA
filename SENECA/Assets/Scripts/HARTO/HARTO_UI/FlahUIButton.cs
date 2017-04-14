using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlahUIButton : MonoBehaviour 
{

	public float _Boundary;				//	How far the image travels from its origin
	public float _Speed;					//	How fast the image moves
	private float _Top;						//	Refernece to top boundary
	private float _Bottom;					//	Reference to bottom boundary
	private Vector3 _Position;
	private Image _UIIcon;
	// Use this for initialization
	void Start () 
	{
		_UIIcon = GetComponent<Image>();
		_UIIcon.color = new Color(_UIIcon.color.r, _UIIcon.color.g, _UIIcon.color.b, 0.0f);
		_Position = GetComponent<RectTransform>().localPosition;
		_Top = _Position.y + _Boundary;
		_Bottom = _Position.y - _Boundary;
	}
	
	// Update is called once per frame
	void Update () 
	{
		_UIIcon.color = new Color(_UIIcon.color.r, _UIIcon.color.g, _UIIcon.color.b, Mathf.PingPong(Time.time, 1.5f));

		_Position.y+= _Speed; 

			if (_Position.y > _Top || _Position.y < _Bottom)
			{
				_Speed *= -1;
			}

			_UIIcon.transform.localPosition = _Position;
	}
}
