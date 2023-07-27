using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public GameObject amPrefab;
    public AudioManager audioMan;
    public CanvasManager canvasMan;
    public SObuiding[] buildingList;
    public BuildingPlacement buildingPlacer;
    public CameraDirector cameraDirector;

    public int activeSceneIndex;

    private void Awake()
    {
        if (!gm)
        {
            DontDestroyOnLoad(this);
            gm = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (!audioMan) audioMan = Instantiate(amPrefab, this.transform).GetComponent<AudioManager>();
        if (!canvasMan) canvasMan = gameObject.GetComponent<CanvasManager>();
        if (!buildingPlacer) buildingPlacer = gameObject.GetComponent<BuildingPlacement>();
        if (!cameraDirector) cameraDirector = gameObject.AddComponent<CameraDirector>();
    }

    private void OnEnable()
    {
        Debug.Log(activeSceneIndex);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        activeSceneIndex = scene.buildIndex;
        canvasMan.Init();
        if(activeSceneIndex == 1)
        {
            gameObject.GetComponent<BuildingPlacement>().enabled = true;
        }
    }
    public void ChangeScene(int _SceneIndex)
    {
        StartCoroutine(TransitionToNewScene(_SceneIndex));
    }

    public IEnumerator TransitionToNewScene(int _sceneToLoad)
    {
        canvasMan.m_anim.SetTrigger("fadeOut");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(_sceneToLoad);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
