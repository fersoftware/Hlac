using System;
using System.Collections.Generic;
using System.Text;

namespace Busca {
	public class BuscaProfundidade : Busca {
		// pronfundidade máxima que a busca pode ir, antes de desistir
		// (isso serve para evitar que a busca execute infinitamente)
		public int ProfundidadeMaxima = 40;

		public BuscaProfundidade() {
		}

		public BuscaProfundidade(int profundidadeMaxima) {
			ProfundidadeMaxima = profundidadeMaxima;
		}

		public BuscaProfundidade(MostradorDeStatus mostradorDeStatus)
			: base(mostradorDeStatus) {
		}

		public BuscaProfundidade(int profundidadeMaxima, MostradorDeStatus mostradorDeStatus)
			: base(mostradorDeStatus) {
			ProfundidadeMaxima = profundidadeMaxima;
		}

		public override string ToString() {
			return "BP - Busca em Profundidade (profundidade maxima = " + ProfundidadeMaxima + ")";
		}

		public override No Buscar(Estado inicial) {
			Status.Iniciar();
			IniciarFechados();

			LinkedList<No> abertos = new LinkedList<No>();

			abertos.AddFirst(new No(inicial, null));

			while (!Parar && abertos.Count > 0) {
				No n = abertos.First.Value;
				abertos.RemoveFirst();

				Status.Explorando(n, abertos.Count);

				if (n.Estado.IsMeta) {
					Status.Terminar(true);
					return n;
				}

				if (n.Profundidade < ProfundidadeMaxima) {
					LinkedList<No> sucessores = Sucessores(n);
					while (sucessores.Count > 0) {
						abertos.AddFirst(sucessores.Last.Value);
						sucessores.RemoveLast();
					}
				}
			}

			Status.Terminar(false);
			return null;
		}
	}
}
