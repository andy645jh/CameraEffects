using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VistaMonstruo : MonoBehaviour
{
    private System.Random _random = new System.Random();

    public GameObject camaraPrincipal;

    public GameObject[] camarasMonstruos;

    public GameObject canvas;

    public GameObject vistaAjena;

    public Touch touch;

    public Point2D[] puntos;

    public GameObject objetoPrueba;

    public GameObject[] puntosCamara = new GameObject[6];

    void Start()
    {
        print("Width: " + Screen.width);
        print("Height: " + Screen.height);
        camarasMonstruos = GameObject.FindGameObjectsWithTag("CamaraMonstruo");
        puntos = new Point2D[6];
        for (int i = 0; i < puntos.Length; i++)
        {
            puntos[i] = null;
        }
    }

    void Update()
    {        
        if (Input.GetMouseButtonDown(0)) {
            if (Input.mousePosition.x < Screen.width / 3 && Input.mousePosition.y < Screen.height / 2)
            {
                if (puntos[0] != null)
                {
                    //Hacer que la vista ajena muestre la cámara
                    float distancia = Acercamiento(0, Screen.width / 3, 0, Screen.height / 2, puntos[0].x, puntos[0].y, (int)Input.mousePosition.x, (int)Input.mousePosition.y);
                    vistaAjena.GetComponent<Image>().color = new Color(0, 0, 0, distancia);
                    if (distancia < 0.10)
                    {
                        CambiarAMonstruo(0);
                    }
                }
                else
                {
                    vistaAjena.GetComponent<Image>().color = new Color(0, 0, 0);
                }
            }
            else if (Input.mousePosition.x < 2 * Screen.width / 3 && Input.mousePosition.y < Screen.height / 2)
            {
                if (puntos[1] != null)
                {
                    //Hacer que la vista ajena muestre la cámara
                    float distancia = Acercamiento(Screen.width / 3, 2 * Screen.width / 3, 0, Screen.height / 2, puntos[1].x, puntos[1].y, (int)Input.mousePosition.x, (int)Input.mousePosition.y);
                    vistaAjena.GetComponent<Image>().color = new Color(0, 0, 0, distancia);
                    if (distancia < 0.10)
                    {
                        CambiarAMonstruo(1);
                    }
                }
                else
                {
                    vistaAjena.GetComponent<Image>().color = new Color(0, 0, 0);
                }
            }
            else if (Input.mousePosition.x < Screen.width && Input.mousePosition.y < Screen.height / 2)
            {
                if (puntos[2] != null)
                {
                    //Hacer que la vista ajena muestre la cámara
                    float distancia = Acercamiento(2 * Screen.width / 3, Screen.width, 0, Screen.height / 2, puntos[2].x, puntos[2].y, (int)Input.mousePosition.x, (int)Input.mousePosition.y);
                    vistaAjena.GetComponent<Image>().color = new Color(0, 0, 0, distancia);
                    if (distancia < 0.10)
                    {
                        CambiarAMonstruo(2);
                    }
                }
                else
                {
                    vistaAjena.GetComponent<Image>().color = new Color(0, 0, 0);
                }
            }
            else if (Input.mousePosition.x < Screen.width / 3 && Input.mousePosition.y < Screen.height)
            {
                if (puntos[3] != null)
                {
                    //Hacer que la vista ajena muestre la cámara
                    float distancia = Acercamiento(0, Screen.width / 3, Screen.height / 2, Screen.height, puntos[3].x, puntos[3].y, (int)Input.mousePosition.x, (int)Input.mousePosition.y);
                    vistaAjena.GetComponent<Image>().color = new Color(0, 0, 0, distancia);
                    if (distancia < 0.10)
                    {
                        CambiarAMonstruo(3);
                    }
                }
                else
                {
                    vistaAjena.GetComponent<Image>().color = new Color(0, 0, 0);
                }
            }
            else if (Input.mousePosition.x < 2 * Screen.width / 3 && Input.mousePosition.y < Screen.height)
            {
                if (puntos[4] != null)
                {
                    //Hacer que la vista ajena muestre la cámara
                    float distancia = Acercamiento(Screen.width / 3, 2 * Screen.width / 3, Screen.height / 2, Screen.height, puntos[4].x, puntos[4].y, (int)Input.mousePosition.x, (int)Input.mousePosition.y);
                    vistaAjena.GetComponent<Image>().color = new Color(0, 0, 0, distancia);
                    if (distancia < 0.10)
                    {
                        CambiarAMonstruo(4);
                    }
                }
                else
                {
                    vistaAjena.GetComponent<Image>().color = new Color(0, 0, 0);
                }
            }
            else if (Input.mousePosition.x < Screen.width && Input.mousePosition.y < Screen.height)
            {
                if (puntos[5] != null)
                {
                    //Hacer que la vista ajena muestre la cámara
                    float distancia = Acercamiento(2 * Screen.width / 3, Screen.width, Screen.height / 2, Screen.height, puntos[5].x, puntos[5].y, (int)Input.mousePosition.x, (int)Input.mousePosition.y);
                    vistaAjena.GetComponent<Image>().color = new Color(0, 0, 0, distancia);
                    if (distancia < 0.10)
                    {
                        CambiarAMonstruo(5);
                    }
                }
                else
                {
                    vistaAjena.GetComponent<Image>().color = new Color(0, 0, 0);
                }
            }

            print("X: " + Input.mousePosition.x);
            print("Y: " + Input.mousePosition.y);
        }
    }

    public void CambiarAMonstruo(int numero)
    {
        vistaAjena.SetActive(false);
        puntos[numero].objeto.GetComponent<Camera>().enabled = true;
        camaraPrincipal.GetComponent<Camera>().enabled = false;
        

    }

    public void EntrarAlModoVistaAjena()
    {
        GenerarPuntos();
        vistaAjena.SetActive(true);
    }

    public void SalirDelModoVistaAjena()
    {
        vistaAjena.SetActive(false);
    }

    public void GenerarPuntos()
    {

        int[] zonas = {0,0,0,0,0,0};

        for (int i = 0; i < camarasMonstruos.Length; i++) {
            zonas[i] = i + 1;
        }

        Shuffle(zonas);

        for (int i = 0; i < zonas.Length; i++) {
            if (zonas[i] != 0) {
                switch (i)
                {
                    case 0:
                        puntos[i] = new Point2D(_random.Next(Screen.width/15, 4*Screen.width/15), _random.Next(Screen.height/10, 4*Screen.height/10), camarasMonstruos[zonas[i] - 1]);
                        break;
                    case 1:
                        puntos[i] = new Point2D(_random.Next(6*Screen.width/15, 9*Screen.width/15), _random.Next(Screen.height/10, 4*Screen.height/10), camarasMonstruos[zonas[i] - 1]);
                        break;
                    case 2:
                        puntos[i] = new Point2D(_random.Next(11*Screen.width/15, 14*Screen.width/15), _random.Next(Screen.height/10, 4*Screen.height/10), camarasMonstruos[zonas[i] - 1]);
                        break;
                    case 3:
                        puntos[i] = new Point2D(_random.Next(Screen.width / 15, 4*Screen.width/15), _random.Next(6*Screen.height/10, 9*Screen.height/10), camarasMonstruos[zonas[i] - 1]);
                        break;
                    case 4:
                        puntos[i] = new Point2D(_random.Next(6*Screen.width/15, 9*Screen.width/15), _random.Next(6*Screen.height/10, 9*Screen.height/10), camarasMonstruos[zonas[i] - 1]);
                        break;
                    case 5:
                        puntos[i] = new Point2D(_random.Next(11*Screen.width/15, 14*Screen.width/15), _random.Next(6*Screen.height/10, 9*Screen.height/10), camarasMonstruos[zonas[i] - 1]);
                        break;
                }
            }
        }

        for (int i = 0; i < puntos.Length; i++) {
            if (puntosCamara[i] != null) {
                Destroy(puntosCamara[i]);
                print(i + " destruido");
            }
            if(puntos[i] != null)
            {
                GameObject objeto = Instantiate(objetoPrueba);
                objeto.transform.parent = vistaAjena.transform;
                objeto.transform.position = new Vector3(puntos[i].x, puntos[i].y, 0);
                puntosCamara[i] = objeto;
            }
        }        
    }

    void Shuffle(int[] array)
    {
        int p = array.Length;
        for (int n = p - 1; n > 0; n--)
        {
            int r = _random.Next(0, n);
            int t = array[r];
            array[r] = array[n];
            array[n] = t;
        }
    }

    float Acercamiento(int inicioX, int finX, int inicioY, int finY, int aciertoX, int aciertoY, int puntoX, int puntoY)
    {
        int bordeX;
        int bordeY;

        if (puntoX > aciertoX)
        {
            bordeX = finX;
        }
        else
        {
            bordeX = inicioX;
        }

        if (puntoY > aciertoY)
        {
            bordeY = finY;
        }
        else
        {
            bordeY = inicioY;
        }
        print("Distancia: " + Distancia(puntoX, puntoY, aciertoX, aciertoY) / (Distancia(puntoX, puntoY, aciertoX, aciertoY) + Distancia(puntoX, puntoY, bordeX, bordeY)));
        return Distancia(puntoX, puntoY, aciertoX, aciertoY) / (Distancia(puntoX, puntoY, aciertoX, aciertoY) + Distancia(puntoX, puntoY, bordeX, bordeY));
    }

    float Distancia(int aX, int aY, int bX, int bY)
    {
        return Mathf.Sqrt((aX - bX) * (aX - bX) + (aY - bY) * (aY - bY));
    }

    public class Point2D {

        public int x;
        public int y;
        public GameObject objeto;

        public Point2D(int x, int y, GameObject objeto) {
            this.x = x;
            this.y = y;
            this.objeto = objeto;
        }
    }
}
