using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoreMountains.InfiniteRunnerEngine
{
	public class InvincibleTool : MonoBehaviour
	{
		public bool AvoidTwice = false;
		// Update is called once per frame
		void Update()
		{
			if (Input.GetKeyDown(KeyCode.Tab) && !AvoidTwice)
			{
				GameManager.Instance.Invincible = true;
				AvoidTwice = true;
			}

			else if(Input.GetKeyDown(KeyCode.Tab) && AvoidTwice)
			{
				GameManager.Instance.Invincible = false;
				AvoidTwice = false;
			}
			
		}
	}
}
