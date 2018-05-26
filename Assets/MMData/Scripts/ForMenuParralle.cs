using UnityEngine.UI;
using UnityEngine;

public class ForMenuParralle : MonoBehaviour
{

	/// the relative speed of the object
	public float Speed = 0;
	public static ForMenuParralle CurrentParallaxOffset;

	protected RawImage _rawImage;
	protected Renderer _renderer;
	protected Vector2 _newOffset;

	protected float _position = 0;
	protected float yOffset;

	/// <summary>
	/// On start, we store the current offset
	/// </summary>
	protected virtual void Start()
	{
		CurrentParallaxOffset = this;
		if (GetComponent<Renderer>() != null)
		{
			_renderer = GetComponent<Renderer>();
		}

		if (_renderer == null && GetComponent<RawImage>() != null)
		{
			_rawImage = GetComponent<RawImage>();
		}

	}

	/// <summary>
	/// On update, we apply the offset to the texture
	/// </summary>
	protected virtual void Update()
	{
		if ((_rawImage == null) && (_renderer == null))
		{
			return;
		}

		_position += (Speed / 300) * Time.deltaTime;



		// position reset
		if (_position > 1.0f)
		{
			_position -= 1.0f;
		}

		// we apply the offset to the object's texture
		_newOffset.x = _position;
		_newOffset.y = yOffset;

		if (_renderer != null)
		{
			_renderer.material.mainTextureOffset = _newOffset;
		}
		if (_rawImage != null)
		{
			_rawImage.material.mainTextureOffset = _newOffset;
		}

	}
}
