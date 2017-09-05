using System;
using System.Collections.Generic;
using System.Text;

namespace Busca {
	public class BuscaProfundidadeIterativa : Busca {
		private BuscaProfundidade buscaProfundidade = new BuscaProfundidade();

		public BuscaProfundidadeIterativa() {
		}

		public BuscaProfundidadeIterativa(MostradorDeStatus mostradorDeStatus)
			: base(mostradorDeStatus) {
		}

		public override string ToString() {
			return "BPI - Busca em Profundidade Iterativa";
		}

		public override void InterromperBusca() {
			base.InterromperBusca();
			buscaProfundidade.InterromperBusca();
		}

		public override No Buscar(Estado inicial) {
			Status.Iniciar();
			IniciarFechados();

			int prof = 0;
			while (!Parar) {
				// indica a profundidade máxima que a busca pode ir nessa iteração
				buscaProfundidade.ProfundidadeMaxima = prof;

				Status.ProfundidadeMaxima = prof;
				prof++;

				No n = buscaProfundidade.Buscar(inicial);
				// acumula das varias buscas em profundidade
				Status.NosVisitados += buscaProfundidade.Status.NosVisitados;

				if (n != null) {
					Status.Terminar(true);
					return n;
				}
			}

			Status.Terminar(false);
			return null;
		}
	}
}
