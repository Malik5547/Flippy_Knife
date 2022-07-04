using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class KnifeSelect : MonoBehaviour
{
    public Transform knifePreview;
    public GameObject[] knifes;
    public GameObject knifeButton;
    public GameObject unlockButton;
    public Transform unlockBtnParent;
    public RectTransform ParentPanel;
    public Light previewLight;
    public GameObject coinIcon;
    public GameObject priceText;
    public TMP_Text coinsText;

    private GameObject selectedKnife;
    private GameObject unlockBtn = null;
    private List<KnifeIcon> icons = new List<KnifeIcon>();
    private int currentIndex = 0;
    private int coinScore = 0;

    void Awake()
    {
        KnifeButtons();

        if (PlayerPrefs.HasKey("coins"))
            coinScore = PlayerPrefs.GetInt("coins");
        else
            PlayerPrefs.SetInt("coins", coinScore);

        coinsText.text = coinScore.ToString();
    }

    void KnifeButtons()
    {
        //if (PlayerPrefs.HasKey("knife"))
        //{
        //    knifeSelected = PlayerPrefs.GetString("knife");
        //    Debug.Log("Selected knife: " + knifeSelected);
        //}
        //else
        //{
        //    PlayerPrefs.SetString("knife", knifes[0].name);
        //    knifeSelected = PlayerPrefs.GetString("knife");
        //}

        for (int i = 0; i < knifes.Length; i++)
        {
            int x = i;

            GameObject knifeBtn = Instantiate(knifeButton);
            icons.Add(knifeBtn.GetComponent<KnifeIcon>());
            Transform knifeParent = knifeBtn.transform.Find("Object Parent");
            GameObject knife = Instantiate(knifes[i], knifeParent);
            knife.transform.localPosition = new Vector3(0f, 0f, 0f);
            Debug.Log("Knife position: " + knife.transform.position);

            //Set knife properties
            knife.gameObject.SetActive(true);
            Knife knifeScript = knife.GetComponent<Knife>();
            Rigidbody rb;
            if (knife.TryGetComponent(out rb))
            {
                rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                rb.isKinematic = true;
            }

            Transform trail = knife.transform.Find("GFX").Find("Trail");
            if (trail != null)  
                trail.gameObject.SetActive(false);
            Knife script;
            if (script = knife.GetComponent<Knife>())
            {
                Destroy(script);
            }
            foreach (Transform trans in knife.GetComponentsInChildren<Transform>(true))
            {
                trans.gameObject.layer = LayerMask.NameToLayer("UI");
            }

           // knife.transform.SetParent(knifeParent, false);

            knifeBtn.transform.SetParent(ParentPanel, false);

            knifeBtn.GetComponent<Button>().onClick.AddListener(delegate {
                KnifeSelected(x);
            });

            if (PlayerPrefs.HasKey(knifes[i].name))
            {
                if (PlayerPrefs.GetInt(knifes[i].name) == 0)
                {
                    knifeBtn.GetComponent<KnifeIcon>().SetLocked(true);
                }
            }
            else
            {
                PlayerPrefs.SetInt(knifes[i].name, 0);
                knifeBtn.GetComponent<KnifeIcon>().SetLocked(true);
            }
        }
    }

    void KnifeSelected(int index)
    {
        for (int i = knifePreview.childCount; i > 0; i--)
        {
            //Destroy(knifePreview.GetChild(i - 1).gameObject);
            if (selectedKnife != null)
                Destroy(selectedKnife);
        }

        selectedKnife = Instantiate(knifes[index], knifePreview);

        //Disable knife script
        Knife knifeScript = selectedKnife.GetComponent<Knife>();
        knifeScript.enabled = false;
        knifeScript.SetTrailEnabled(false);

        foreach (Transform trans in selectedKnife.GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = LayerMask.NameToLayer("Preview");
        }

        selectedKnife.transform.localPosition = new Vector3(0f, 0f, 0f);

        if (unlockBtn != null)
        {
            Destroy(unlockBtn);
        }

        if (icons[index].IsLocked())
        {
            unlockBtn = Instantiate(unlockButton, unlockBtnParent);

            unlockBtn.GetComponent<Button>().onClick.AddListener(delegate
            {
                UnlockKnife(index);
            });

            priceText.GetComponent<TMP_Text>().text = knifes[index].GetComponent<Knife>().price.ToString();
            coinIcon.SetActive(true);
            priceText.SetActive(true);

            previewLight.enabled = false;
        }
        else
        {
            coinIcon.SetActive(false);
            priceText.SetActive(false);
            previewLight.enabled = true;
            SelectedKnife.SetSelectedKnife(knifes[index]);
        }

        icons[currentIndex].SetSelected(false);
        currentIndex = index;
        icons[currentIndex].SetSelected(true);

        Debug.Log("Knife Selected: " + index.ToString());
    }

    void UnlockKnife(int index)
    {
        Debug.Log("Unlock knife " + index);
        int price = selectedKnife.GetComponent<Knife>().price;
        if (price < coinScore)
        {
            coinScore -= price;
            coinsText.text = coinScore.ToString();
            PlayerPrefs.SetInt("coins", coinScore);
            icons[index].SetLocked(false);
            PlayerPrefs.SetInt(knifes[index].name, 1);
            KnifeSelected(index);
        }
    }

    public void BackClicked()
    {
        SceneManager.LoadScene("Start Menu");
    }
}
