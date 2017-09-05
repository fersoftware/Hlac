using System;
using System.Collections.Generic;
using System.Text;

namespace Busca {
	public class BuscaLargura : Busca {
		public BuscaLargura() {
		}

		public BuscaLargura(MostradorDeStatus mostradorDeStatus)
			: base(mostradorDeStatus) {
		}

		public override string ToString() {
			return "BL - Busca em Largura";
		}

		public override No Buscar(Estado inicial) {
			Status.Iniciar();
			IniciarFechados();

			LinkedList<No> abertos = new LinkedList<No>();

			abertos.AddLast(new No(inicial, null));

			while (!Parar && abertos.Count > 0) {
				No n = abertos.First.Value;
				abertos.RemoveFirst();

				Status.Explorando(n, abertos.Count);

				if (n.Estado.IsMeta) {
					Status.Terminar(true);
					return n;
				}

				LinkedList<No> sucessores = Sucessores(n);
				while (sucessores.Count > 0) {
					abertos.AddLast(sucessores.First.Value);
					sucessores.RemoveFirst();
				}
			}

			Status.Terminar(false);
			return null;
		}
	}
}
