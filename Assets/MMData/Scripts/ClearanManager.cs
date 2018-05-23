using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.InfiniteRunnerEngine;
using MoreMountains.Tools;

public class ClearanManager : MonoBehaviour {

	public float Goal = 1500;
	private bool complete = false;
	void Update () {
		if (GameManager.Instance.Points >= Goal && !complete)
			WinEffect(LevelManager.Instance.CurrentPlayableCharacters[0]);
	}


	/// <summary>
	/// Kills the player.
	/// </summary>
	public virtual void WinEffect(PlayableCharacter player)
	{
		complete = true;
		LevelManager.Instance.CurrentPlayableCharacters.Remove(player);
		Destroy(player.gameObject);
		MMEventManager.TriggerEvent(new MMGameEvent("Win"));
		StartCoroutine(RemoveCharacterCo(player));
	}

	/// <summary>
	/// Coroutine that kills the player, stops the camera, resets the points.
	/// </summary>
	/// <returns>The player co.</returns>
	protected virtual IEnumerator RemoveCharacterCo(PlayableCharacter player)
	{
		yield return new WaitForSeconds(1f);

	}
}
