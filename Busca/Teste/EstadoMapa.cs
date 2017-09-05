using System;
using System.Collections.Generic;
using System.Text;
using Busca;
using Busca.Grafo;
using Busca.Util;

namespace Teste {
	class EstadoMapa : Estado {
		// vamos deixar o mapa e a cidade final dentro da
		// própria classe, por comodidade
		public static readonly Grafo Mapa = CriarMapa();
		public static Vertice CidadeFinal = Mapa.GetVertice(0);

		private static Grafo CriarMapa() {
			// ao criarmos um grafo não dirigido, é como se todas as
			// estradas entre as cidades possuíssem mão dupla!
			Grafo mapa = new GrafoNaoDirigido();

			// primeiro criamos as cidades
			// (os nomes serão as letras de A até O)
			for (int i = 0; i < 16; i++) {
				mapa.CriarVertice(i, ((char)('A' + i)).ToString());
			}

			// agora criamos as estradas que ligam as cidades
			mapa.CriarAresta(0, 1, 3);
			mapa.CriarAresta(0, 2, 6);

			mapa.CriarAresta(1, 9, 3);
			mapa.CriarAresta(1, 7, 3);

			mapa.CriarAresta(2, 13, 2);
			mapa.CriarAresta(2, 14, 2);
			mapa.CriarAresta(2, 3, 3);
			mapa.CriarAresta(2, 6, 2);

			mapa.CriarAresta(3, 4, 1);
			mapa.CriarAresta(3, 5, 1);

			mapa.CriarAresta(4, 5, 1);
			mapa.CriarAresta(4, 8, 2);
			mapa.CriarAresta(4, 11, 14);

			mapa.CriarAresta(5, 6, 1);

			mapa.CriarAresta(6, 7, 2);

			mapa.CriarAresta(7, 8, 2);
			mapa.CriarAresta(7, 9, 4);

			mapa.CriarAresta(9, 10, 1);
			mapa.CriarAresta(9, 12, 3);

			mapa.CriarAresta(11, 12, 2);
			mapa.CriarAresta(11, 15, 1);

			return mapa;
		}

		private readonly Vertice cidadeAtual;
		private readonly int custoDaCidadeAnteriorParaAAtual;

		public EstadoMapa(Vertice cidadeAtual, int custoDaCidadeAnteriorParaAAtual) {
			this.cidadeAtual = cidadeAtual;
			this.custoDaCidadeAnteriorParaAAtual = custoDaCidadeAnteriorParaAAtual;
		}

		public override string ToString() {
			// para poder imprimir a solução completa na tela
			return cidadeAtual.ToString();
		}

		public override bool Equals(object obj) {
			if (obj == this) {
				return true;
			}
			EstadoMapa e = (obj as EstadoMapa);
			return (e != null &&
				e.cidadeAtual.Equals(cidadeAtual) &&
				e.custoDaCidadeAnteriorParaAAtual == custoDaCidadeAnteriorParaAAtual);
		}

		public override int GetHashCode() {
			// quando o hash code for "simples", ele pode ser calculado diretamente,
			// sem ser armazenado em uma variável
			return (cidadeAtual.Id * 31) + (custoDaCidadeAnteriorParaAAtual * 11);
		}

		public int Custo {
			get {
				// para sair da cidade anterior e chegar nesta cidade atual
				// existe um custo, que não é constante!!!
				return custoDaCidadeAnteriorParaAAtual;
			}
		}

		public string Descricao {
			get {
				return "Problema de encontrar rotas no mapa: " + Mapa.ToString();
			}
		}

		public int H {
			get {
				// perceba que essa heurística é MUITO ruim!
				// estamos apenas pegando a "distância entre os id's" das cidades
				// uma heurística melhor poderia ser utilizar a distância em linha
				// reta entre a cidade atual e a cidade final (caso possuíssemos
				// as coordenadas geográficas das cidades...)
				return Math.Abs(CidadeFinal.Id - cidadeAtual.Id);
			}
		}

		public bool IsMeta {
			get {
				return cidadeAtual.Equals(CidadeFinal);
			}
		}

		public IEnumerable<Estado> Sucessores {
			get {
				List<Estado> sucessores = new List<Estado>();

				// possíveis estados futuros (cidades para onde é possível
				// ir partindo da cidade deste estado atual)
				foreach (Aresta a in cidadeAtual.Arestas) {
					sucessores.Add(new EstadoMapa(a.Vizinho(cidadeAtual), a.Custo));
				}

				return sucessores;
			}
		}
	}
}
