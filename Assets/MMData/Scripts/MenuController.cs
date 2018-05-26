using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {
	
	public Button btn_start;
	public Button btn_exit;

	public Button select_level1;
	public Button select_level2;
	public Button select_level3;

	public GameObject InitView;
	public GameObject SelectView;


	public CamZoom Zoomtool;

	void Start () {
		btn_start.onClick.AddListener(delegate
		{

			InitView.SetActive(false);
			SelectView.SetActive(true);
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
}
