using UnityEngine;

#region NPC.cs Overview
/************************************************************************************************************************/
/*                                                                                                                      */
/*    Movement of the Locals is controls by this script                                                                 */
/*                                                                                                                      */
/*    Function List as of 5/20/2017:                                                                                    */
/*          private:                                                                                                    */
/*                 private void Start()                                                                                 */
/*                 private void RandomizeNPCMovement()                                                                  */
/*                 private void wheretostart()                                                                          */
/*                 private void Update ()                                                                               */
/*                                                                                                                      */
/************************************************************************************************************************/
#endregion
public class NPC : MonoBehaviour
{
	private Vector3 startPos;
	private Vector3 endPos;
	public float distance;
	public float lerptime;

	private float currentLerptime;
	private bool n;                         //  changes the direction the Local moves in

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
    void Start ()
    {
		startPos = this.gameObject.transform.position;
		endPos = this.gameObject.transform.position + Vector3.right * distance;
	}

    #region Overview private void RandomizeNPCMovement()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Randomizes the start and end positon for the Local     	    				                                */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void RandomizeNPCMovement()
    {
        lerptime = Random.Range(0.75f, 3.0f);
        distance = Random.Range(0.75f, 8.0f);
    }

    #region Overview private void wheretostart()
    /************************************************************************************************************************/
    /*                                                                                                                      */
    /*      Responsible for:                                                                                                */
    /*          Setting the start and end position of the Local's movement					                                */
    /*                                                                                                                      */
    /*      Parameters:                                                                                                     */
    /*          None                                                                                                        */
    /*                                                                                                                      */
    /*      Returns:                                                                                                        */
    /*          Nothing                                                                                                     */
    /*                                                                                                                      */
    /************************************************************************************************************************/
    #endregion
    private void wheretostart()
    {
        if (this.gameObject.transform.position == startPos)
        {
            n = true;
            if (Random.Range(0, 100) < 80.0f)
            {
                RandomizeNPCMovement();
            }
            currentLerptime = 0;
        }

        if (this.gameObject.transform.position == endPos)
        {
            n = false;
            if (Random.Range(0, 100) < 10.0f)
            {
                RandomizeNPCMovement();
            }
            currentLerptime = 0;
        }
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
		wheretostart ();

		if (n == true)
        {
			currentLerptime += Time.deltaTime/0.5f;
			if (currentLerptime >= lerptime)
            {
				currentLerptime = lerptime;
			}

            //  Perc is the percentage of the way the Local is from the start position and end position
			float Perc = currentLerptime / lerptime;
            //  Moves the Local between the start position and the end position
			this.transform.position = Vector3.Lerp (startPos, endPos, Perc);
		}

		if (n == false)
        {
			currentLerptime += Time.deltaTime;
			if (currentLerptime >= lerptime)
            {
				currentLerptime = lerptime;
			}

            //  Perc is the percentage of the way the Local is from the start position and end position
            float Perc = currentLerptime / lerptime;
            //  Moves the Local between the start position and the end position
            this.transform.position = Vector3.Lerp(endPos, startPos, Perc);
		}
	}
}
