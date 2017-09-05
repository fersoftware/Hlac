using System;
using System.Collections.Generic;
using System.Text;

namespace Busca {
	public abstract class BuscaHeuristica : Busca, IComparer<No> {
		public BuscaHeuristica() {
		}

		public BuscaHeuristica(MostradorDeStatus mostradorDeStatus)
			: base(mostradorDeStatus) {
		}

		public override string ToString() {
			return "Algoritmo de busca geral com heuristica";
		}

		public int Compare(No x, No y) {
			// se x.F for maior do que y.F, x irá para depois de y na fila
			return x.F - y.F;
		}
	}
}
