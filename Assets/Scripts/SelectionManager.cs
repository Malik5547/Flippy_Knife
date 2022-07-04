using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SelectionManager : MonoBehaviour
{

    public KnifeIcon currentSelection;
    public GameObject knifeParent;

    private GameObject knifeObject = null;
    private static int _previousScene = 0;


    public void SelectKnife(KnifeIcon knifeIcon)
    {
        currentSelection = knifeIcon;

        Debug.Log("Clicked: " + knifeIcon.gameObject);
    }

    public void SetCurrentSelection(KnifeIcon knifeIcon)
    {
        currentSelection.SetSelected(false);

        currentSelection = knifeIcon;

        if (knifeObject !=null)
        {
            Destroy(knifeObject);
        }

        knifeObject = Instantiate(knifeIcon.knifePrefab, knifeParent.transform);
        Knife knifeScript = knifeObject.GetComponent<Knife>();
        knifeScript.enabled = false;

        // Set knife in SelectedKnife
        Debug.Log(knifeIcon);
        Debug.Log(knifeIcon.knifePrefab);
        SelectedKnife.SetSelectedKnife(knifeIcon.knifePrefab);

        currentSelection.SetSelected(true);
    }

    public static void SetPreviousScene(int scene)
    {
        _previousScene = scene;
    }

    public void GoPreviousScene()
    {
        SceneManager.LoadScene(_previousScene);
    }


}
