using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Busca {
	public class Status {
		public int NosVisitados;
		public int ProfundidadeMaxima; // a máxima profundidade que a busca foi
		public int NosEmAberto;
		private DateTime tempoInicio;
		public MostradorDeStatus MostradorDeStatus;
		private bool resolvido;

		public void Iniciar() {
			NosVisitados = 0;
			ProfundidadeMaxima = 0;
			tempoInicio = DateTime.Now;
		}

		public void Terminar(bool resolvido) {
			this.resolvido = resolvido;
			if (MostradorDeStatus != null) {
				MostradorDeStatus.Parar();
			}
		}

		public bool Resolvido {
			get {
				return resolvido;
			}
		}

		public int TempoDecorrido {
			get {
				return (int)(DateTime.Now - tempoInicio).TotalMilliseconds;
			}
		}

		/* o algoritmo pegou n para explorar de um total de s */
		public void Explorando(No n, int nosEmAberto) {
			NosVisitados++;
			NosEmAberto = nosEmAberto;

			if (n.Profundidade > ProfundidadeMaxima) {
				ProfundidadeMaxima = n.Profundidade;
			}
		}
	}
}
