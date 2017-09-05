using System;
using System.Collections.Generic;
using System.Text;

namespace Busca {
	public class BuscaHeuristicaSubidaDaMontanha : BuscaHeuristica {
		public BuscaHeuristicaSubidaDaMontanha() {
		}

		public BuscaHeuristicaSubidaDaMontanha(MostradorDeStatus mostradorDeStatus) {
		}

		public override string ToString() {
			return "BSM - Busca com Subida da Montanha";
		}

		public override No Buscar(Estado inicial) {
			Status.Iniciar();
			IniciarFechados();

			Estado atual = inicial;
			// vamos armazenar todos os nós para poder imprimir
			// todos os passos quando chegarmos à solução final
			No noAtual = new No(atual, null);

			while (!Parar && atual != null) {
				// acha o menor h entre os filhos (menor = melhor)
				Estado melhorFilhoDeAtual = null;
				foreach (Estado e in atual.Sucessores) {
					if (melhorFilhoDeAtual == null) {
						melhorFilhoDeAtual = e;
					} else if (e.H < melhorFilhoDeAtual.H) {
						melhorFilhoDeAtual = e;
					}
				}

				Status.NosVisitados++;

				if (melhorFilhoDeAtual.H >= atual.H) {
					// o nó atual não possui um filho melhor que si próprio
					if (atual.IsMeta) {
						// apesar de não possuir um filho melhor,
						// o estado atual já resolve o problema!
						Status.Terminar(true);
						return noAtual;
					} else {
						// ficamos sem opções para prosseguir, por isso,
						// se for possível, vamos tentar recomeçar tudo
						// de outro lugar aleatório
						if (atual is GeradorDeEstadoAleatorio) {
							atual = (atual as GeradorDeEstadoAleatorio).GerarEstadoAleatorio();
							noAtual = new No(atual, null);
						} else {
							// realmente não foi possível chegar a uma solução viável
							atual = null;
							noAtual = null;
						}
					}
				} else {
					// o nó atual tem um filho melhor que si próprio
					atual = melhorFilhoDeAtual;
					noAtual = new No(atual, noAtual);
				}
			}

			Status.Terminar(false);
			return null;
		}
	}
}
