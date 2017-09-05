using System;
using System.Collections.Generic;
using System.Text;

namespace Busca.Grafo {
	public class ArestaDirigida : Aresta {
		// Nas arestas dirigidas:
		// - Origem: vi
		// - Destino: vj
		public ArestaDirigida(Vertice vi, Vertice vj)
			: base(vi, vj) {
			vi.Arestas.Add(this);
		}

		public ArestaDirigida(Vertice vi, Vertice vj, int custo)
			: base(vi, vj, custo) {
			vi.Arestas.Add(this);
		}

		public override string ToString() {
			if (Custo == 0) {
				return Vi + " -> " + Vj;
			}

			return Vi + " - " + Custo + " -> " + Vj;
		}
	}
}
