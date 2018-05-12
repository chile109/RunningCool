using UnityEngine;
using System.Collections;
using MoreMountains.Tools;

namespace MoreMountains.InfiniteRunnerEngine
{
	public class FlappyPile : MonoBehaviour
	{
		public bool IsTop = true;
		public float SkyBottomY;
		public float SkyTopY;

		protected Renderer _renderer;
		protected Vector3 _oddVector = new Vector3(1, -1, 1);

		protected virtual void OnSpawnComplete()
		{
			if (GetComponent<Renderer>() != null)
			{
				_renderer = GetComponent<Renderer>();
			}

			float randomModifier = Random.Range(1f, _renderer.bounds.size.y - 3f);

			if (IsTop)
			{
				_renderer.transform.localScale = _oddVector;
				transform.position = new Vector2(transform.position.x, SkyTopY + _renderer.bounds.size.y / 2 - randomModifier);
			}
			else
			{
				_renderer.transform.localScale = Vector3.one;
				transform.position = new Vector2(transform.position.x, SkyBottomY - _renderer.bounds.size.y / 2 + randomModifier);
			}
		}

		/// <summary>
		/// On enable, we register to the OnObjectSpawned event
		/// </summary>
		void OnEnable()
		{
			GetComponent<PoolableObject>().OnSpawnComplete += OnSpawnComplete;
		}

		/// <summary>
		/// On disable, we unregister to the OnObjectSpawned event
		/// </summary>
		void OnDisable()
		{
			GetComponent<PoolableObject>().OnSpawnComplete -= OnSpawnComplete;
		}
	}

}