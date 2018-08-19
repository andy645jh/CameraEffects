using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject handButton;
    GameManager gamemanager;
    public GameObject textoDescriptivo;
    public GameObject uiCanvas;
    private void Start()
    {
        uiCanvas = GameObject.Find("UI Canvas");
        gamemanager = GameObject.Find("GameManager").transform.GetComponent<GameManager>();
        handButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.DraggObject);
    }

    public void ActivarMano()
    {
        if (handButton.activeSelf == false)
        {
            handButton.SetActive(true);
        }
    }

    public void QuitarTextoEnSegundos(int seg)
    {
        Invoke("QuitarTexto", seg);
    }

    public void QuitarTexto()
    {
        textoDescriptivo.SetActive(false);
    }

}
