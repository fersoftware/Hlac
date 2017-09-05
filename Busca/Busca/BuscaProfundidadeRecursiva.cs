using System;
using System.Collections.Generic;
using System.Text;

namespace Busca {
	public class BuscaProfundidadeRecursiva : BuscaProfundidade {
		public BuscaProfundidadeRecursiva() {
		}

		public BuscaProfundidadeRecursiva(int profundidadeMaxima)
			: base(profundidadeMaxima) {
		}

		public BuscaProfundidadeRecursiva(MostradorDeStatus mostradorDeStatus)
			: base(mostradorDeStatus) {
		}

		public BuscaProfundidadeRecursiva(int profundidadeMaxima, MostradorDeStatus mostradorDeStatus)
			: base(profundidadeMaxima, mostradorDeStatus) {
		}

		public override string ToString() {
			return "BP - Busca em Profundidade (profundidade maxima = " + ProfundidadeMaxima + ") - versao recursiva";
		}

		public override No Buscar(Estado inicial) {
			// como o algoritmo é recursivo, não precisa da lista de abertos!
			// (essa lista será simulada pela própria pilha de chamadas do sistema)
			Status.Iniciar();
			IniciarFechados();

			No n = BuscarRecursivamente(new No(inicial, null));

			Status.Terminar(n != null);

			return n;
		}

		private No BuscarRecursivamente(No atual) {
			if (atual == null) {
				return null;
			}

			Status.Explorando(atual, 0);

			if (atual.Estado.IsMeta) {
				return atual;
			}

			if (Parar) {
				return null;
			}

			if (atual.Profundidade < ProfundidadeMaxima) {
				foreach (No s in Sucessores(atual)) {
					// aqui ocorre a simulação do "abertos" da classe BuscaProfundidade
					No n = BuscarRecursivamente(s);
					if (n != null) {
						return n;
					}
				}
			}

			return null;
		}
	}
}
