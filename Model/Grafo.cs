// using System;
using System.Collections.Generic;

public class Grafo
{
    private int[,] _matrizAdjacencia;
    private int _tamanho;

    public Grafo(int tamanho)
    {
        _tamanho = tamanho;
        _matrizAdjacencia = new int[tamanho, tamanho];
    }

    public void AdicionarAresta(int origem, int destino)
    {
        _matrizAdjacencia[origem, destino] = 1;
    }

    public int ObterPrimeiroVertice()
    {
        for (int i = 0; i < _tamanho; i++)
        {
            for (int j = 0; j < _tamanho; j++)
            {
                if (_matrizAdjacencia[i, j] == 1)
                {
                    return i;
                }
            }
        }
        return -1; // Se o grafo estiver vazio, retorna -1.
    }

    public List<int> ObterVizinhos(int vertice)
    {
        List<int> vizinhos = new List<int>();
        for (int i = 0; i < _tamanho; i++)
        {
            if (_matrizAdjacencia[vertice, i] == 1)
            {
                vizinhos.Add(i);
            }
        }
        return vizinhos;
    }

    public void BuscaEmProfundidade(int inicio)
    {
        bool[] visitado = new bool[_tamanho];
        DFSUtil(inicio, visitado);
    }

    public List<int> BuscaEmLargura(int inicio)
    {
        bool[] visitado = new bool[_tamanho];
        Queue<int> fila = new Queue<int>();
        List<int> vertices = new List<int>();

        visitado[inicio] = true;
        fila.Enqueue(inicio);

        while (fila.Count > 0)
        {
            int vertice = fila.Dequeue();

            vertices.Add(vertice);
            // Console.WriteLine($"Visitando vértice: {vertice}");

            foreach (int vizinho in ObterVizinhos(vertice))
            {
                if (!visitado[vizinho])
                {
                    visitado[vizinho] = true;
                    fila.Enqueue(vizinho);
                }
            }
        }

        return vertices;
    }

    private void DFSUtil(int vertice, bool[] visitado)
    {
        visitado[vertice] = true;
        // Console.WriteLine($"Visitando vértice: {vertice}");

        foreach (int vizinho in ObterVizinhos(vertice))
        {
            if (!visitado[vizinho])
            {
                DFSUtil(vizinho, visitado);
            }
        }
    }

    private bool DFSUtil(int verticeAtual, int verticeAlvo, bool[] visitado)
    {
        if (verticeAtual == verticeAlvo)
        {
            return true; // Encontrou vértice
        }

        visitado[verticeAtual] = true;

        foreach (int vizinho in ObterVizinhos(verticeAtual))
        {
            if (!visitado[vizinho])
            {
                if (DFSUtil(vizinho, verticeAlvo, visitado))
                {
                    return true; // Vértice encontrado em um ramo descendente
                }
            }
        }

        return false; // Vértice não encontrado neste ramo
    }

    public bool BuscarVerticeDFS(int verticeAlvo)
    {
        int verticeInicial = ObterPrimeiroVertice();
        if (verticeInicial == -1)
        {
            return false; // Grafo vazio
        }

        bool[] visitado = new bool[_tamanho];
        return DFSUtil(verticeInicial, verticeAlvo, visitado);
    }

    public bool BuscarVerticeBFS(int verticeAlvo)
    {
        int verticeInicial = ObterPrimeiroVertice();
        if (verticeInicial == -1)
        {
            return false; // Grafo vazio
        }

        bool[] visitado = new bool[_tamanho];
        Queue<int> fila = new Queue<int>();

        visitado[verticeInicial] = true;
        fila.Enqueue(verticeInicial);

        while (fila.Count > 0)
        {
            int verticeAtual = fila.Dequeue();

            if (verticeAtual == verticeAlvo)
            {
                return true; // Encontrou vértice
            }

            foreach (int vizinho in ObterVizinhos(verticeAtual))
            {
                if (!visitado[vizinho])
                {
                    visitado[vizinho] = true;
                    fila.Enqueue(vizinho);
                }
            }
        }

        return false;
    }

    public void LimparGrafo()
    {
        for (int i = 0; i < _tamanho; i++)
        {
            for (int j = 0; i < _tamanho; i++)
            {
                _matrizAdjacencia[i, j] = 0;
            }
        }
    }
}
