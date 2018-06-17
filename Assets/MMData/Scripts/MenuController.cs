using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuController : MonoBehaviour {
	
	public Button btn_start;
	public Button btn_exit;

	public Button select_level1;
	public Button select_level2;
	public Button select_level3;

	public GameObject InitView;
	public GameObject SelectView;

	public CamZoom Zoomtool;
	public Button[] buttons;

	void Start () {
		buttons = GetComponentsInChildren<Button>();
		foreach(var b in buttons)
		{
			b.onClick.AddListener(delegate
			{
				AudioManager.BTN_ES.Trigger("BTN01");
			});
		}

		btn_start.onClick.AddListener(delegate
		{

			InitView.SetActive(false);
			StartCoroutine(GoSelect());
			Zoomtool.Zoom(0.5f);

		});

		btn_exit.onClick.AddListener(delegate
		{
			
			Application.Quit();

		});

		select_level1.onClick.AddListener(delegate {

			SceneManager.LoadSceneAsync("Level1");
		});

		select_level2.onClick.AddListener(delegate {
		
			SceneManager.LoadSceneAsync("Level2");
		});

		select_level3.onClick.AddListener(delegate {
		
			SceneManager.LoadSceneAsync("Level3");
		});
	}

	IEnumerator GoSelect()
	{
		float elapsedTime = 0;
		Vector3 startingPos = SelectView.transform.localPosition;
		while (elapsedTime < 0.5f)
		{
			SelectView.transform.localPosition = Vector3.Lerp(startingPos, Vector3.zero, (elapsedTime / 0.5f));
			elapsedTime += Time.deltaTime;
			yield return null;
		}
	}
}
