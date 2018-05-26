using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamZoom : MonoBehaviour
{
	
	public Vector3 TargetScale;
	private Transform ZoomBG;
	private Vector3 InitScale;


	private void Start()
	{
		ZoomBG = this.transform;
		InitScale = ZoomBG.localScale;
	}


	public void Zoom(float _time)
	{
		Debug.Log(_time);
		StartCoroutine(ZIn(_time));
	}

	IEnumerator ZIn(float _duration)
	{
		float elapsedTime = 0;

		while (elapsedTime < _duration)
		{
			Debug.Log(elapsedTime);
			ZoomBG.localScale = Vector3.Lerp(InitScale, TargetScale, elapsedTime / _duration);
			elapsedTime += Time.deltaTime;

			yield return new WaitForEndOfFrame();
		} 

		Debug.Log("End");
	}
}
