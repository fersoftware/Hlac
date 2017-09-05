using System;
using System.Collections.Generic;
using System.Text;

namespace Busca.Grafo {
	public class GrafoDirigido : Grafo {
		public override void CriarAresta(int i, int j) {
			Vertice vi = Vertices[i];
			Vertice vj = Vertices[j];
			new ArestaDirigida(vi, vj);
		}

		public override void CriarAresta(int i, int j, int custo) {
			Vertice vi = Vertices[i];
			Vertice vj = Vertices[j];
			new ArestaDirigida(vi, vj, custo);
		}
	}
}
