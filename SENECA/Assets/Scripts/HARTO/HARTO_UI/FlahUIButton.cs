using UnityEngine;
using UnityEngine.UI;

#region FlahUIButton.cs Overview
/*********************************************************************************************************************************************************/
/*                                                                                                                                                       */
/*    Moves UI icons up and down and fades their transparency                                                                                            */
/*                                                                                                                                                       */
/*    Function List as of 5/20/2017:                                                                                                                     */
/*          private:                                                                                                                                     */
/*                 private void Start ()                                                                                                                 */
/*                 private void Update()                                                                                                                 */
/*                                                                                                                                                       */
/*********************************************************************************************************************************************************/
#endregion
public class FlahUIButton : MonoBehaviour 
{
	public float _Boundary;				//	How far the image travels from its origin
	public float _Speed;					//	How fast the image moves
	private float _Top;						//	Refernece to top boundary
	private float _Bottom;					//	Reference to bottom boundary
	private Vector3 _Position;
	private Image _UIIcon;

    #region Overview protected void Start()
    /************************************************************************************************************************/
    /*    Responsible for:                                                                                                  */
    /*      Initalizing variables. Runs once at the beginning of the program                                                */
    /*                                                                                                                      */
    /*    Parameters:                                                                                                       */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*    Returns:                                                                                                          */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void Start () 
	{
		_UIIcon = GetComponent<Image>();
		if(!transform.name.Contains("Mask"))
		{
			_UIIcon.color = new Color(_UIIcon.color.r, _UIIcon.color.g, _UIIcon.color.b, 0.0f);
		}
		else
		{
			_UIIcon.color = new Color(_UIIcon.color.r, _UIIcon.color.g, _UIIcon.color.b, 1.0f);
		}
		_Position = GetComponent<RectTransform>().localPosition;
		_Top = _Position.y + _Boundary;
		_Bottom = _Position.y - _Boundary;
	}

    #region Overview private void Update()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Running once per frame					                                                                    */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void Update () 
	{
		if(!transform.name.Contains("Mask"))
		{
            //  Fades transparency in and out
			_UIIcon.color = new Color(_UIIcon.color.r, _UIIcon.color.g, _UIIcon.color.b, Mathf.PingPong(Time.time, 1.5f));
		}
		_Position.y += _Speed; 

		if (_Position.y > _Top || _Position.y < _Bottom)
		{
			_Speed *= -1;
		}

		_UIIcon.transform.localPosition = _Position;
	}
}
