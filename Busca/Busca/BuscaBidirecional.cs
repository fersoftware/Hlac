using System;
using System.Collections.Generic;
using System.Text;

namespace Busca {
	public class BuscaBidirecional : Busca {
		public BuscaBidirecional() {
		}

		public BuscaBidirecional(MostradorDeStatus mostradorDeStatus)
			: base(mostradorDeStatus) {
		}

		public override string ToString() {
			return "BBD - Busca Bidirecional";
		}

		public override No Buscar(Estado inicial) {
    			throw new Exception("Esta classe não implementa a busca com um único parâmetro");
		}

		public No Buscar(Estado inicial, Estado meta) {
			Status.Iniciar();
			// tem que usar poda só por ascendência! não pode usar nós fechados!
			UsarFechados = false;

			// melhorias pendentes para aplicar ao código:
			// - ter duas tabelas de fechados, uma para cada lado
			// - usar uma árvore para ter ordem (g) e ser rápido de achar um nó

			LinkedList<No> abertosCima = new LinkedList<No>();
			LinkedList<No> abertosBaixo = new LinkedList<No>();

			abertosCima.AddLast(new No(inicial, null));
			No noMeta = new No(meta, null);
			abertosBaixo.AddLast(noMeta);

			while (!Parar && abertosCima.Count > 0 && abertosBaixo.Count > 0) {

				// incrementa em cima
				No n = abertosCima.First.Value;
				abertosCima.RemoveFirst();

				Status.Explorando(n, abertosCima.Count + abertosBaixo.Count);

				// verifica se tem n na borda da árvore de baixo
				LinkedListNode<No> io = abertosBaixo.Find(n);
				if (io != null) {
					No nb = io.Value;
					nb.InvertePaternidade();
					nb.Pai = n.Pai;
					noMeta.AtualizaProfundidade();
					Status.Terminar(true);
					return noMeta;
				}

				LinkedList<No> sucessores = Sucessores(n);
				while (sucessores.Count > 0) {
					abertosCima.AddLast(sucessores.First.Value);
					sucessores.RemoveFirst();
				}

				// incrementa para baixo
				n = abertosBaixo.First.Value;
				abertosBaixo.RemoveFirst();

				Status.Explorando(n, abertosCima.Count + abertosBaixo.Count);

				// ve se tem n na borda da árvore de cima
				io = abertosCima.Find(n);
				if (io != null) {
					No nc = io.Value;
					n.InvertePaternidade();
					n.Pai = nc.Pai;
					noMeta.AtualizaProfundidade();
					Status.Terminar(true);
					return noMeta;
				}

				LinkedList<No> antecessores = Antecessores(n);
				while (antecessores.Count > 0) {
					abertosBaixo.AddLast(antecessores.First.Value);
					antecessores.RemoveFirst();
				}
			}

			Status.Terminar(false);
			return null;
		}
	}
}
