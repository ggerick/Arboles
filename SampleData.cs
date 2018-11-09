using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSharpTree
{
    class SampleData
    {
        public static NodoArbol<string> GetSet1()
        {
            NodoArbol<string> Raiz = new NodoArbol<string>("Raiz");//Creacion de la raiz del arbol
            {
                NodoArbol<string> Nodo0 = Raiz.AñadirHijo("nodo0");//Creacion de las ramas o nodos hijos 0,1,2
                NodoArbol<string> Nodo1 = Raiz.AñadirHijo("nodo1");
                NodoArbol<string> Nodo2 = Raiz.AñadirHijo("nodo2");
                {
                    NodoArbol<string> Nodo20 = Nodo2.AñadirHijo(null);//Creamos un nodo extra y le asignamos el valor nulo
                    NodoArbol<string> Nodo21 = Nodo2.AñadirHijo("nodo21");
                    {
                        NodoArbol<string> Nodo210 = Nodo21.AñadirHijo("nodo210");//Añadimos mas nodos hijos
                        NodoArbol<string> Nodo211 = Nodo21.AñadirHijo("nodo211");
                    }
                }
                NodoArbol<string> Nodo3 = Raiz.AñadirHijo("nodo3");
                {
                    NodoArbol<string> Nodo30 = Nodo3.AñadirHijo("nodo30");
                }
            }
            //Todos los nodos anteriormente creados se agregan a la raiz como si fuera una lista de objetos
            return Raiz;
        }
    }
    class SampleIterating // Creacion de una clase du iteraciones simples
    {
        static void MainTest(string[] args)
        {
            NodoArbol<string> RaizDelArbol = SampleData.GetSet1();//Instanciamos la raiz del arbol NodoArbol
            foreach (NodoArbol<string> Nodo in RaizDelArbol)//Hacemos un recorido al arbol
            {
                string indent = CreateIndent(Nodo.Nivel);
                Console.WriteLine(indent + (Nodo.Data ?? "nulo"));
            }
        }

        private static String CreateIndent(int depth)//Creamos la clase que recibe como parametro la profundidad del arbol
        {
            StringBuilder sb = new StringBuilder();//Intanciamos la clase string builder para usar sus metodos
            for (int i = 0; i < depth; i++)//Por toda la profundidad del arbol
            {
                sb.Append(' ');//Vamos generando espacios para hacerlo mas claro la estructura del arbol
            }
            return sb.ToString();
        }
    }

    public class NodoArbol<T> : IEnumerable<NodoArbol<T>> // Generemos una clase que hereda de IEnumerable
    {
        //Declaracion de lsus propiedades
        public T Data { get; set; } // el contenido
        public NodoArbol<T> Padre { get; set; }//Nodo padre
        public ICollection<NodoArbol<T>> Hijo { get; set; } // la hoja/hijo

        public Boolean EsRaiz//Metodo boleano para saber si el nodo es la raiz o no
        {
            get { return Padre == null; }
        }

        public Boolean EsHoja//Metodo boleano para saber si el nodo es una hoja o no
        {
            get { return Hijo.Count == 0; }
        }

        public int Nivel//Metodo que calcula los niveles del arbol
        {
            get
            {
                if (this.EsRaiz)
                    return 0;
                return Padre.Nivel + 1;
            }
        }

        public NodoArbol(T data)//un constructor que recibe el dato
        {
            this.Data = data;
            this.Hijo = new LinkedList<NodoArbol<T>>();

            this.ElementosIndex = new LinkedList<NodoArbol<T>>();
            this.ElementosIndex.Add(this);
        }

        public NodoArbol<T> AñadirHijo(T Hijo) // Una clase que sirve para añadir mas hojas/nodos al arbol
        {
            NodoArbol<T> NodoHijo = new NodoArbol<T>(Hijo) { Padre = this };
            this.Hijo.Add(NodoHijo);//Agrega el nodo hijo

            this.RegistrarHijoParaBuscar(NodoHijo);//Se busca al nodo hijo

            return NodoHijo;// lo devuelve alv
        }

        public override string ToString()//Polimorfismo
        {
            return Data != null ? Data.ToString() : "[data nula]";
        }


        #region searching

        private ICollection<NodoArbol<T>> ElementosIndex { get; set; }// Declaramos una collecion privada de los nodos del arbol para encontrar su posicion

        private void RegistrarHijoParaBuscar(NodoArbol<T> node)//Un metodo para encontrar estos nodos, y que los recibe
        {
            ElementosIndex.Add(node);//Agrega el nodo a l colleciond e nodox
            if (Padre != null)//Si tiene padre
                Padre.RegistrarHijoParaBuscar(node);//Se mete al nodo del padre para buscar el nodo hijo
        }

        public NodoArbol<T> EncontrarNodoDelArbol(Func<NodoArbol<T>, bool> Predicado)
        {
            return this.ElementosIndex.FirstOrDefault(Predicado);
        }



        IEnumerator IEnumerable.GetEnumerator()//Funciones
        {
            return GetEnumerator();
        }

        public IEnumerator<NodoArbol<T>> GetEnumerator()//metodo para obtener la direccion/posicion exacta del nodo hijo
        {
            yield return this;
            foreach (var DireccionHijo in this.Hijo)//Recorre la posicion de los hijos
            {
                foreach (var CualquierHijo in DireccionHijo)//Recorre todos los nodos hijos en la collecion de las direcciones de los nodos hijos
                    yield return CualquierHijo;//Y los devuelve
            }
        }

    }
}
#endregion