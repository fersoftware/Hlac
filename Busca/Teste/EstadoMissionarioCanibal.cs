using System;
using System.Collections.Generic;
using System.Text;
using Busca;
using Busca.Grafo;
using Busca.Util;

namespace Teste {
	class EstadoMissionarioCanibal : Estado {
		public const int MARGEM_INICIAL = 0;
		public const int MARGEM_FINAL = 1;

		private readonly int missionariosMargemInicial, canibaisMargemInicial, barco, h;
		private readonly string operacao;

		public EstadoMissionarioCanibal(int missionariosMargemInicial, int canibaisMargemInicial, int barco, string operacao) {
			this.missionariosMargemInicial = missionariosMargemInicial;
			this.canibaisMargemInicial = canibaisMargemInicial;
			this.barco = barco;
			this.operacao = operacao;
			h = CalcularH();
		}

		public override string ToString() {
			// para poder imprimir a solução completa na tela, incluindo também
			// quem estava de qual lado
			string margemInicial = "";
			string margemFinal = "";
			if (barco == MARGEM_INICIAL) {
				margemInicial = "B";
			} else {
				margemFinal = "B";
			}
			for (int i = 0; i < missionariosMargemInicial; i++) {
				margemInicial += "M";
			}
			for (int i = 0; i < canibaisMargemInicial; i++) {
				margemInicial += "C";
			}
			for (int i = 0; i < 3 - missionariosMargemInicial; i++) {
				margemFinal += "M";
			}
			for (int i = 0; i < 3 - canibaisMargemInicial; i++) {
				margemFinal += "C";
			}
			return margemInicial + "|" + margemFinal + " (" + operacao + ")";
		}

		public override bool Equals(object obj) {
			if (obj == this) {
				return true;
			}
			EstadoMissionarioCanibal e = (obj as EstadoMissionarioCanibal);
			return (e != null &&
				e.missionariosMargemInicial == missionariosMargemInicial &&
				e.canibaisMargemInicial == canibaisMargemInicial &&
				e.barco == barco);
		}

		public override int GetHashCode() {
			// quando o hash code for "simples", ele pode ser calculado diretamente,
			// sem ser armazenado em uma variável
			return (missionariosMargemInicial * 7) + (canibaisMargemInicial * 5) + (barco * 3);
		}

		private int CalcularH() {
			// nossa heurística será:
			// assumiremos que quanto mais pessoas estiverem na margem final,
			// mais próximo da solução estaremos (lembrando: melhor = menor h)

			// será que essa heurística é mesmo válida/útil???
			// compare com a heurística da classes EstadoAspiradorDePoGrande
			// e EstadoRainha

			return (missionariosMargemInicial + canibaisMargemInicial);
		}

		public int Custo {
			get {
				// todas as nossas operações têm o mesmo custo: 1
				// (não existe operação mais complexa que outra, ou mais
				// demorada que outra)
				return 1;
			}
		}

		public string Descricao {
			get {
				return
					"Tres misssionarios e tres canibais estao a beira de um rio e dispoem de " +
					"um barco com capacidade para apenas duas pessoas. O problema e determinar " +
					"as tripulacoes de uma serie de travessias de maneira que todo o grupo passe " +
					"para o outro lado do rio, respeitada a condicao de que em momento algum os " +
					"canibais sejam mais numerosos do que os missionarios em uma das margens do rio.";
			}
		}

		public int H {
			get {
				return h;
			}
		}

		public bool IsMeta {
			get {
				return (h == 0);
			}
		}

		private int MargemOposta(int margem) {
			// se margem == 1, então 0
			// se margem == 0, então 1
			return 1 - margem;
		}

		private bool IsEstadoValido {
			get {
				if (missionariosMargemInicial != 0 &&
					missionariosMargemInicial < canibaisMargemInicial) {
					// há missionários na margem inicial, e eles estão em menor número
					// do que os canibais
					return false;
				}

				int missionariosMargemFinal = 3 - missionariosMargemInicial;
				int canibaisMargemFinal = 3 - canibaisMargemInicial;
				if (missionariosMargemFinal != 0 &&
					missionariosMargemFinal < canibaisMargemFinal) {
					// há missionários na margem final, e eles estão em menor número
					// do que os canibais
					return false;
				}

				return true;
			}
		}

		private EstadoMissionarioCanibal TentarLevar1Missionario() {
			EstadoMissionarioCanibal novoEstado = null;

			if (barco == MARGEM_INICIAL) {
				// tenta levar 1 missionário da margem inicial para a margem final
				if (missionariosMargemInicial >= 1) {
					novoEstado = new EstadoMissionarioCanibal(missionariosMargemInicial - 1, canibaisMargemInicial, MARGEM_FINAL, "Levar 1 missionario para final");
				}
			} else {
				// tenta levar 1 missionário da margem final para a margem inicial
				int missionariosMargemFinal = 3 - missionariosMargemInicial;
				if (missionariosMargemFinal >= 1) {
					novoEstado = new EstadoMissionarioCanibal(missionariosMargemInicial + 1, canibaisMargemInicial, MARGEM_INICIAL, "Levar 1 missionario para inicial");
				}
			}

			if (novoEstado != null && novoEstado.IsEstadoValido) {
				return novoEstado;
			}
			return null;
		}

		private EstadoMissionarioCanibal TentarLevar2Missionarios() {
			EstadoMissionarioCanibal novoEstado = null;

			if (barco == MARGEM_INICIAL) {
				// tenta levar 2 missionários da margem inicial para a margem final
				if (missionariosMargemInicial >= 2) {
					novoEstado = new EstadoMissionarioCanibal(missionariosMargemInicial - 2, canibaisMargemInicial, MARGEM_FINAL, "Levar 2 missionarios para final");
				}
			} else {
				// tenta levar 2 missionários da margem final para a margem inicial
				int missionariosMargemFinal = 3 - missionariosMargemInicial;
				if (missionariosMargemFinal >= 2) {
					novoEstado = new EstadoMissionarioCanibal(missionariosMargemInicial + 2, canibaisMargemInicial, MARGEM_INICIAL, "Levar 2 missionarios para inicial");
				}
			}

			if (novoEstado != null && novoEstado.IsEstadoValido) {
				return novoEstado;
			}
			return null;
		}

		private EstadoMissionarioCanibal TentarLevar1Canibal() {
			EstadoMissionarioCanibal novoEstado = null;

			if (barco == MARGEM_INICIAL) {
				// tenta levar 1 canibal da margem inicial para a margem final
				if (canibaisMargemInicial >= 1) {
					novoEstado = new EstadoMissionarioCanibal(missionariosMargemInicial, canibaisMargemInicial - 1, MARGEM_FINAL, "Levar 1 canibal para final");
				}
			} else {
				// tenta levar 1 canibal da margem final para a margem inicial
				int canibaisMargemFinal = 3 - canibaisMargemInicial;
				if (canibaisMargemFinal >= 1) {
					novoEstado = new EstadoMissionarioCanibal(missionariosMargemInicial, canibaisMargemInicial + 1, MARGEM_INICIAL, "Levar 1 canibal para inicial");
				}
			}

			if (novoEstado != null && novoEstado.IsEstadoValido) {
				return novoEstado;
			}
			return null;
		}

		private EstadoMissionarioCanibal TentarLevar2Canibais() {
			EstadoMissionarioCanibal novoEstado = null;

			if (barco == MARGEM_INICIAL) {
				// tenta levar 2 canibais da margem inicial para a margem final
				if (canibaisMargemInicial >= 2) {
					novoEstado = new EstadoMissionarioCanibal(missionariosMargemInicial, canibaisMargemInicial - 2, MARGEM_FINAL, "Levar 2 canibais para final");
				}
			} else {
				// tenta levar 2 canibais da margem final para a margem inicial
				int canibaisMargemFinal = 3 - canibaisMargemInicial;
				if (canibaisMargemFinal >= 2) {
					novoEstado = new EstadoMissionarioCanibal(missionariosMargemInicial, canibaisMargemInicial + 2, MARGEM_INICIAL, "Levar 2 canibais para inicial");
				}
			}

			if (novoEstado != null && novoEstado.IsEstadoValido) {
				return novoEstado;
			}
			return null;
		}

		private EstadoMissionarioCanibal TentarLevarUmDeCada() {
			EstadoMissionarioCanibal novoEstado = null;

			if (barco == MARGEM_INICIAL) {
				// tenta levar um de cada da margem inicial para a margem final
				if (missionariosMargemInicial >= 1 && canibaisMargemInicial >= 1) {
					novoEstado = new EstadoMissionarioCanibal(missionariosMargemInicial - 1, canibaisMargemInicial - 1, MARGEM_FINAL, "Levar um de cada para final");
				}
			} else {
				// tenta levar um de cada da margem final para a margem inicial
				int missionariosMargemFinal = 3 - missionariosMargemInicial;
				int canibaisMargemFinal = 3 - canibaisMargemInicial;
				if (missionariosMargemFinal >= 1 && canibaisMargemFinal >= 1) {
					novoEstado = new EstadoMissionarioCanibal(missionariosMargemInicial + 1, canibaisMargemInicial + 1, MARGEM_INICIAL, "Levar um de cada para inicial");
				}
			}

			if (novoEstado != null && novoEstado.IsEstadoValido) {
				return novoEstado;
			}
			return null;
		}

		public IEnumerable<Estado> Sucessores {
			get {
				List<Estado> sucessores = new List<Estado>();

				EstadoMissionarioCanibal novoEstado;

				// possíveis estados futuros
				novoEstado = TentarLevar1Missionario();
				if (novoEstado != null) {
					sucessores.Add(novoEstado);
				}

				novoEstado = TentarLevar2Missionarios();
				if (novoEstado != null) {
					sucessores.Add(novoEstado);
				}

				novoEstado = TentarLevar1Canibal();
				if (novoEstado != null) {
					sucessores.Add(novoEstado);
				}

				novoEstado = TentarLevar2Canibais();
				if (novoEstado != null) {
					sucessores.Add(novoEstado);
				}

				novoEstado = TentarLevarUmDeCada();
				if (novoEstado != null) {
					sucessores.Add(novoEstado);
				}

				return sucessores;
			}
		}
	}
}
