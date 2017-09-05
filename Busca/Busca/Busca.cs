using System;
using System.Collections.Generic;
using System.Text;

namespace Busca {
	public abstract class Busca {
		public bool Parar;
		public bool Podar = true;
		public bool UsarFechados = true;

		// status e mostradorDeStatus servem apenas para mostrar o andamento da busca atual
		private Status status = new Status();
		private MostradorDeStatus mostradorDeStatus;

		// mapeia os estados para um custo g
		private Dictionary<Estado, int> fechados;

		public Busca() {
		}

		public Busca(MostradorDeStatus mostradorDeStatus) {
			MostradorDeStatus = mostradorDeStatus;
		}

		public override string ToString() {
			return "Algoritmo de busca geral";
		}

		public abstract No Buscar(Estado inicial);

		protected void IniciarFechados() {
			fechados = new Dictionary<Estado, int>();
		}

		public Status Status {
			get {
				return status;
			}
		}

		public Status NovoStatus() {
			status = new Status();
			if (mostradorDeStatus != null) {
				mostradorDeStatus.Status = status;
			}
			status.MostradorDeStatus = mostradorDeStatus;
			return status;
		}

		public MostradorDeStatus MostradorDeStatus {
			get {
				return mostradorDeStatus;
			}
			set {
				if (mostradorDeStatus != null) {
					mostradorDeStatus.Parar();
				}
				mostradorDeStatus = value;
				if (mostradorDeStatus != null) {
					mostradorDeStatus.Status = status;
				}
				status.MostradorDeStatus = mostradorDeStatus;
			}
		}

		public virtual void InterromperBusca() {
			Parar = true;
			status.Terminar(false);
		}

		public LinkedList<No> Sucessores(No pai) {
			// lista de todos os sucessores do nó atual
			return SomenteNovosNos(pai.Estado.Sucessores, pai);
		}

		public LinkedList<No> Antecessores(No pai) {
			if (pai.Estado is Antecessor) {
				return SomenteNovosNos((pai.Estado as Antecessor).Antecessores, pai);
			}
			Console.WriteLine("O estado " + pai.Estado + " nao implementa antecessores!");
			return new LinkedList<No>();
		}

		private LinkedList<No> SomenteNovosNos(IEnumerable<Estado> estados, No pai) {
			// a lista de sucessores (ou antecessores) novos
			LinkedList<No> novosNos = new LinkedList<No>();
			foreach (Estado e in estados) {
				No filho = new No(e, pai);
				if (Podar) {
					if (UsarFechados && fechados != null) {
						int custo;
						// o nó ainda está em aberto (não está fechado)
						// ou tem custo menor que o custo atual
						if (fechados.TryGetValue(e, out custo) == false || filho.G < custo) {
							novosNos.AddLast(filho);
							fechados[e] = filho.G;
						}
					} else if (filho.IsDescendenteNovo(pai)) {
						// poda os filhos que têm um ancestral igual a ele
						novosNos.AddLast(filho);
					}
				} else {
					novosNos.AddLast(filho);
				}
			}
			return novosNos;
		}
	}
}
