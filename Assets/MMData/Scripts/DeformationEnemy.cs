using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeformationEnemy : MonoBehaviour {

	public Vector3 TargetScale;
	private Transform ZoomBG;
	private Vector3 InitScale;

	private void OnEnable()
	{
		ZoomBG = this.transform;
		InitScale = ZoomBG.localScale;

		StartCoroutine(ZIn(0.5f));
	}

	IEnumerator ZIn(float _duration)
	{
		float elapsedTime = 0;

		while (elapsedTime < _duration)
		{
			//Debug.Log(elapsedTime);
			ZoomBG.localScale = Vector3.Lerp(InitScale, TargetScale, elapsedTime / _duration);
			elapsedTime += Time.deltaTime;

			yield return new WaitForEndOfFrame();
		}

		StartCoroutine(Zout(0.5f));
	}

	IEnumerator Zout(float _duration)
	{
		float elapsedTime = 0;

		while (elapsedTime < _duration)
		{
			//Debug.Log(elapsedTime);
			ZoomBG.localScale = Vector3.Lerp(TargetScale, InitScale, elapsedTime / _duration);
			elapsedTime += Time.deltaTime;

			yield return new WaitForEndOfFrame();
		}

		StartCoroutine(ZIn(0.5f));
	}
}
