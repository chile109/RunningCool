using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthEnemy : MonoBehaviour {

	public Color TargetColor;
	public Anima2D.SpriteMeshInstance SpriteMesh;
	private Color InitColor;

	private void OnEnable()
	{
		InitColor = SpriteMesh.color;

		StartCoroutine(Fadeout(0.5f));
	}

	IEnumerator Fadeout(float _duration)
	{
		float elapsedTime = 0;

		while (elapsedTime < _duration)
		{
			//Debug.Log(elapsedTime);
			SpriteMesh.color =Color.Lerp(InitColor, TargetColor, elapsedTime / _duration);
			elapsedTime += Time.deltaTime;

			yield return new WaitForEndOfFrame();
		}

		StartCoroutine(FadeIn(1f));
	}

	IEnumerator FadeIn(float _duration)
	{
		yield return new WaitForSeconds(2f);

		float elapsedTime = 0;

		while (elapsedTime < _duration)
		{
			//Debug.Log(elapsedTime);
			SpriteMesh.color = Color.Lerp(TargetColor, InitColor, elapsedTime / _duration);
			elapsedTime += Time.deltaTime;

			yield return new WaitForEndOfFrame();
		}

		StartCoroutine(Fadeout(1f));
	}
}
