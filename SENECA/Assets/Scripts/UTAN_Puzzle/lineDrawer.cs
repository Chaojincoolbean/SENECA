using UnityEngine;

#region DEPRECIATED lineDrawer.cs Overview DEPRECIATED
/************************************************************************************************************************/
/*                                                                                                                      */
/*    DEPRECIATED Drawing a  line for the HARTO puzzle  DEPRECIATED                                                     */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void Start()                                                                                 */
/*                 private void Update ()                                                                               */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class lineDrawer : MonoBehaviour
{
	private GameObject clone;
	private LineRenderer line;
	private int i;
	public  GameObject tf;

    #region Overview private void Start()
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
		tf = this.gameObject;
		i = 0;
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
		
		if(Input.GetMouseButtonDown(0))
        {
			clone = (GameObject)Instantiate(tf,tf.transform.position, transform.rotation);

			line = clone.GetComponent<LineRenderer>();
			line.SetColors(Color.white,Color.white);
			line.SetWidth(0.1f,0.1f);
			i = 0;

		}

		if (Input.GetMouseButton(0))
        {
			i ++;
			line.SetVertexCount(i);
			line.SetPosition(i - 1, Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 15)));
		}
		
	}
}
