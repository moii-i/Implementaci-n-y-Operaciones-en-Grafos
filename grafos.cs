// Proyecto: Implementación de Grafos en C#
using System;
using System.Collections.Generic;

namespace GrafoBasico
{
    class Grafo
    {
        private Dictionary<string, List<string>> adyacencia;
        private bool esDirigido;

        public Grafo(bool dirigido)
        {
            adyacencia = new Dictionary<string, List<string>>();
            esDirigido = dirigido;
        }

        public void AgregarNodo(string nodo)
        {
            if (!adyacencia.ContainsKey(nodo))
            {
                adyacencia[nodo] = new List<string>();
            }
            else
            {
                Console.WriteLine("El nodo ya existe.");
            }
        }

        public void AgregarArista(string origen, string destino)
        {
            if (!adyacencia.ContainsKey(origen) || !adyacencia.ContainsKey(destino))
            {
                Console.WriteLine("Uno o ambos nodos no existen.");
                return;
            }
            adyacencia[origen].Add(destino);
            if (!esDirigido)
            {
                adyacencia[destino].Add(origen);
            }
        }

        public void MostrarGrafo()
        {
            Console.WriteLine("\nRepresentación del Grafo:");
            foreach (var nodo in adyacencia)
            {
                Console.Write(nodo.Key + " -> ");
                Console.WriteLine(string.Join(", ", nodo.Value));
            }
        }

        public void BFS(string inicio)
        {
            if (!adyacencia.ContainsKey(inicio))
            {
                Console.WriteLine("Nodo de inicio no existe.");
                return;
            }
            var visitados = new HashSet<string>();
            var cola = new Queue<string>();

            visitados.Add(inicio);
            cola.Enqueue(inicio);

            Console.WriteLine("\nRecorrido BFS:");
            while (cola.Count > 0)
            {
                string actual = cola.Dequeue();
                Console.Write(actual + " ");

                foreach (var vecino in adyacencia[actual])
                {
                    if (!visitados.Contains(vecino))
                    {
                        visitados.Add(vecino);
                        cola.Enqueue(vecino);
                    }
                }
            }
            Console.WriteLine();
        }

        public void DFS(string inicio)
        {
            if (!adyacencia.ContainsKey(inicio))
            {
                Console.WriteLine("Nodo de inicio no existe.");
                return;
            }
            var visitados = new HashSet<string>();
            Console.WriteLine("\nRecorrido DFS:");
            DFSRecursivo(inicio, visitados);
            Console.WriteLine();
        }

        private void DFSRecursivo(string nodo, HashSet<string> visitados)
        {
            visitados.Add(nodo);
            Console.Write(nodo + " ");

            foreach (var vecino in adyacencia[nodo])
            {
                if (!visitados.Contains(vecino))
                {
                    DFSRecursivo(vecino, visitados);
                }
            }
        }
    }

    class Programa
    {
        static void Main(string[] args)
        {
            Console.WriteLine("¿Desea un grafo dirigido? (s/n): ");
            string respuesta = Console.ReadLine();
            bool esDirigido = respuesta.ToLower() == "s";

            Grafo grafo = new Grafo(esDirigido);

            int opcion;
            do
            {
                Console.WriteLine("\n--- Menú ---");
                Console.WriteLine("1. Agregar nodo");
                Console.WriteLine("2. Agregar arista");
                Console.WriteLine("3. Mostrar grafo");
                Console.WriteLine("4. Recorrido BFS");
                Console.WriteLine("5. Recorrido DFS");
                Console.WriteLine("6. Salir");
                Console.Write("Seleccione una opción: ");

                opcion = int.Parse(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        Console.Write("Ingrese el nombre del nodo: ");
                        string nodo = Console.ReadLine();
                        grafo.AgregarNodo(nodo);
                        break;
                    case 2:
                        Console.Write("Nodo origen: ");
                        string origen = Console.ReadLine();
                        Console.Write("Nodo destino: ");
                        string destino = Console.ReadLine();
                        grafo.AgregarArista(origen, destino);
                        break;
                    case 3:
                        grafo.MostrarGrafo();
                        break;
                    case 4:
                        Console.Write("Nodo de inicio para BFS: ");
                        string inicioBFS = Console.ReadLine();
                        grafo.BFS(inicioBFS);
                        break;
                    case 5:
                        Console.Write("Nodo de inicio para DFS: ");
                        string inicioDFS = Console.ReadLine();
                        grafo.DFS(inicioDFS);
                        break;
                    case 6:
                        Console.WriteLine("Saliendo...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

            } while (opcion != 6);
        }
    }
}
