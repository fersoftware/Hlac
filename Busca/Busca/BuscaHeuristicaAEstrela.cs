using Busca.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace Busca {
	public class BuscaHeuristicaAEstrela : BuscaHeuristica {
		// máximo valor de f que pode ser aceito (-1 é ilimitado)
		public int MaximoF = -1;

		// máximo número de nós abertos que pode ser aceito (-1 é ilimitado)
		public int MaximoAbertos = -1;

		public BuscaHeuristicaAEstrela() {
		}

		public BuscaHeuristicaAEstrela(MostradorDeStatus mostradorDeStatus)
			: base(mostradorDeStatus) {
		}

		public override string ToString() {
			return "Busca Heuristica A*";
		}

		public override No Buscar(Estado inicial) {
			Status.Iniciar();
			IniciarFechados();

			// exceto pela comparação dos limites MaximoF e MaximoAbertos
			// perceba como essa busca é muito similar à busca em largura,
			// utilizando uma fila de prioridades em vez de uma fila comum
			// (uma fila de prioridades é uma fila que permanece sempre
			// ordenada de acordo com algum critério)

			// lista ordenada por f()
			PriorityQueue<No> abertos = new PriorityQueue<No>(100, this);

			abertos.Add(new No(inicial, null));

			while (!Parar && abertos.Count > 0) {
				No melhor = abertos.Remove();

				Status.Explorando(melhor, abertos.Count);

				if (melhor.Estado.IsMeta) {
					Status.Terminar(true);
					return melhor;
				}

				if (MaximoF < 0 || melhor.F < MaximoF) {
					LinkedList<No> sucessores = Sucessores(melhor);
					while (sucessores.Count > 0) {
						abertos.Add(sucessores.First.Value);
						sucessores.RemoveFirst();
					}
				}

				if (MaximoAbertos > 0 && abertos.Count > MaximoAbertos) {
					// estourou o limite máximo de nós abertos
					break;
				}
			}

			Status.Terminar(false);
			return null;
		}
	}
}
