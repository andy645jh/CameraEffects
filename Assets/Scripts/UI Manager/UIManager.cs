using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject handButton;
    GameManager gamemanager;
    RayCastDrag rayCastDrag;
    public GameObject textoDescriptivo;
    public GameObject uiCanvas;
    public Sprite handIcon;
    public Sprite handReleaseIcon;
    

    private void Start()
    {
        rayCastDrag = GameObject.Find("MainCamera").GetComponent<RayCastDrag>();
        uiCanvas = GameObject.Find("UI Canvas");
        gamemanager = GameObject.Find("GameManager").GetComponent<GameManager>();
        handButton.transform.GetComponent<Button>().onClick.AddListener(gamemanager.DraggObject);
        gamemanager.objetoAgarrado = null;

    }

    public void CheckItemGrabbed()
    {
        if (gamemanager.tieneUnObjetoCogido == false)
        {
            print("mano normal");
            handButton.GetComponent<Image>().sprite = handIcon;
            handButton.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);

        }
        else
        {
            if (rayCastDrag.NotHitting == false) {
                print("mano normal mientras llevas objeto");
                handButton.GetComponent<Image>().sprite = handIcon;
                handButton.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 200);

            }
            else
          	{
                print("Mano de objeto");
                handButton.GetComponent<Image>().sprite = handReleaseIcon;
                handButton.GetComponent<RectTransform>().sizeDelta = new Vector2(298, 136);

                
            }
        }
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
