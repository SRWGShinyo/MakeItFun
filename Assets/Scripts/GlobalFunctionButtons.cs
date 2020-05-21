using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class GlobalFunctionButtons : MonoBehaviour
{
    public int nextLevel;
    public GameObject panel;


    public void loadNext()
    {
        SceneManager.LoadScene(nextLevel);
    }

    public void openPanel()
    {
        EventSystem.current.SetSelectedGameObject(null);
        panel.transform.DOScale(1, 1);
    }

    public void closePanel()
    {
        EventSystem.current.SetSelectedGameObject(null);
        panel.transform.DOScale(0, 1);
    }

    public void home()
    {
        EventSystem.current.SetSelectedGameObject(null);
        SceneManager.LoadScene(0);
    }

    public void redo()
    {
        EventSystem.current.SetSelectedGameObject(null);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void quit()
    {
        Application.Quit();
    }
}
