using System;
using System.Collections.Generic;
using System.Text;

namespace Busca.Grafo {
	public class Vertice {
		public readonly int Id;
		public readonly string Nome;
		public readonly HashSet<Aresta> Arestas;

		public Vertice(int id) {
			Id = id;
			Nome = null;
			Arestas = new HashSet<Aresta>();
		}

		public Vertice(int id, string nome) {
			Id = id;
			Nome = nome;
			Arestas = new HashSet<Aresta>();
		}

		public override string ToString() {
			return (Nome ?? Id.ToString());
		}

		public override bool Equals(object obj) {
			if (obj == this) {
				return true;
			}
			Vertice v = obj as Vertice;
			return (v != null &&
				v.Id == Id);
		}

		public override int GetHashCode() {
			return Id * 17;
		}
	}
}
