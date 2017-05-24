using System.Collections;
using UnityEngine;
using SenecaEvents;

#region witchlightmanager.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Responsible for unflipping the camera in the Seneca Meadow scene                                                  */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void Start()                                                                                 */
/*                 private void OnTriggerEnter2D(Collider2D col)                                                        */
/*                 private IEnumerator SizeLerp()                                                                       */
/*                 private void Update()                                                                                */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class witchlightmanager1 : MonoBehaviour
{
    public bool isCameraRotating;
    public float x;                         //  x position of the witchlight
    public float y;                         //  y positon of the witchlight
    public float n;                         //  Z rotation value of the camera
    public Animator anim;
    public Camera mainCamera;

    public GameObject player;
	
	public GameObject Flare1;
	public GameObject Flare2;
	public GameObject Flare3;
	public GameObject Flare4;

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
		x = this.gameObject.transform.position.x;
		y = this.gameObject.transform.position.y;
		isCameraRotating = false;
		anim = this.gameObject.GetComponent<Animator> ();
		mainCamera = Camera.main;
	}

    #region Overview private void OnTriggerEnter2D(Collider2D col)
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Starting to move the camera out and setting the flare's animation t true                                    */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          Collider2D collider: the object you collided with                                                           */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Flare1.gameObject.GetComponent<Animator>().SetBool("Flare", true);
            Flare2.gameObject.GetComponent<Animator>().SetBool("Flare", true);
            Flare3.gameObject.GetComponent<Animator>().SetBool("Flare", true);
            Flare4.gameObject.GetComponent<Animator>().SetBool("Flare", true);

            StartCoroutine(SizeLerp());
        }
    }

    #region Overview private void SizeLerp()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Zooming out the camera during the camera flip to hide the game's edges                                      */
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

        Flare1.transform.position = new Vector3(-3.16f, 1.67f, 0);
        Flare2.transform.position = new Vector3(3.16f, 1.67f, 0);
        Flare3.transform.position = new Vector3(3.16f, -1.67f, 0);
        Flare4.transform.position = new Vector3(-3.16f, -1.67f, 0);

        isCameraRotating = true;

        float t = 0;

        while (t < 1)
        {
            mainCamera.orthographicSize = Mathf.Lerp(5, 2, t / 1.2f);
            t = t + Time.deltaTime;
        }

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
    private void Update()
    {
        if (isCameraRotating == true)
        {
            mainCamera.transform.Rotate(Vector3.forward * n * Time.deltaTime);

            if (mainCamera.transform.rotation.eulerAngles.z >= 180)
            {
                Services.Events.Fire(new SceneChangeEvent("Utan Meadow"));
                TransitionData.Instance.SENECA_MEADOW.position = player.transform.position;
                TransitionData.Instance.SENECA_MEADOW.scale = player.transform.localScale;
                Services.Scenes.Swap<UtanMeadowSceneScript>(TransitionData.Instance);
            }
        }
    }	
}
