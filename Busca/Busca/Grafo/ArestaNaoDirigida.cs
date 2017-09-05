using System;
using System.Collections.Generic;
using System.Text;

namespace Busca.Grafo {
	public class ArestaNaoDirigida : Aresta {
		public ArestaNaoDirigida(Vertice vi, Vertice vj)
			: base(vi, vj) {
			vi.Arestas.Add(this);
			vj.Arestas.Add(this);
		}

		public ArestaNaoDirigida(Vertice vi, Vertice vj, int custo)
			: base(vi, vj, custo) {
			vi.Arestas.Add(this);
			vj.Arestas.Add(this);
		}

		public override string ToString() {
			if (Custo == 0) {
				return Vi + " <-> " + Vj;
			}

			return Vi + " <- " + Custo + " -> " + Vj;
		}
	}
}
