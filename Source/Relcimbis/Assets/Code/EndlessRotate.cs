using UnityEngine;
using System.Collections;
//Written by Michael Bethke
public class EndlessRotate : MonoBehaviour
{


	private void Update ()
	{
		
		gameObject.transform.Rotate ( Vector3.up, Time.deltaTime * 100 );
	}
}
