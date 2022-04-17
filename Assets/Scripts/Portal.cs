using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public GameObject loadingScreen;
    public Slider slider;
    private float cooldown = 3.0f;
    private float lastLoad;
    public string sceneName;
    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player" && Time.time - lastLoad > cooldown)
        {
            lastLoad = Time.time;
            LoadLevel();
        }      
    }
    public void LoadLevel()
    {
        StartCoroutine(LoadAsynchronously(sceneName));
    }

    IEnumerator LoadAsynchronously(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);
        loadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            slider.value = progress;

            yield return null;
        }
        
    }
}
