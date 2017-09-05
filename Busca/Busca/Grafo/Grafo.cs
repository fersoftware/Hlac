using System;
using System.Collections.Generic;
using System.Text;

namespace Busca.Grafo {
	public abstract class Grafo {
		protected readonly Dictionary<int, Vertice> Vertices = new Dictionary<int, Vertice>();

		public override string ToString() {
			StringBuilder r = new StringBuilder();
			foreach (Vertice v in Vertices.Values) {
				r.Append(v);
				r.Append(": ");
				bool primeiro = true;
				foreach (Aresta a in v.Arestas) {
					if (primeiro) {
						primeiro = false;
					} else {
						r.Append(", ");
					}
					r.Append(a);
				}
				r.AppendLine();
			}
			return r.ToString();
		}

		private void AdicionarVertice(Vertice v) {
			Vertices[v.Id] = v;
		}

		public Vertice CriarVertice(int id) {
			Vertice v = new Vertice(id);
			Vertices[id] = v;
			return v;
		}

		public Vertice CriarVertice(int id, string nome) {
			Vertice v = new Vertice(id, nome);
			Vertices[id] = v;
			return v;
		}

		public Vertice GetVertice(int id) {
			Vertice v;
			Vertices.TryGetValue(id, out v);
			return v;
		}

		public abstract void CriarAresta(int i, int j);

		public abstract void CriarAresta(int i, int j, int custo);
	}
}
