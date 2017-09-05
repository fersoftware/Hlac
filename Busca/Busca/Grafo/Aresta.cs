using System;
using System.Collections.Generic;
using System.Text;

namespace Busca.Grafo {
	public abstract class Aresta {
		public readonly Vertice Vi, Vj;
		public readonly int Custo;

		public Aresta(Vertice vi, Vertice vj) {
			Vi = vi;
			Vj = vj;
			Custo = 0;
		}

		public Aresta(Vertice vi, Vertice vj, int custo) {
			Vi = vi;
			Vj = vj;
			Custo = custo;
		}

		public Vertice Vizinho(Vertice v) {
			if (Vi == v) {
				return Vj;
			}

			return Vi;
		}

		public Vertice Vizinho(int id) {
			if (Vi.Id == id) {
				return Vj;
			}

			return Vi;
		}

		public override bool Equals(object obj) {
			if (obj == this) {
				return true;
			}
			Aresta a = obj as Aresta;
			return (a != null &&
				a.Vi.Equals(Vi) &&
				a.Vj.Equals(Vj));
		}

		public override int GetHashCode() {
			return (Vi.Id * 31) + (Vj.Id * 7);
		}
	}
}
