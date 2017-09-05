using System;
using System.Collections.Generic;
using System.Text;
using Busca;
using Busca.Grafo;
using Busca.Util;

namespace Teste {
	class EstadoAspiradorDePo : Estado {
		public const int LIMPO = 0;
		public const int SUJO = 1;
		public const int ESQUERDA = 0;
		public const int DIREITA = 1;

		private readonly int quartoEsquerdo, quartoDireito, posicao;
		private readonly string operacao;

		public EstadoAspiradorDePo(int quartoEsquerdo, int quartoDireito, int posicao, string operacao) {
			this.quartoEsquerdo = quartoEsquerdo;
			this.quartoDireito = quartoDireito;
			this.posicao = posicao;
			this.operacao = operacao;
		}

		public override string ToString() {
			// para poder imprimir a solução completa na tela
			return operacao;
		}

		public override bool Equals(object obj) {
			if (obj == this) {
				return true;
			}
			EstadoAspiradorDePo e = (obj as EstadoAspiradorDePo);
			return (e != null &&
				e.quartoEsquerdo == quartoEsquerdo &&
				e.quartoDireito == quartoDireito &&
				e.posicao == posicao);
		}

		public override int GetHashCode() {
			// quando o hash code for "simples", ele pode ser calculado diretamente,
			// sem ser armazenado em uma variável
			return (quartoEsquerdo * 7) + (quartoDireito * 5) + (posicao * 3);
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
				return "Problema classico do aspirador de po";
			}
		}

		public int H {
			get {
				// esse problema não possui uma heurística
				throw new NotImplementedException();
			}
		}

		public bool IsMeta {
			get {
				return (quartoEsquerdo == LIMPO) && (quartoDireito == LIMPO);
			}
		}

		public IEnumerable<Estado> Sucessores {
			get {
				List<Estado> sucessores = new List<Estado>();

				// possíveis estados futuros
				if (posicao == DIREITA) {
					// ir para a esquerda e não limpar nada
					sucessores.Add(new EstadoAspiradorDePo(quartoEsquerdo, quartoDireito, ESQUERDA, "Ir para esquerda"));

					// limpar o quarto direito
					if (quartoDireito == SUJO) {
						sucessores.Add(new EstadoAspiradorDePo(quartoEsquerdo, LIMPO, DIREITA, "Limpar o quarto direito"));
					}
				} else {
					// ir para a direita e não limpar nada
					sucessores.Add(new EstadoAspiradorDePo(quartoEsquerdo, quartoDireito, DIREITA, "Ir para direita"));

					// limpar o quarto esquerdo
					if (quartoEsquerdo == SUJO) {
						sucessores.Add(new EstadoAspiradorDePo(LIMPO, quartoDireito, ESQUERDA, "Limpar o quarto esquerdo"));
					}
				}

				return sucessores;
			}
		}
	}
}
