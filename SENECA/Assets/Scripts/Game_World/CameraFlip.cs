using System.Collections;
using UnityEngine;

#region CameraFlip.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Responsible for flipping the camera in the Utan Meadow scene                                                      */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void Start()                                                                                 */
/*                 private IEnumerator SizeLerp()                                                                       */
/*                 private void Update()                                                                                */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class CameraFlip : MonoBehaviour
{
    public bool isChangeSize;
    public bool isCameraRotating;
    public float n;                     //  The value used to rotate the camera
	public Camera FlipCamera;           //  The camera that does the flipping
	public Camera MainCamera;           //  Reference to the main camera
	public GameObject Flare1;           //  The camera flares
	public GameObject Flare2;           //  The camera flares
    public GameObject Flare3;           //  The camera flares
    public GameObject Flare4;           //  The camera flares

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
		isChangeSize = true;
		isCameraRotating = true;

		Flare1.gameObject.GetComponent<Animator> ().SetBool("FlareReverse", true);
		Flare2.gameObject.GetComponent<Animator> ().SetBool("FlareReverse", true);
		Flare3.gameObject.GetComponent<Animator> ().SetBool("FlareReverse", true);
		Flare4.gameObject.GetComponent<Animator> ().SetBool("FlareReverse", true);

		MainCamera = Camera.main;

		MainCamera.enabled = false;
		FlipCamera.enabled = true;
	}

    #region Overview private void SizeLerp()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Zooming in the camera during the camera flip to hide the game's edges and moving the camera flares closer   */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          The enumerator of the type                                                                                  */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private IEnumerator SizeLerp()
    {
        yield return new WaitForSeconds(1);

        float t = 0;

        while (t < 1)
        {
            FlipCamera.orthographicSize = Mathf.Lerp(2, 3.5f, t);

            t = t + Time.deltaTime;

            if (FlipCamera.orthographicSize >= 3f)
            {

                FlipCamera.orthographicSize = 5f;

                Flare1.transform.position = new Vector3(-8.64f, 4.66f, 0);
                Flare2.transform.position = new Vector3(8.64f, 4.66f, 0);
                Flare3.transform.position = new Vector3(8.64f, -4.66f, 0);
                Flare4.transform.position = new Vector3(-8.64f, -4.66f, 0);
            }
        }

        FlipCamera.enabled = false;
        MainCamera.enabled = true;
        MainCamera.orthographicSize = 5f;
        MainCamera.transform.eulerAngles = new Vector3(0, 0, 0);

        yield return null;
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
		if (isChangeSize == true)
        {
            //  Start the lerping once we are changing sizes
			StartCoroutine (SizeLerp());
			isChangeSize = false;
		}

		if (isCameraRotating == true)
        {
            //  Continue to rotate the camera unless told not to
			transform.Rotate (Vector3.forward * n * Time.deltaTime);	
		}

		if (transform.rotation.eulerAngles.z <= 5)
        {	
			isCameraRotating = false;
            //  lock the comera's rotation when the camera has flipped about 180 degress.
			transform.eulerAngles = new Vector3(0,0,0);
		}
	}
}
