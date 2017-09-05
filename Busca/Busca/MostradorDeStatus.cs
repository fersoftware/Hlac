using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Busca {
	public class MostradorDeStatus {
		public volatile Status Status;
		private bool alive;
		private Thread thread;

		public MostradorDeStatus() {
			alive = true;
			thread = new Thread(CorpoDaThread);
			thread.Start();
		}

		public MostradorDeStatus(Status status) {
			Status = status;
			alive = true;
			thread = new Thread(CorpoDaThread);
			thread.Start();
		}

		public void Parar() {
			if (alive) {
				alive = false;
				thread.Interrupt();
			}
		}

		private void MostrarInfoFinal() {
			Console.WriteLine(": Fim da busca. " + Status.NosVisitados + " nos visitados em " + Status.TempoDecorrido + " milissegundos\n");
		}

		private void MostrarInfo() {
			Console.WriteLine("Status:");
			Console.WriteLine("    " + Status.NosVisitados + " nos visitados / nos em aberto = " + Status.NosEmAberto);
			Console.WriteLine("    Profundidade atual = " + Status.ProfundidadeMaxima);
			Console.WriteLine("    Tempo decorrido = " + Status.TempoDecorrido);
		}

		private void CorpoDaThread() {
			while (alive) {
				try {
					Thread.Sleep(1000);
					if (alive && Status != null) {
						MostrarInfo();
					}
				} catch {
				}
			}
			if (Status != null) {
				MostrarInfoFinal();
			}
		}
	}
}
