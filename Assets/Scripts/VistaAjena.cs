using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VistaAjena : MonoBehaviour
{
    private System.Random _random = new System.Random();

    public GameObject camaraPrincipal;

    public GameObject[] camarasMonstruos;

    public GameObject canvas;

    public Point2D[] puntos;

    public GameObject[] puntosCamara;

    public GameObject objetoPrueba;

    public float distanciaParaFijarMonstruo = 0.10f;

    public float distancia;

    public bool enVistaAjena = false;
    public bool buscandoCamaraMonstruo = false;

    public GameObject botonAlternarVistaAjena;
    public GameObject joystick;

	public Point2D puntoCercano;
	public int indexPuntoCercano;

    public GameObject agujero;  

    private Color color;

    Touch touch;

    void Start()
    {
        
    }

    //UPDATE PARA MOVIL
    
    void Update()
    {
        if (buscandoCamaraMonstruo) {

            /* PARA MOVIL
            touch = Input.touches[0];
            CalcularPuntoCercano(touch.position.x, touch.position.y);
			distancia = Acercamiento(puntoCercano.x, puntoCercano.y, (int)touch.position.x, (int)touch.position.y);
			this.gameObject.GetComponent<Image>().color = new Color(color.r, color.g, color.b, distancia);
			CambiarAMonstruo(indexPuntoCercano);
            */

            /* PARA ORDENADOR */
            if (Input.GetMouseButtonDown(0))
            {
                CalcularPuntoCercano(Input.mousePosition.x, Input.mousePosition.y);
                distancia = Acercamiento(puntoCercano.x, puntoCercano.y, (int)Input.mousePosition.x, (int)Input.mousePosition.y);
                this.gameObject.GetComponent<Image>().color = new Color(color.r, color.g, color.b, distancia);
                CambiarAMonstruo(indexPuntoCercano);
            }
            

        }
    }

    public void CambiarAMonstruo(int numero)
    {
        for (int i = 0; i < camarasMonstruos.Length; i++)
        {
            camarasMonstruos[i].GetComponent<Camera>().enabled = false;
        }
        puntos[numero].objeto.GetComponent<Camera>().enabled = true;
        
        camaraPrincipal.GetComponent<Camera>().enabled = false;
        if (distancia < distanciaParaFijarMonstruo)
        {
            this.gameObject.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0);
            botonAlternarVistaAjena.SetActive(true);
            buscandoCamaraMonstruo = false;
            agujero.transform.position = new Vector3(puntos[numero].x, puntos[numero].y, 0);
            agujero.SetActive(true);
            agujero.GetComponent<Animator>().Play("agujeroGenerico");
            agujero.GetComponent<Animator>().Rebind();
        }
    }
  
    public void AlternarModoVistaAjena()
    {
        if (!enVistaAjena)
        {
            camarasMonstruos = GameObject.FindGameObjectsWithTag("CamaraMonstruo");
            if (camarasMonstruos.Length > 0 && camarasMonstruos.Length <= 6)
            {
                puntos = new Point2D[6];
                puntosCamara = new GameObject[6];
                for (int i = 0; i < puntos.Length; i++)
                {
                    puntos[i] = null;
                }
                color = this.gameObject.GetComponent<Image>().color;

                this.gameObject.GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1);
                GenerarPuntos();
                this.gameObject.SetActive(true);
                botonAlternarVistaAjena.SetActive(false);
                joystick.SetActive(false);
                enVistaAjena = true;
                buscandoCamaraMonstruo = true;
                print("alternar modo vistaAjena llamado");
            }
            else
            {
                //NO HAY MONSTRUOS
                print("NO SE HAN ENCONTRADO MONSTRUOS");
            }
        }
        else {
            for (int i = 0; i < camarasMonstruos.Length; i++)
            {
                camarasMonstruos[i].GetComponent<Camera>().enabled = false;
            }
            camaraPrincipal.GetComponent<Camera>().enabled = true;
            joystick.SetActive(true);
            enVistaAjena = false;
        }
    }

    public void GenerarPuntos()
    {

        int[] zonas = {0,0,0,0,0,0};

        for (int i = 0; i < camarasMonstruos.Length; i++) {
            zonas[i] = i + 1;
        }

        Shuffle(zonas);

        for (int i = 0; i < zonas.Length; i++) {
            if (zonas[i] != 0)
            {
                switch (i)
                {
                    case 0:
                        puntos[i] = new Point2D(_random.Next(Screen.width / 15, 4 * Screen.width / 15), _random.Next(Screen.height / 10, 4 * Screen.height / 10), camarasMonstruos[zonas[i] - 1]);
                        break;
                    case 1:
                        puntos[i] = new Point2D(_random.Next(6 * Screen.width / 15, 9 * Screen.width / 15), _random.Next(Screen.height / 10, 4 * Screen.height / 10), camarasMonstruos[zonas[i] - 1]);
                        break;
                    case 2:
                        puntos[i] = new Point2D(_random.Next(11 * Screen.width / 15, 14 * Screen.width / 15), _random.Next(Screen.height / 10, 4 * Screen.height / 10), camarasMonstruos[zonas[i] - 1]);
                        break;
                    case 3:
                        puntos[i] = new Point2D(_random.Next(Screen.width / 15, 4 * Screen.width / 15), _random.Next(6 * Screen.height / 10, 9 * Screen.height / 10), camarasMonstruos[zonas[i] - 1]);
                        break;
                    case 4:
                        puntos[i] = new Point2D(_random.Next(6 * Screen.width / 15, 9 * Screen.width / 15), _random.Next(6 * Screen.height / 10, 9 * Screen.height / 10), camarasMonstruos[zonas[i] - 1]);
                        break;
                    case 5:
                        puntos[i] = new Point2D(_random.Next(11 * Screen.width / 15, 14 * Screen.width / 15), _random.Next(6 * Screen.height / 10, 9 * Screen.height / 10), camarasMonstruos[zonas[i] - 1]);
                        break;
                }
            }
            else
            {
                puntos[i] = null;
            }
        }
        
        for (int i = 0; i < puntos.Length; i++) {
            if (puntosCamara[i] != null) {
                Destroy(puntosCamara[i]);
                print(i + " destruido");
            }
            if (puntos[i] != null)
            {
                puntosCamara[i] = Instantiate(objetoPrueba);
                puntosCamara[i].transform.SetParent(this.gameObject.transform);
                puntosCamara[i].transform.position = new Vector3(puntos[i].x, puntos[i].y, 0);
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

    float Acercamiento(int aciertoX, int aciertoY, int puntoX, int puntoY)
    {
        int bordeX;
        int bordeY;

        if (puntoX > aciertoX)
        {
            bordeX = Screen.width;
        }
        else
        {
            bordeX = 0;
        }

        if (puntoY > aciertoY)
        {
            bordeY = Screen.height;
        }
        else
        {
            bordeY = 0;
        }
        print("Distancia: " + Distancia(puntoX, puntoY, aciertoX, aciertoY) / (Distancia(puntoX, puntoY, aciertoX, aciertoY) + Distancia(puntoX, puntoY, bordeX, bordeY)));
        return Distancia(puntoX, puntoY, aciertoX, aciertoY) / (Distancia(puntoX, puntoY, aciertoX, aciertoY) + Distancia(puntoX, puntoY, bordeX, bordeY));
    }

    float Distancia(float aX, float aY, float bX, float bY)
    {
        return Mathf.Sqrt((aX - bX) * (aX - bX) + (aY - bY) * (aY - bY));
    }

	void CalcularPuntoCercano(float aX, float aY) {		
		puntoCercano = null;
		indexPuntoCercano = 0;
		float distancia;
		float aux = float.MaxValue;
		for(int i = 0; i < puntos.Length; i++) {
			if (puntos [i] != null) {
				distancia = Distancia (aX, aY, puntos [i].x, puntos [i].y);
				if (distancia < aux) {
					aux = distancia;
					puntoCercano = puntos [i];
					indexPuntoCercano = i;
				}
			}
		}
	}

    public class Point2D {

        public int x;
        public int y;
        public GameObject objeto;

        public Point2D() {
            this.x = 0;
            this.y = 0;
            this.objeto = null;
        }

        public Point2D(int x, int y, GameObject objeto) {
            this.x = x;
            this.y = y;
            this.objeto = objeto;
        }
    }
}
