using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CSharpTree
{
    public class Tiempo
    {
        public void Medicion()
        {
            Stopwatch Tiempo = new Stopwatch();//Clase StopWatch  la cual utilizo para medir el tiempo de ejecucion
            Tiempo.Start();//Inicia el temporizador a contar el tiempo de ejecucion
            NodoArbol<string> treeRoot = SampleData.GetSet1();//Proceso del arbol
            NodoArbol<string> found = treeRoot.EncontrarNodoDelArbol(node => node.Data != null && node.Data.Contains("210"));
            Console.WriteLine("Nodo Encontrado: " + found);//Imprime el nodo que se encontro
            Tiempo.Stop();//Pausa el tiempo
            Console.Write("El tiempo de ejecucion del arbol es: {0}", Tiempo.Elapsed.ToString()+" ns");//Imprime el tiempo
            Console.ReadKey(true);
        }
    }
}
